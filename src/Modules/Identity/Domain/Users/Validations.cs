using CustomCADs.Shared.Domain;

namespace CustomCADs.Modules.Identity.Domain.Users;

using static DomainConstants;
using static UserConstants;

internal static class Validations
{
	extension(User user)
	{
		internal User ValidateRole()
			=> user
				.ThrowIfNull(
					expression: (x) => x.Role,
					predicate: string.IsNullOrWhiteSpace
				);

		internal User ValidateUsername()
			=> user
				.ThrowIfNull(
					expression: (x) => x.Username,
					predicate: string.IsNullOrWhiteSpace
				).ThrowIfInvalidLength(
					expression: (x) => x.Username,
					length: (UsernameMinLength, UsernameMaxLength)
				);

		internal User ValidateEmail()
			=> user
				.ThrowIfNull(
					expression: (x) => x.Email.Value,
					predicate: string.IsNullOrWhiteSpace
				).ThrowIfPredicateIsFalse(
					expression: (x) => x.Email.Value,
					predicate: Regexes.Email.IsMatch,
					message: "A {0} must have a proper {1}."
				);
	}

}
