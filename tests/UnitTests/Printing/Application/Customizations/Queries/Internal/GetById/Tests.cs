using CustomCADs.Modules.Printing.Application.Customizations.Queries.Internal.GetById;
using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Modules.Printing.Domain.Services;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Queries.Internal.GetById;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly GetCustomizationByIdHandler handler;
	private readonly GetCustomizationByIdQuery request = new(ValidId);

	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IMaterialReads> materialReads = new();
	private readonly Mock<IPrintCalculator> calculator = new();

	private readonly Customization customization = CreateCustomization(id: ValidId);
	private readonly Material material = CreateMaterial();

	public Tests()
	{
		handler = new(reads.Object, materialReads.Object, calculator.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(customization);

		materialReads.Setup(x => x.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(material);
	}

	[Test]
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

	[Test]
	public async Task Handle_ShouldCalculateWeightAndCost()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		calculator.Verify(
			x => x.CalculateWeight(customization, material),
			Times.Once()
		);
		calculator.Verify(
			x => x.CalculateCost(customization, material),
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
		await Assert.That(result.Id).IsEqualTo(ValidId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as Customization);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Customization>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenMaterialNotFound()
	{
		// Arrange
		materialReads.Setup(x => x.SingleByIdAsync(ValidMaterialId, false, ct))
			.ReturnsAsync(null as Material);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Material>>(() => handler.Handle(request, ct));
	}
}
