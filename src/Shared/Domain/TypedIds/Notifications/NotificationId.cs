using CustomCADs.Shared.Domain.Bases.Id;
using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Shared.Domain.TypedIds.Notifications;

public readonly struct NotificationId : IEntityId<Guid>
{
	public NotificationId() : this(Guid.Empty) { }
	private NotificationId(Guid value)
	{
		Value = value;
	}

	public Guid Value { get; init; }
	public bool IsEmpty() => Value == Guid.Empty;

	public static NotificationId New() => new(Guid.NewGuid());
	public static NotificationId New(Guid id) => new(id);

	public override bool Equals([NotNullWhen(true)] object? obj)
		=> obj is NotificationId id && this == id;

	public override int GetHashCode()
		=> Value.GetHashCode();

	public override string ToString()
		=> Value.ToString();

	public static bool operator ==(NotificationId left, NotificationId right)
		=> left.Value == right.Value;

	public static bool operator !=(NotificationId left, NotificationId right)
		=> !(left == right);
}
