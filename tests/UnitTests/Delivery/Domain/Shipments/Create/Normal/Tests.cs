using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments.Create.Normal;

using static Data.Shipments.TestData;

public class Tests : Data.Shipments.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowExcepion_WhenShipmentIsValid()
	{
		Shipment.Create(
			buyerId: ValidBuyerId,
			service: ValidService,
			email: ValidEmail,
			phone: ValidPhone,
			recipient: ValidRecipient,
			count: MinValidCount,
			weight: MinValidWeight,
			country: ValidCountry,
			city: ValidCity,
			street: ValidStreet
		);
	}

	[Fact]
	public void Create_ShouldPopulateProperties_WhenShipmentIsValid()
	{
		Shipment shipment = Shipment.Create(
			buyerId: ValidBuyerId,
			service: ValidService,
			email: ValidEmail,
			phone: ValidPhone,
			recipient: ValidRecipient,
			count: MinValidCount,
			weight: MinValidWeight,
			country: ValidCountry,
			city: ValidCity,
			street: ValidStreet
		);

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
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenInvalid(
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
			() => Shipment.Create(
				buyerId: ValidBuyerId,
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
