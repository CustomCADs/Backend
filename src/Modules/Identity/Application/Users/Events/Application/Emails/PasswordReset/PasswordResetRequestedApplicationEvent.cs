using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.PasswordReset;

public record PasswordResetRequestedApplicationEvent(
	string Email,
	string Endpoint
) : BaseApplicationEvent;
