using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Domain.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	protected static Shipment CreateShipment(
		AccountId? buyerId = null,
		string? service = null,
		string? email = null,
		string? phone = null,
		string? recipient = null,
		int? count = null,
		double? weight = null,
		string? country = null,
		string? city = null,
		string? street = null
	) => Shipment.Create(
		buyerId: buyerId ?? ValidBuyerId,
		service: service ?? ValidService,
		email: email ?? ValidEmail,
		phone: phone ?? ValidPhone,
		recipient: recipient ?? ValidRecipient,
		count: count ?? MinValidCount,
		weight: weight ?? MinValidWeight,
		country: country ?? ValidCountry,
		city: city ?? ValidCity,
		street: street ?? ValidStreet
	);

	protected static Shipment CreateShipmentWithId(
		ShipmentId? id = null,
		AccountId? buyerId = null,
		string? service = null,
		string? email = null,
		string? phone = null,
		string? recipient = null,
		int? count = null,
		double? weight = null,
		string? country = null,
		string? city = null,
		string? street = null
	) => Shipment.CreateWithId(
		id: id ?? ValidId,
		buyerId: buyerId ?? ValidBuyerId,
		service: service ?? ValidService,
		email: email ?? ValidEmail,
		phone: phone ?? ValidPhone,
		recipient: recipient ?? ValidRecipient,
		count: count ?? MinValidCount,
		weight: weight ?? MinValidWeight,
		country: country ?? ValidCountry,
		city: city ?? ValidCity,
		street: street ?? ValidStreet
	);
}
