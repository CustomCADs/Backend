using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.Purchase.WithDelivery;

using static Data.Customs.TestData;

public class Validator : Data.Customs.BaseUnitTests
{
	private readonly PurchaseCustomWithDeliveryValidator validator = new();

	[Theory]
	[ClassData(typeof(ValidData))]
	public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			CustomizationId: ValidCustomizationId,
			Count: count,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(InvalidShipmentServiceData))]
	[ClassData(typeof(InvalidCountryData))]
	[ClassData(typeof(InvalidCityData))]
	[ClassData(typeof(InvalidPhoneData))]
	[ClassData(typeof(InvalidEmailData))]
	public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			CustomizationId: ValidCustomizationId,
			Count: count,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command);

		// Assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(InvalidShipmentServiceData))]
	public async Task Validate_ShouldReturnProperErrors_WhenShipmentServiceIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			CustomizationId: ValidCustomizationId,
			Count: count,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ShipmentService);
	}

	[Theory]
	[ClassData(typeof(InvalidCountryData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			CustomizationId: ValidCustomizationId,
			Count: count,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.Country);
	}

	[Theory]
	[ClassData(typeof(InvalidCityData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			CustomizationId: ValidCustomizationId,
			Count: count,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.City);
	}

	[Theory]
	[ClassData(typeof(InvalidPhoneData))]
	public async Task Validate_ShouldReturnProperErrors_WhenPhoneIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			CustomizationId: ValidCustomizationId,
			Count: count,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Phone);
	}

	[Theory]
	[ClassData(typeof(InvalidEmailData))]
	public async Task Validate_ShouldReturnProperErrors_WhenEmailIsNotValid(string paymentMethodId, int count, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange
		PurchaseCustomWithDeliveryCommand command = new(
			Id: ValidId,
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			CustomizationId: ValidCustomizationId,
			Count: count,
			ShipmentService: shipmentService,
			Address: new(country, city, street),
			Contact: new(phone, email)
		);

		// Act
		var result = await validator.TestValidateAsync(command, cancellationToken: ct);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Email);
	}
}
