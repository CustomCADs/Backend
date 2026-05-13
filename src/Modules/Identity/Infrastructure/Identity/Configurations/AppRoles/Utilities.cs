using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Configurations.AppRoles;

internal static class Utilities
{
	extension(EntityTypeBuilder<AppRole> builder)
	{
		internal EntityTypeBuilder<AppRole> SetSeeding()
		{
			builder.HasData([
				CreateAppRole(
					id: Constants.Roles.CustomerId,
					name: Shared.Domain.DomainConstants.Users.CustomerRole,
					concurrencyStamp: "51da1b9f-803c-4bd3-9a00-da7ac259ce32"
				),
				CreateAppRole(
					id: Constants.Roles.ContributorId,
					name: Shared.Domain.DomainConstants.Users.ContributorRole,
					concurrencyStamp: "a1a170e0-ee84-4afe-afd9-1df57009f291"
				),
				CreateAppRole(
					id: Constants.Roles.DesignerId,
					name: Shared.Domain.DomainConstants.Users.DesignerRole,
					concurrencyStamp: "1a8ba0a7-4853-42da-980d-3107784e7ab1"
				),
				CreateAppRole(
					id: Constants.Roles.AdminId,
					name: Shared.Domain.DomainConstants.Users.AdminRole,
					concurrencyStamp: "42174679-32f1-48b0-9524-0f00791ec760"
				),
			]);

			return builder;

			static AppRole CreateAppRole(Guid id, string name, string concurrencyStamp)
				=> new(name)
				{
					NormalizedName = name.ToUpperInvariant(),
					Id = id,
					ConcurrencyStamp = concurrencyStamp,
				};
		}
	}

}
