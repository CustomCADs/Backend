using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

using static Data.ActiveCarts.TestData;

public class Validator : Data.ActiveCarts.BaseUnitTests
{
	private readonly PurchaseActiveCartWithDeliveryValidator validator = new();
	private static PurchaseActiveCartWithDeliveryCommand Request(Theory theory)
		=> new(
			CallerId: ValidBuyerId,
			PaymentMethodId: theory.PaymentMethodId,
			ShipmentService: theory.ShipmentService,
			Address: new(theory.Country, theory.City, theory.Street),
			Contact: new(theory.Phone, theory.Email)
		);

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldBeValid_WhenCartIsValid(Theory theory)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(Request(theory), cancellationToken: ct);

		// Assert
		await Assert.That(result.IsValid).IsTrue();
	}

	[Test]
	[MethodDataSource(typeof(InvalidShipmentServiceData), nameof(ITheoryData<>.GetTestData))]
	[MethodDataSource(typeof(InvalidCountryData), nameof(ITheoryData<>.GetTestData))]
	[MethodDataSource(typeof(InvalidCityData), nameof(ITheoryData<>.GetTestData))]
	[MethodDataSource(typeof(InvalidPhoneData), nameof(ITheoryData<>.GetTestData))]
	[MethodDataSource(typeof(InvalidEmailData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(Theory theory)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(Request(theory), cancellationToken: ct);

		// Assert
		await Assert.That(result.IsValid).IsFalse();
	}

	[Test]
	[MethodDataSource(typeof(InvalidShipmentServiceData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldReturnProperErrors_WhenShipmentServiceIsNotValid(Theory theory)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(Request(theory), cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ShipmentService);
	}

	[Test]
	[MethodDataSource(typeof(InvalidCountryData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(Theory theory)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(Request(theory), cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.Country);
	}

	[Test]
	[MethodDataSource(typeof(InvalidCityData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(Theory theory)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(Request(theory), cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.City);
	}

	[Test]
	[MethodDataSource(typeof(InvalidPhoneData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldReturnProperErrors_WhenPhoneIsNotValid(Theory theory)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(Request(theory), cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Phone);
	}

	[Test]
	[MethodDataSource(typeof(InvalidEmailData), nameof(ITheoryData<>.GetTestData))]
	public async Task Validate_ShouldReturnProperErrors_WhenEmailIsNotValid(Theory theory)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(Request(theory), cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Email);
	}
}
