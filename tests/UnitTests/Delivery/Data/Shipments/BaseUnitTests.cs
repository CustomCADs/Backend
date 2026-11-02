using CustomCADs.Modules.Delivery.Domain.Shipments;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Delivery;

namespace CustomCADs.UnitTests.Delivery.Data.Shipments;

using static TestData;

public partial class BaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	protected static Shipment CreateShipment(
		AccountId? buyerId = null,
		ShipmentDelivery? delivery = null,
		string service = ValidService,
		string email = ValidEmail,
		string phone = ValidPhone,
		string recipient = ValidRecipient,
		int count = MinValidCount,
		double weight = MinValidWeight,
		ShipmentId? id = null
	) => Shipment.CreateWithId(
		id: id ?? ValidId,
		buyerId: buyerId ?? ValidBuyerId,
		delivery: delivery ?? new(DeliveryDestination.Office, null, 10),
		service: service,
		email: email,
		phone: phone,
		recipient: recipient,
		count: count,
		weight: weight
	);
}
