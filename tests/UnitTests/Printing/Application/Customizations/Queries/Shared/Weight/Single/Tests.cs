using CustomCADs.Modules.Printing.Application.Customizations.Queries.Shared.Weight;
using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Modules.Printing.Domain.Services;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Shared.Weight.Single;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly GetCustomizationWeightByIdHandler handler;
	private readonly GetCustomizationWeightByIdQuery request = new(ValidId);

	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<IPrintCalculator> calculator = new();

	private const double Weight = 10.5;
	private readonly Customization customization = CreateCustomization();
	private readonly Material material = CreateMaterial();

	public Tests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(customization);

		materialReads.Setup(x => x.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(material);

		calculator.Setup(x => x.CalculateWeight(It.IsAny<Customization>(), It.IsAny<Material>()))
			.Returns((decimal)Weight);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
		materialReads.Verify(
			x => x.SingleByIdAsync(ValidMaterialId, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldCalculateWeight()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		calculator.Verify(
			x => x.CalculateWeight(customization, material),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		double result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(Weight, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Customization);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Customization>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenMaterialNotFound()
	{
		// Arrange
		materialReads.Setup(x => x.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(null as Material);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Material>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
