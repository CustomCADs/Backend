using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using FluentValidation;

namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;

public sealed class ChangeUsernameValidator : CommandValidator<ChangeUsernameCommand>
{
	public ChangeUsernameValidator(IRequestSender sender)
	{
		RuleFor(x => x)
			.MustAsync(async (command, ct) =>
			{
				bool usernameIsDuplicate = await sender.SendQueryAsync(
					query: new GetAccountExistsByUsernameQuery(command.Username),
					ct: ct
				).ConfigureAwait(false);

				return !usernameIsDuplicate;
			})
			.WhenAsync(async (command, ct) =>
			{
				try
				{
					var info = await sender.SendQueryAsync(
						query: new GetAccountInfoByUsernameQuery(command.Username),
						ct: ct
					).ConfigureAwait(false);

					// Don't run if the account with this username is the user's own
					return info.Id != command.Id;
				}
				catch (Exception ex) when (ex.GetType().IsGenericType && ex.GetType().GetGenericTypeDefinition() == typeof(CustomNotFoundException<>))
				{
					// Don't run if the account with this username doesn't exist
					return false;
				}
			})
			.WithMessage("Username already taken");
	}
}
