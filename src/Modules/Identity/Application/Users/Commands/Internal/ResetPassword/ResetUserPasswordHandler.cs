namespace CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ResetPassword;

public sealed class ResetUserPasswordHandler(IUserService service)
	: ICommandHandler<ResetUserPasswordCommand>
{
	public async Task Handle(ResetUserPasswordCommand req, CancellationToken ct)
		=> await service.ResetPasswordAsync(req.Email, req.Token, req.NewPassword).ConfigureAwait(false);
}
