using CustomCADs.Modules.Accounts.Domain.Roles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Accounts.Persistence.Configurations.Roles;

using static RoleConstants;

internal static class Utilities
{
	extension(EntityTypeBuilder<Role> builder)
	{
		internal EntityTypeBuilder<Role> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Role> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.HasConversion(
					x => x.Value,
					x => RoleId.New(x)
				)
				.UseIdentityColumn();

			return builder;
		}

		internal EntityTypeBuilder<Role> SetValidations()
		{
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Role.Name));

			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(DescriptionMaxLength)
				.HasColumnName(nameof(Role.Description));

			return builder;
		}

		internal EntityTypeBuilder<Role> SetSeeding()
		{
			builder.HasData([
				Role.CreateWithId(
					id: Domain.Constants.Roles.CustomerId,
					name: Shared.Domain.DomainConstants.Users.CustomerRole,
					description: Domain.Constants.Roles.CustomerDescription
				),
				Role.CreateWithId(
					id: Domain.Constants.Roles.ContributorId,
					name: Shared.Domain.DomainConstants.Users.ContributorRole,
					description: Domain.Constants.Roles.ContributorDescription
				),
				Role.CreateWithId(
					id: Domain.Constants.Roles.DesignerId,
					name: Shared.Domain.DomainConstants.Users.DesignerRole,
					description: Domain.Constants.Roles.DesignerDescription
				),
				Role.CreateWithId(
					id: Domain.Constants.Roles.AdminId,
					name: Shared.Domain.DomainConstants.Users.AdminRole,
					description: Domain.Constants.Roles.AdminDescription
				),
			]);

			return builder;
		}
	}
}
