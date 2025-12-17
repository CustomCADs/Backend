using CustomCADs.Shared.Application;
using CustomCADs.Shared.Domain;
using FluentValidation;

namespace CustomCADs.Modules.Identity.API.Identity.Post.Register;

using static ApplicationConstants.FluentMessages;
using static DomainConstants;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(x => x.Role)
			.Must(x => x is Roles.Customer or Roles.Contributor);

		RuleFor(x => x.ConfirmPassword)
			.NotEmpty().WithMessage(RequiredError)
			.Equal(x => x.Password).WithMessage("Passwords must be equal!");
	}
}
