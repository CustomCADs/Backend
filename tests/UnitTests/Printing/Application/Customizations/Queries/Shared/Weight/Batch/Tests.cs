using CustomCADs.Modules.Printing.Application.Customizations.Queries.Shared.Weight;
using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Weight.Batch;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly BatchGetCustomizationWeightByIdHandler handler;
	private readonly BatchGetCustomizationWeightByIdQuery request = new(Ids);

	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();

	private const double Weight = 10.5;
	private static readonly CustomizationId[] Ids = [];
	private static readonly Customization[] Customizations = [CreateCustomization()];
	private static readonly MaterialId[] MaterialIds = [.. Customizations.Select(x => x.MaterialId)];
	private static readonly Dictionary<MaterialId, Material> Materials = new()
	{
		[ValidMaterialId] = CreateMaterial(),
	};

	public Tests()
	{
		handler = new(reads.Object, materialReads.Object);

		reads.Setup(x => x.AllAsync(Ids, false, ct))
			.ReturnsAsync(Customizations);

		materialReads.Setup(x => x.AllByIdsAsync(MaterialIds, false, ct))
			.ReturnsAsync(Materials);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(Ids, false, ct),
			Times.Once()
		);
		materialReads.Verify(
			x => x.AllByIdsAsync(MaterialIds, false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			foreach (var (_, weight) in result)
			{
				await Assert.That(weight).IsEqualTo(Weight);
			}
		}
	}
}
