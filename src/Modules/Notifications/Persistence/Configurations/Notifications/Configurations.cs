using CustomCADs.Notifications.Domain.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Notifications.Persistence.Configurations.Notifications;

public class Configurations : IEntityTypeConfiguration<Notification>
{
	public void Configure(EntityTypeBuilder<Notification> builder)
	   => builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValueObjects()
			.SetValidations()
		;
}
