using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using CustomCADs.Modules.Delivery.Domain.Shipments.ValueObjects;
using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Delivery.Domain.Shipments;

public class Shipment : BaseAggregateRoot
{
	private Shipment() { }
	private Shipment(
		AccountId buyerId,
		ShipmentAddress address,
		ShipmentInfo info,
		ShipmentReference reference,
		ShipmentContact contact
	)
	{
		Status = ShipmentStatus.Awaiting;
		RequestedAt = DateTimeOffset.UtcNow;
		BuyerId = buyerId;
		Address = address;
		Info = info;
		Reference = reference;
		Contact = contact;
	}

	public ShipmentId Id { get; private set; }
	public DateTimeOffset RequestedAt { get; private set; }
	public ShipmentStatus Status { get; private set; }
	public ShipmentReference Reference { get; private set; } = new();
	public ShipmentAddress Address { get; private set; } = new();
	public ShipmentContact Contact { get; private set; } = new();
	public ShipmentInfo Info { get; private set; } = new();
	public AccountId BuyerId { get; private set; }

	public static Shipment Create(
		AccountId buyerId,
		string service,
		string? email,
		string? phone,
		string recipient,
		int count,
		double weight,
		string country,
		string city,
		string street
	) => new Shipment(
		buyerId: buyerId,
		address: new ShipmentAddress() with { Country = country, City = city, Street = street },
		info: new ShipmentInfo() with { Count = count, Recipient = recipient, Weight = weight },
		reference: new ShipmentReference() with { Service = service },
		contact: new ShipmentContact() with { Email = email, Phone = phone }
	)
	.ValidateService()
	.ValidateEmail()
	.ValidatePhone()
	.ValidateRecipient()
	.ValidateCount()
	.ValidateWeight()
	.ValidateCountry()
	.ValidateCity()
	.ValidateStreet();

	public static Shipment CreateWithId(
		ShipmentId id,
		AccountId buyerId,
		string service,
		string? email,
		string? phone,
		string recipient,
		int count,
		double weight,
		string country,
		string city,
		string street
	) => new Shipment(
		buyerId: buyerId,
		address: new ShipmentAddress() with { Country = country, City = city, Street = street },
		info: new ShipmentInfo() with { Count = count, Recipient = recipient, Weight = weight },
		reference: new ShipmentReference() with { Service = service },
		contact: new ShipmentContact() with { Email = email, Phone = phone }
	)
	{ Id = id }
	.ValidateService()
	.ValidateEmail()
	.ValidatePhone()
	.ValidateRecipient()
	.ValidateCount()
	.ValidateWeight()
	.ValidateCountry()
	.ValidateCity()
	.ValidateStreet();

	public Shipment Activate(string referenceId)
	{
		ShipmentStatus newStatus = ShipmentStatus.Active;
		if (Status is not ShipmentStatus.Awaiting)
		{
			throw CustomValidationException<Shipment>.Status(newStatus, this.Status);
		}
		Status = newStatus;

		Reference = Reference with { Id = referenceId };
		this.ValidateReference();

		return this;
	}

	public Shipment Cancel()
	{
		ShipmentStatus newStatus = ShipmentStatus.Cancelled;
		if (Status is not (ShipmentStatus.Awaiting or ShipmentStatus.Active))
		{
			throw CustomValidationException<Shipment>.Status(newStatus, this.Status);
		}
		Status = newStatus;

		return this;
	}

	public Shipment Deliver()
	{
		ShipmentStatus newStatus = ShipmentStatus.Delivered;
		if (Status is not ShipmentStatus.Active)
		{
			throw CustomValidationException<Shipment>.Status(newStatus, this.Status);
		}
		Status = newStatus;

		return this;
	}
}
