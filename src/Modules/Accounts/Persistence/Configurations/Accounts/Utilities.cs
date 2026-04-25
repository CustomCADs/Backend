using CustomCADs.Modules.Accounts.Domain.Accounts;
using CustomCADs.Shared.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Accounts.Persistence.Configurations.Accounts;

using static AccountConstants;
using static DomainConstants.Users;

internal static class Utilities
{
	extension(EntityTypeBuilder<Account> builder)
	{
		internal EntityTypeBuilder<Account> SetPrimaryKey()
		{
			builder.HasKey(x => x.Id);

			return builder;
		}

		internal EntityTypeBuilder<Account> SetStronglyTypedIds()
		{
			builder.Property(x => x.Id)
				.ValueGeneratedOnAdd()
				.HasConversion(
					x => x.Value,
					x => AccountId.New(x)
				);

			return builder;
		}

		internal EntityTypeBuilder<Account> SetIndexes()
		{
			builder.HasIndex(x => x.Username).IsUnique();
			builder.HasIndex(x => x.Email).IsUnique();

			return builder;
		}

		internal EntityTypeBuilder<Account> SetValidations()
		{
			builder.Property(x => x.Username)
				.IsRequired()
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Account.Username));

			builder.Property(x => x.Email)
				.IsRequired()
				.HasColumnName(nameof(Account.Email));

			builder.Property(x => x.FirstName)
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Account.FirstName));

			builder.Property(x => x.LastName)
				.HasMaxLength(NameMaxLength)
				.HasColumnName(nameof(Account.LastName));

			builder.Property(x => x.RoleName)
				.IsRequired()
				.HasColumnName(nameof(Account.RoleName));

			builder.Property(x => x.TrackViewedProducts)
				.IsRequired()
				.HasColumnName(nameof(Account.TrackViewedProducts));

			builder.Property(x => x.CreatedAt)
				.IsRequired()
				.HasColumnName(nameof(Account.CreatedAt));

			builder
				.Property(x => x.IsDeleted)
				.IsRequired()
				.HasColumnName(nameof(Account.IsDeleted));

			builder
				.Property(x => x.DeletedAt)
				.HasColumnName(nameof(Account.DeletedAt));

			return builder;
		}

		internal EntityTypeBuilder<Account> SetSeeding()
		{
			builder.HasData([
				Account.CreateWithId(CustomerAccountId, CustomerRole, CustomerUsername, CustomerEmail, new DateTimeOffset(2025, 05, 10, 19, 23, 12, 123, TimeSpan.FromHours(3))),
				Account.CreateWithId(ContributorAccountId, ContributorRole, ContributorUsername, ContributorEmail, new DateTimeOffset(2025, 05, 13, 17, 42, 57, 456, TimeSpan.FromHours(3))),
				Account.CreateWithId(DesignerAccountId, DesignerRole, DesignerUsername, DesignerEmail, new DateTimeOffset(2025, 01, 09, 13, 15, 28, 789, TimeSpan.FromHours(3))),
				Account.CreateWithId(HeadDesignerAccountId, DesignerRole, HeadDesignerUsername, HeadDesignerEmail, new DateTimeOffset(2024, 03, 17, 02, 17, 32, 789, TimeSpan.FromHours(3))),
				Account.CreateWithId(AdminAccountId, AdminRole, AdminUsername, AdminEmail, new DateTimeOffset(2024, 03, 17, 02, 45, 13, 000, TimeSpan.FromHours(3))),
			]);

			return builder;
		}

		internal EntityTypeBuilder<Account> SetFilters()
		{
			builder.HasQueryFilter(x => !x.IsDeleted);

			return builder;
		}
	}
}
