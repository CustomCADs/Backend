using CustomCADs.Modules.Delivery.Domain.Shipments;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Data.Shipments;

using static TestData;

public partial class BaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

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
		string? street = null,
		ShipmentId? id = null
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
