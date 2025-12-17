using CustomCADs.Modules.Accounts.Domain.Roles;
using CustomCADs.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Accounts.Persistence.Configurations.Roles;

using static DomainConstants.Roles;
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
				Role.CreateWithId(RoleId.New(1), Customer, CustomerDescription),
				Role.CreateWithId(RoleId.New(2), Contributor, ContributorDescription),
				Role.CreateWithId(RoleId.New(3), Designer, DesignerDescription),
				Role.CreateWithId(RoleId.New(4), Admin, AdminDescription),
			]);

			return builder;
		}
	}
}
