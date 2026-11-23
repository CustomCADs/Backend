using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Domain;
using FluentValidation;

namespace CustomCADs.Modules.Identity.API.Users.Endpoints.Mutations.Post.Register;

using static ApplicationConstants.FluentMessages;
using static DomainConstants.Users;

public class RegisterRequestValidator : Validator<RegisterRequest>
{
	public RegisterRequestValidator()
	{
		RuleFor(x => x.Role)
			.Must(x => x is CustomerRole or ContributorRole).WithMessage("""must be either "Customer" or "Contributor" """);


		RuleFor(x => x)
			.MustAsync(
				async (command, ct) => !await Resolve<IUserService>()
					.GetExistsByUsernameAsync(command.Username)
					.ConfigureAwait(false)
			).WithMessage("Cannot register a User with a Duplicate Username")
			.MustAsync(
				async (command, ct) => !await Resolve<IUserService>()
					.GetExistsByEmailAsync(command.Email)
					.ConfigureAwait(false)
			).WithMessage("Cannot register a User with a Duplicate Email");

		RuleFor(x => x.ConfirmPassword)
			.NotEmpty().WithMessage(RequiredError)
			.Equal(x => x.Password).WithMessage("must be equal to Password!");
	}
}
