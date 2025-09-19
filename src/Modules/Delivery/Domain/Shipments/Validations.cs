using CustomCADs.Shared.Domain;

namespace CustomCADs.Delivery.Domain.Shipments;

using static ShipmentConstants;

public static class Validations
{
	public static Shipment ValidateService(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Reference.Service,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Shipment ValidateEmail(this Shipment shipment)
		=> shipment
			.ThrowIfPredicateIsFalse(
				expression: x => x.Contact.Email,
				predicate: x => x is null || DomainConstants.Regexes.Email.IsMatch(x),
				message: "Invalid Email"
			);

	public static Shipment ValidatePhone(this Shipment shipment)
		=> shipment
			.ThrowIfPredicateIsFalse(
				expression: x => x.Contact.Phone,
				predicate: x => x is null || DomainConstants.Regexes.Phone.IsMatch(x),
				message: "Invalid Phone"
			);

	public static Shipment ValidateRecipient(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Info.Recipient,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Shipment ValidateCount(this Shipment shipment)
		=> shipment
			.ThrowIfInvalidRange(
				expression: x => x.Info.Count,
				range: (MinCount, MaxCount),
				inclusive: true
			);

	public static Shipment ValidateWeight(this Shipment shipment)
		=> shipment
			.ThrowIfInvalidRange(
				expression: x => x.Info.Weight,
				range: (MinWeight, MaxWeight),
				inclusive: true
			);

	public static Shipment ValidateCountry(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Address.Country,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Shipment ValidateCity(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Address.City,
				predicate: string.IsNullOrWhiteSpace
			);

	public static Shipment ValidateStreet(this Shipment shipment)
		=> shipment
			.ThrowIfNull(
				expression: x => x.Address.Street,
				predicate: string.IsNullOrWhiteSpace
			);
}
