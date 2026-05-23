using CustomCADs.Modules.Files.Application.Cads.Commands.Internal.Create;
using CustomCADs.Modules.Files.Domain.Repositories;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Internal.Create;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly CreateCadHandler handler;
	private readonly CreateCadCommand request = new(ValidKey, ValidContentType, ValidVolume, ValidOwnerId);

	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object, cache.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Cad>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
			ct
		)).ReturnsAsync(CreateCad(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Cad>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
				ct
			),
			Times.Once()
		);
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
			x => x.UpdateAsync(
				ValidId,
				It.Is<Cad>(x => x.Key == ValidKey && x.ContentType == ValidContentType)
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CadId id = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
