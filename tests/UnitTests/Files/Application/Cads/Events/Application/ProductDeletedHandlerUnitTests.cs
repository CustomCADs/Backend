using CustomCADs.Modules.Files.Application.Cads.Events.Application;
using CustomCADs.Modules.Files.Application.Cads.Storage;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Events.Files;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Files.Application.Cads.Events.Application;

using static CadsData;

public class ProductDeletedHandlerUnitTests : CadsBaseUnitTests
{
	private readonly ProductDeletedHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<ICadStorageService> storage = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private static readonly Cad cad = CreateCad();

	public ProductDeletedHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, storage.Object, cache.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ProductDeletedApplicationEvent ae = new(
			Id: default,
			CadId: ValidId,
			ImageId: default
		);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductDeletedApplicationEvent ae = new(
			Id: default,
			CadId: ValidId,
			ImageId: default
		);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		writes.Verify(x => x.Remove(cad), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		ProductDeletedApplicationEvent ae = new(
			Id: default,
			CadId: ValidId,
			ImageId: default
		);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		cache.Verify(
			x => x.ClearAsync(ValidId),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		ProductDeletedApplicationEvent ae = new(
			Id: default,
			CadId: ValidId,
			ImageId: default
		);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		storage.Verify(x => x.DeleteFileAsync(cad.Key, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Cad);

		ProductDeletedApplicationEvent ae = new(
			Id: default,
			CadId: ValidId,
			ImageId: default
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			async () => await handler.HandleAsync(ae)
		);
	}
}
