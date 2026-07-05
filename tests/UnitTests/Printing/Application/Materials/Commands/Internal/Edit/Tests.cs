using CustomCADs.Modules.Printing.Application.Materials.Commands.Internal.Edit;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Commands.Internal.Edit;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	private readonly EditMaterialHandler handler;
	private readonly EditMaterialCommand request = new(
		Id: ValidId,
		Name: MaxValidName,
		Density: MaxValidDensity,
		Cost: MaxValidCost
	);

	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private readonly Material material = CreateMaterial();

	public Tests()
	{
		handler = new(reads.Object, cache.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(material);
	}

	[Test]
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

	[Test]
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

	[Test]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, material),
			Times.Once()
		);
	}
}
