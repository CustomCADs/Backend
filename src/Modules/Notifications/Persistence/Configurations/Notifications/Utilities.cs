using CustomCADs.Modules.Notifications.Domain.Notifications;
using CustomCADs.Modules.Notifications.Domain.Notifications.Enums;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Notifications.Persistence.Configurations.Notifications;

internal static class Utilities
{
	extension(EntityTypeBuilder<Notification> builder)
	{
		internal EntityTypeBuilder<Notification> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Notification> SetStronglyTypedIds()
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

		internal EntityTypeBuilder<Notification> SetValueObjects()
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

		internal EntityTypeBuilder<Notification> SetValidations()
		{
			builder.Property(x => x.Type)
				.IsRequired()
				.HasColumnName(nameof(Notification.Type));

			builder.Property(x => x.Status)
				.IsRequired()
				.HasColumnName(nameof(Notification.Status))
				.HasConversion(
					x => x.ToString(),
					x => Enum.Parse<NotificationStatus>(x)
				);

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

}
