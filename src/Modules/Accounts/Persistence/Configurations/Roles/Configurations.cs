using CustomCADs.Modules.Accounts.Domain.Roles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Accounts.Persistence.Configurations.Roles;

public class Configurations : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder
			.SetPrimaryKey()
			.SetStronglyTypedIds()
			.SetValidations()
			.SetSeeding();
	}
}
