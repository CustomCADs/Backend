using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Domain;
using FluentValidation;

namespace CustomCADs.Modules.Identity.API.Identity.Post.Register;

using static ApplicationConstants.FluentMessages;
using static DomainConstants.Users;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(x => x.Role)
			.Must(x => x is CustomerRole or ContributorRole)
			.WithMessage("""Role must be either "Customer" or "Contributor" """);


		RuleFor(x => x.Username)
			.MustAsync(
				async (username, ct) => !await Resolve<IUserService>()
					.GetExistsByUsernameAsync(username)
					.ConfigureAwait(false)
			).WithMessage("Cannot register a User with a Duplicate Username");

		RuleFor(x => x.Email)
			.MustAsync(
				async (email, ct) => !await Resolve<IUserService>()
					.GetExistsByEmailAsync(email)
					.ConfigureAwait(false)
			).WithMessage("Cannot register a User with a Duplicate Email");

		RuleFor(x => x.ConfirmPassword)
			.NotEmpty().WithMessage(RequiredError)
			.Equal(x => x.Password).WithMessage("Passwords must be equal!");
	}
}
