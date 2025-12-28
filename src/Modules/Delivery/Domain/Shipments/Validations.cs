using CustomCADs.Shared.Domain;

namespace CustomCADs.Modules.Delivery.Domain.Shipments;

using static ShipmentConstants;

internal static class Validations
{
	extension(Shipment shipment)
	{
		internal Shipment ValidateReference()
			=> shipment
				.ThrowIfNull(
					expression: x => x.Reference.Id,
					predicate: string.IsNullOrWhiteSpace
				);

		internal Shipment ValidateService()
			=> shipment
				.ThrowIfNull(
					expression: x => x.Reference.Service,
					predicate: string.IsNullOrWhiteSpace
				);

		internal Shipment ValidateEmail()
			=> shipment
				.ThrowIfPredicateIsFalse(
					expression: x => x.Contact.Email,
					predicate: x => x is null || DomainConstants.Regexes.Email.IsMatch(x),
					message: "Invalid Email"
				);

		internal Shipment ValidatePhone()
			=> shipment
				.ThrowIfPredicateIsFalse(
					expression: x => x.Contact.Phone,
					predicate: x => x is null || DomainConstants.Regexes.Phone.IsMatch(x),
					message: "Invalid Phone"
				);

		internal Shipment ValidateRecipient()
			=> shipment
				.ThrowIfNull(
					expression: x => x.Info.Recipient,
					predicate: string.IsNullOrWhiteSpace
				);

		internal Shipment ValidateCount()
			=> shipment
				.ThrowIfInvalidRange(
					expression: x => x.Info.Count,
					range: (MinCount, MaxCount)
				);

		internal Shipment ValidateWeight()
			=> shipment
				.ThrowIfInvalidRange(
					expression: x => x.Info.Weight,
					range: (MinWeight, MaxWeight),
					inclusive: true
				);

		internal Shipment ValidateCountry()
			=> shipment
				.ThrowIfNull(
					expression: x => x.Address.Country,
					predicate: string.IsNullOrWhiteSpace
				);

		internal Shipment ValidateCity()
			=> shipment
				.ThrowIfNull(
					expression: x => x.Address.City,
					predicate: string.IsNullOrWhiteSpace
				);

		internal Shipment ValidateStreet()
			=> shipment
				.ThrowIfNull(
					expression: x => x.Address.Street,
					predicate: string.IsNullOrWhiteSpace
				);
	}

}
