using CustomCADs.Files.Application.Cads.Commands.Internal.Create;
using CustomCADs.Files.Domain.Repositories;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Internal.Create;

using static CadsData;

public class CreateCadHandlerUnitTests : CadsBaseUnitTests
{
	private readonly CreateCadHandler handler;
	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	public CreateCadHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Cad>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
			ct
		)).ReturnsAsync(CreateCadWithId(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateCadCommand command = new(ValidKey, ValidContentType, ValidVolume, ValidOwnerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Cad>(x => x.Key == ValidKey && x.ContentType == ValidContentType),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		CreateCadCommand command = new(ValidKey, ValidContentType, ValidVolume, ValidOwnerId);

		// Act
		await handler.Handle(command, ct);

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
		CreateCadCommand command = new(ValidKey, ValidContentType, ValidVolume, ValidOwnerId);

		// Act
		CadId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
