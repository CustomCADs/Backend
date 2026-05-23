using CustomCADs.Modules.Files.Application.Cads.Commands.Internal.Edit;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Internal.Edit;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly EditCadHandler handler;
	private readonly EditCadCommand request = new(ValidId, ValidContentType, ValidVolume, ValidOwnerId);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private readonly Cad cad = CreateCad();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, cad),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldModifyCad()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(ValidKey, cad.Key),
			() => Assert.Equal(ValidContentType, cad.ContentType)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Cad);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
