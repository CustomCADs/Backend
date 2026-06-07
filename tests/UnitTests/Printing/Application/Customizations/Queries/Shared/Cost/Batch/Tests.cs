using CustomCADs.Modules.Printing.Application.Customizations.Queries.Shared.Cost;
using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Modules.Printing.Domain.Services;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Cost.Batch;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly BatchGetCustomizationCostByIdHandler handler;
	private readonly BatchGetCustomizationCostByIdQuery request = new(Ids);

	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<IPrintCalculator> calculator = new();

	private const decimal Cost = 10.5m;
	private static readonly CustomizationId[] Ids = [];
	private static readonly Customization[] Customizations = [CreateCustomization()];
	private static readonly MaterialId[] MaterialIds = [.. Customizations.Select(x => x.MaterialId)];
	private static readonly Dictionary<MaterialId, Material> Materials = new()
	{
		[ValidMaterialId] = CreateMaterial(),
	};

	public Tests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		reads.Setup(x => x.AllAsync(Ids, false, ct))
			.ReturnsAsync(Customizations);

		materialReads.Setup(x => x.AllByIdsAsync(MaterialIds, false, ct))
			.ReturnsAsync(Materials);

		calculator.Setup(x => x.CalculateCost(It.IsAny<Customization>(), It.IsAny<Material>()))
			.Returns(Cost);
	}

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldCalculateCost()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		calculator.Verify(
			x => x.CalculateCost(
					It.Is<Customization>(x => Customizations.Contains(x)),
					It.Is<Material>(x => Materials.Values.Contains(x))
				),
			Times.Exactly(Customizations.Length)
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		Assert.Multiple([.. result.Select(
			x => (Action)(() => Assert.Equal(Cost, x.Value))
		)]);
	}
}
