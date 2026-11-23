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
		ShipmentDelivery delivery,
		ShipmentInfo info,
		ShipmentReference reference,
		ShipmentContact contact
	) : this()
	{
		Status = ShipmentStatus.Awaiting;
		RequestedAt = DateTimeOffset.UtcNow;
		BuyerId = buyerId;
		Delivery = delivery;
		Info = info;
		Reference = reference;
		Contact = contact;
	}

	public ShipmentId Id { get; private set; }
	public DateTimeOffset RequestedAt { get; private set; }
	public ShipmentStatus Status { get; private set; }
	public ShipmentReference Reference { get; private set; } = new();
	public ShipmentDelivery Delivery { get; private set; } = new();
	public ShipmentContact Contact { get; private set; } = new();
	public ShipmentInfo Info { get; private set; } = new();
	public AccountId BuyerId { get; private set; }

	public static Shipment Create(
		AccountId buyerId,
		ShipmentDelivery delivery,
		string service,
		string email,
		string phone,
		string recipient,
		int count,
		double weight
	) => new Shipment(
		buyerId: buyerId,
		delivery: delivery,
		info: new ShipmentInfo(Count: count, Recipient: recipient, Weight: weight),
		reference: new ShipmentReference(Id: null, Service: service),
		contact: new ShipmentContact(Email: email, Phone: phone)
	)
	.ValidateService()
	.ValidateEmail()
	.ValidatePhone()
	.ValidateRecipient()
	.ValidateCount()
	.ValidateWeight();

	internal static Shipment CreateWithId(
		ShipmentId id,
		AccountId buyerId,
		ShipmentDelivery delivery,
		string service,
		string email,
		string phone,
		string recipient,
		int count,
		double weight
	) => new Shipment(
		buyerId: buyerId,
		delivery: delivery,
		info: new ShipmentInfo(Count: count, Recipient: recipient, Weight: weight),
		reference: new ShipmentReference(Id: null, Service: service),
		contact: new ShipmentContact(Email: email, Phone: phone)
	)
	{ Id = id }
	.ValidateService()
	.ValidateEmail()
	.ValidatePhone()
	.ValidateRecipient()
	.ValidateCount()
	.ValidateWeight();

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
