using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery.Data;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery.ToAddress;

using static UnitTests.Customs.Data.Customs.TestData;

public class Validator : UnitTests.Customs.Data.Customs.BaseUnitTests
{
	private readonly PurchaseCustomWithDeliveryValidator validator = new();
	private static PurchaseCustomWithDeliveryCommand Request(Theory theory)
		=> new(
			Id: ValidId,
			Count: theory.Count,
			CustomizationId: ValidCustomizationId,
			PaymentMethodId: theory.PaymentMethodId,
			ShipmentService: theory.ShipmentService,
			CallerId: ValidBuyerId,
			Address: new(string.Empty, string.Empty, string.Empty, new(0, 0)),
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
