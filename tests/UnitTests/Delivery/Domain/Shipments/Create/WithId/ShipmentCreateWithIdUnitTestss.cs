using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.WithId;

using static ShipmentsData;

public class ShipmentCreateWithIdUnitTestss : ShipmentsBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowExcepion_WhenShipmentIsValid()
	{
		CreateShipmentWithId();
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties_WhenShipmentIsValid()
	{
		var shipment = CreateShipmentWithId();

		Assert.Multiple(
			() => Assert.Equal(ValidBuyerId, shipment.BuyerId),
			() => Assert.Equal(new(ValidCountry, ValidCity, ValidStreet), shipment.Address),
			() => Assert.Equal(new(MinValidCount, MinValidWeight, ValidRecipient), shipment.Info),
			() => Assert.Equal(new(null, ValidService), shipment.Reference),
			() => Assert.Equal(new(ValidPhone, ValidEmail), shipment.Contact),
			() => Assert.True(DateTimeOffset.UtcNow - shipment.RequestedAt < TimeSpan.FromSeconds(1))
		);
	}

	[Theory]
	[ClassData(typeof(Data.ShipmentCreateInvalidData))]
	public void CreateWithId_ShouldThrowException_WhenInvalid(
		string service,
		string? email,
		string? phone,
		string recipient,
		int count,
		double weight,
		string country,
		string city,
		string street
	)
	{
		Assert.Throws<CustomValidationException<Shipment>>(
			() => CreateShipmentWithId(
				service: service,
				email: email,
				phone: phone,
				recipient: recipient,
				count: count,
				weight: weight,
				country: country,
				city: city,
				street: street
			)
		);
	}
}
