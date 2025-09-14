using CustomCADs.Shared.Application;
using CustomCADs.Shared.Domain;
using FluentValidation;

namespace CustomCADs.Identity.Endpoints.Identity.Post.Register;

using static DomainConstants;
using static ApplicationConstants.FluentMessages;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(r => r.Role)
			.Must(r => r is Roles.Customer or Roles.Contributor);

		RuleFor(r => r.ConfirmPassword)
			.NotEmpty().WithMessage(RequiredError)
			.Equal(r => r.Password).WithMessage("Passwords must be equal!");
	}
}
