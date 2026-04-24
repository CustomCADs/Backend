using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

public sealed class ResetPasswordEmailValidator : CommandValidator<ResetPasswordEmailCommand>
{
	public ResetPasswordEmailValidator(IUserService service)
	{
		RuleFor(x => x.Email)
			.MustAsync(
				async (email, ct) => !await service.GetIsSSOByEmailAsync(email).ConfigureAwait(false)
			)
			.WithMessage("Accounts with SSO have no Password to Reset.");
	}
}
