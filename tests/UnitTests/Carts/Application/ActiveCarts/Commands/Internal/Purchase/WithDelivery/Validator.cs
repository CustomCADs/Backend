using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using CustomCADs.Shared.Application.Dtos.Delivery;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

using static Data.ActiveCarts.TestData;

public class Validator : Data.ActiveCarts.BaseUnitTests
{
	private readonly PurchaseActiveCartWithDeliveryValidator validator = new();
	private static PurchaseActiveCartWithDeliveryCommand Request(string paymentMethodId, string shipmentService, AddressDto address, ContactDto contact)
		=> new(
			PaymentMethodId: paymentMethodId,
			CallerId: ValidBuyerId,
			ShipmentService: shipmentService,
			Address: address,
			Contact: contact
		);

	[Theory]
	[ClassData(typeof(ValidData))]
	public async Task Validate_ShouldBeValid_WhenCartIsValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(
			Request(paymentMethodId, shipmentService, new(country, city, street), new(phone, email)),
			cancellationToken: ct
		);

		// Assert
		Assert.True(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(InvalidShipmentServiceData))]
	[ClassData(typeof(InvalidCountryData))]
	[ClassData(typeof(InvalidCityData))]
	[ClassData(typeof(InvalidPhoneData))]
	[ClassData(typeof(InvalidEmailData))]
	public async Task Validate_ShouldBeInvalid_WhenCartIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(
			Request(paymentMethodId, shipmentService, new(country, city, street), new(phone, email)),
			cancellationToken: ct
		);

		// Assert
		Assert.False(result.IsValid);
	}

	[Theory]
	[ClassData(typeof(InvalidShipmentServiceData))]
	public async Task Validate_ShouldReturnProperErrors_WhenShipmentServiceIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(
			Request(paymentMethodId, shipmentService, new(country, city, street), new(phone, email)),
			cancellationToken: ct
		);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.ShipmentService);
	}

	[Theory]
	[ClassData(typeof(InvalidCountryData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCountryIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(
			Request(paymentMethodId, shipmentService, new(country, city, street), new(phone, email)),
			cancellationToken: ct
		);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.Country);
	}

	[Theory]
	[ClassData(typeof(InvalidCityData))]
	public async Task Validate_ShouldReturnProperErrors_WhenCityIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(
			Request(paymentMethodId, shipmentService, new(country, city, street), new(phone, email)),
			cancellationToken: ct
		);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Address.City);
	}

	[Theory]
	[ClassData(typeof(InvalidPhoneData))]
	public async Task Validate_ShouldReturnProperErrors_WhenPhoneIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(
			Request(paymentMethodId, shipmentService, new(country, city, street), new(phone, email)),
			cancellationToken: ct
		);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Phone);
	}

	[Theory]
	[ClassData(typeof(InvalidEmailData))]
	public async Task Validate_ShouldReturnProperErrors_WhenEmailIsNotValid(string paymentMethodId, string shipmentService, string country, string city, string street, string? phone, string? email)
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(
			Request(paymentMethodId, shipmentService, new(country, city, street), new(phone, email)),
			cancellationToken: ct
		);

		// Assert
		result.ShouldHaveValidationErrorFor(x => x.Contact.Email);
	}
}
