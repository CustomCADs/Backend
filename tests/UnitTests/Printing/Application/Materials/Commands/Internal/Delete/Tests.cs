using CustomCADs.Modules.Printing.Application.Materials.Commands.Internal.Delete;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Commands.Internal.Delete;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	private readonly DeleteMaterialHandler handler;
	private readonly DeleteMaterialCommand request = new(ValidId);

	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<IWrites<Material>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private readonly Material material = CreateMaterial();

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(material);
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
		writes.Verify(
			x => x.Remove(material),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldClearCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.ClearAsync(ValidId),
			Times.Once()
		);
	}
}
