using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Modules.Identity.Application.Users.Events.Application.Emails.EmailVerification;

public record EmailVerificationRequestedApplicationEvent(
	string Email,
	string Endpoint
) : BaseApplicationEvent;
