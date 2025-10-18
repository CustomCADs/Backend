using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Identity.Application.Users.Commands.Internal.SSO.Register;

public sealed class SingleSignOnUserHandler(
	IUserService service,
	ITokenService tokenService,
	IRequestSender sender
) : ICommandHandler<SingleSignOnUserCommand, TokensDto>
{
	public async Task<TokensDto> Handle(SingleSignOnUserCommand req, CancellationToken ct)
	{
		User user = await GetUserAsync(req, ct).ConfigureAwait(false);

		RefreshToken rt = tokenService.IssueRefreshToken(
			createRefreshToken: (token) => user.AddRefreshToken(token, longerSession: false)
		);
		await service.SaveRefreshTokensAsync(user).ConfigureAwait(false);

		return tokenService.IssueTokens(user, rt);
	}

	private async Task<User> GetUserAsync(SingleSignOnUserCommand req, CancellationToken ct)
	{
		bool duplicateUsername = await service.GetExistsByUsernameAsync(req.Username).ConfigureAwait(false);
		bool duplicateEmail = await service.GetExistsByEmailAsync(req.Email).ConfigureAwait(false);

		return (duplicateUsername, duplicateEmail) switch
		{
			(false, false) => await CreateUser(req, ct).ConfigureAwait(false),
			(true, false) => await service.GetByUsernameAsync(req.Username).ConfigureAwait(false),
			(false, true) => await service.GetByEmailAsync(req.Email).ConfigureAwait(false),
			(true, true) => await service.GetByUsernameAsync(req.Username).ConfigureAwait(false),
		};

		async Task<User> CreateUser(SingleSignOnUserCommand req, CancellationToken ct)
		{
			string role = req.Role ?? DomainConstants.Roles.Customer;
			AccountId accountId = await sender.SendCommandAsync(
				command: new CreateAccountCommand(
					Role: role,
					Username: req.Username,
					Email: req.Email,
					FirstName: null,
					LastName: null
				),
				ct: ct
			).ConfigureAwait(false);

			await service.CreateSSOAsync(
				user: User.Create(
					role: role,
					username: req.Username,
					email: new(req.Email, IsVerified: true),
					accountId: accountId
				),
				provider: req.Provider
			).ConfigureAwait(false);

			return await service.GetByUsernameAsync(req.Username).ConfigureAwait(false);
		}
	}
}
