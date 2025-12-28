using CustomCADs.Modules.Accounts.Domain.Roles;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Modules.Accounts.Domain.Accounts;

using static AccountConstants;
using static DomainConstants;

internal static class Validations
{
	extension(Account account)
	{
		internal Account ValidateRole()
			=> account
				.ThrowIfNull(
					expression: (x) => x.RoleName,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: (x) => x.RoleName,
					length: (RoleConstants.NameMinLength, RoleConstants.NameMaxLength)
				);

		internal Account ValidateUsername()
			=> account
				.ThrowIfNull(
					expression: (x) => x.Username,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfInvalidLength(
					expression: (x) => x.Username,
					length: (NameMinLength, NameMaxLength)
				);

		internal Account ValidateEmail()
			=> account
				.ThrowIfNull(
					expression: (x) => x.Email,
					predicate: string.IsNullOrWhiteSpace
				)
				.ThrowIfPredicateIsFalse(
					expression: (x) => x.Email,
					predicate: Regexes.Email.IsMatch,
					message: "An {0} must have a proper {1}."
				);

		internal Account ValidateFirstName()
			=> account.FirstName is null
				? account
				: account
					.ThrowIfInvalidLength(
						expression: (x) => x.FirstName,
						length: (NameMinLength, NameMaxLength)
					);

		internal Account ValidateLastName()
			=> account.LastName is null
				? account
				: account
					.ThrowIfInvalidLength(
						expression: (x) => x.LastName,
						length: (NameMinLength, NameMaxLength)
					);
	}

}
