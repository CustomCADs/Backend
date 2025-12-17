using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments;

using static ShipmentsData;

public class ShipmentsBaseUnitTests
{
	protected static readonly CancellationToken ct = CancellationToken.None;

	public static Shipment CreateShipment(
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

	public static Shipment CreateShipmentWithId(
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

	protected static ShipmentTrackDto[] CreateShipmentTracksDtos(int count = 4, string message = "Message")
		=> [..
			Enumerable.Range(1, count).Select(i => new ShipmentTrackDto(
				DateTime: DateTimeOffset.UtcNow.AddSeconds(i),
				Place: null,
				IsDelivered: false,
				Message: message + i
			))
		];
}
