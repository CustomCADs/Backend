using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.ToAddress;
using CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.ToAddress;

using static Carts.Data.ActiveCarts.TestData;

public class Validator : Carts.Data.ActiveCarts.BaseUnitTests
{
	private readonly PurchaseActiveCartWithDeliveryToAddressValidator validator = new();
	private static PurchaseActiveCartWithDeliveryToAddressCommand Request(Theory theory)
		=> new(
			CallerId: ValidBuyerId,
			PaymentMethodId: theory.PaymentMethodId,
			ShipmentService: theory.ShipmentService,
			Address: new(string.Empty, string.Empty, string.Empty, Coordinates: new(0, 0)),
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
