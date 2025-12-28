namespace CustomCADs.Modules.Accounts.Domain.Roles;

using static RoleConstants;

internal static class Validations
{
	extension(Role role)
	{
		internal Role ValidateName()
			=> role
				.ThrowIfNull(
					expression: x => x.Name,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: x => x.Name,
					length: (NameMinLength, NameMaxLength)
				);

		internal Role ValidateDescription()
			=> role
				.ThrowIfNull(
					expression: x => x.Description,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: x => x.Description,
					length: (DescriptionMinLength, DescriptionMaxLength)
				);
	}

}
