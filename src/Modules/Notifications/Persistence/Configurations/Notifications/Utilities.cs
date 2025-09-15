using CustomCADs.Notifications.Domain.Notifications;
using CustomCADs.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Notifications.Persistence.Configurations.Notifications;

public static class Utilities
{
	public static EntityTypeBuilder<Notification> SetPrimaryKey(this EntityTypeBuilder<Notification> builder)
	{
		builder.HasKey(x => x.Id);

		return builder;
	}

	public static EntityTypeBuilder<Notification> SetStronglyTypedIds(this EntityTypeBuilder<Notification> builder)
	{
		builder.Property(x => x.Id)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				x => NotificationId.New(x)
			);

		builder.Property(x => x.AuthorId)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				x => AccountId.New(x)
			);

		builder.Property(x => x.ReceiverId)
			.ValueGeneratedOnAdd()
			.HasConversion(
				x => x.Value,
				x => AccountId.New(x)
			);

		return builder;
	}

	public static EntityTypeBuilder<Notification> SetValueObjects(this EntityTypeBuilder<Notification> builder)
	{
		builder.ComplexProperty(x => x.Content, x =>
		{
			x.Property(x => x.Description)
				.IsRequired()
				.HasColumnName("Description");

			x.Property(x => x.Link)
				.IsRequired(required: false)
				.HasColumnName("Link");
		});

		return builder;
	}

	public static EntityTypeBuilder<Notification> SetValidations(this EntityTypeBuilder<Notification> builder)
	{
		builder.Property(x => x.Type)
			.IsRequired()
			.HasColumnName(nameof(Notification.Type));

		builder.Property(x => x.Status)
			.IsRequired()
			.HasConversion(
				x => x.ToString(),
				x => Enum.Parse<NotificationStatus>(x)
			)
			.HasColumnName(nameof(Notification.Status));

		builder.Property(x => x.CreatedAt)
			.IsRequired()
			.HasColumnName(nameof(Notification.CreatedAt));

		builder.Property(x => x.AuthorId)
			.IsRequired()
			.HasColumnName(nameof(Notification.AuthorId));

		builder.Property(x => x.ReceiverId)
			.IsRequired()
			.HasColumnName(nameof(Notification.ReceiverId));

		return builder;
	}
}
