using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Shared;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Shared.Calculate;

public class Validator : Data.Shipments.BaseUnitTests
{
	private readonly CalculateShipmentValidator validator = new();
	private readonly CalculateShipmentQuery request = new(Weights, Address);

	private static readonly double[] Weights = [0, 1, 2, 3, 4, 5, 6];
	private static readonly AddressDto Address = new("Bulgaria", "Burgas", "Slivnitsa");

	[Test]
	public async Task Validate_ShouldBeValid_WhenAddressIsValid()
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(request);

		// Assert
		await Assert.That(result.IsValid).IsTrue();
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldBeInvalid_WhenAddressIsInvalid(string country, string city, string street)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(request with { Address = new(country, city, street) });

		// Assert
		await Assert.That(result.IsValid).IsFalse();
	}
}
