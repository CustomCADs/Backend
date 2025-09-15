using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Shared.Domain;

namespace CustomCADs.Accounts.Domain.Accounts;

using static AccountConstants;
using static DomainConstants;

public static class Validations
{
	public static Account ValidateRole(this Account account)
		=> account
			.ThrowIfNull(
				expression: (x) => x.RoleName,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: (x) => x.RoleName,
				length: (RoleConstants.NameMinLength, RoleConstants.NameMaxLength)
			);

	public static Account ValidateUsername(this Account account)
		=> account
			.ThrowIfNull(
				expression: (x) => x.Username,
				predicate: string.IsNullOrWhiteSpace
			)
			.ThrowIfInvalidLength(
				expression: (x) => x.Username,
				length: (NameMinLength, NameMaxLength)
			);

	public static Account ValidateEmail(this Account account)
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

	public static Account ValidateFirstName(this Account account)
		=> account.FirstName is null
			? account
			: account
				.ThrowIfInvalidLength(
					expression: (x) => x.FirstName!,
					length: (NameMinLength, NameMaxLength)
				);

	public static Account ValidateLastName(this Account account)
		=> account.LastName is null
			? account
			: account
				.ThrowIfInvalidLength(
					expression: (x) => x.LastName!,
					length: (NameMinLength, NameMaxLength)
				);
}
