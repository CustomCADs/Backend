using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Notifications.Domain.Notifications.ValueObjects;
using CustomCADs.Shared.Domain.Bases.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Notifications.Domain.Notifications;

public class Notification : BaseAggregateRoot
{
	private Notification() { }
	private Notification(
		string type,
		NotificationContent content,
		AccountId authorId,
		AccountId receiverId
		) : this()
	{
		Status = NotificationStatus.Unread;
		CreatedAt = DateTimeOffset.UtcNow;
		Content = content;
		Type = type;
		ReceiverId = receiverId;
		AuthorId = authorId;
	}

	public NotificationId Id { get; init; }
	public string Type { get; private set; } = string.Empty;
	public NotificationContent Content { get; private set; } = new();
	public NotificationStatus Status { get; private set; }
	public DateTimeOffset CreatedAt { get; init; }
	public AccountId AuthorId { get; private set; }
	public AccountId ReceiverId { get; private set; }

	public static Notification Create(
		string type,
		NotificationContent content,
		AccountId authorId,
		AccountId receiverId
	) => new Notification(type, content, authorId, receiverId)
		.ValidateContent()
		.ValidateType();

	public static Notification[] CreateBulk(
		string type,
		NotificationContent content,
		AccountId authorId,
		AccountId[] receiverIds
	)
	{
		Notification[] notifications = [..receiverIds.Select(receiverId =>
			new Notification(type, content, authorId, receiverId)
			{
				Id = NotificationId.New()
			})
		];

		notifications.FirstOrDefault()?.ValidateContent().ValidateType();
		return notifications;
	}

	public static Notification Create(
		NotificationId id,
		string type,
		NotificationContent content,
		AccountId authorId,
		AccountId receiverId
	) => new Notification(type, content, authorId, receiverId)
	{
		Id = id,
	}
	.ValidateContent()
	.ValidateType();

	public Notification SetContent(NotificationContent content)
	{
		Content = content;
		this.ValidateContent();
		return this;
	}

	public Notification Read()
	{
		NotificationStatus newStatus = NotificationStatus.Read;
		if (Status is not NotificationStatus.Unread)
		{
			throw CustomValidationException<Notification>.Status(newStatus, Status);
		}

		Status = newStatus;
		return this;
	}

	public Notification Open()
	{
		NotificationStatus newStatus = NotificationStatus.Opened;
		if (Status is not NotificationStatus.Read)
		{
			throw CustomValidationException<Notification>.Status(newStatus, Status);
		}

		Status = newStatus;
		return this;
	}

	public Notification Hide()
	{
		NotificationStatus newStatus = NotificationStatus.Hidden;
		if (Status is not (NotificationStatus.Unread or NotificationStatus.Read or NotificationStatus.Opened))
		{
			throw CustomValidationException<Notification>.Status(newStatus, Status);
		}

		Status = newStatus;
		return this;
	}

	public Notification Unhide()
	{
		NotificationStatus newStatus = NotificationStatus.Unread;
		if (Status is not NotificationStatus.Hidden)
		{
			throw CustomValidationException<Notification>.Status(newStatus, Status);
		}

		Status = newStatus;
		return this;
	}
}
