namespace CustomCADs.Shared.Application.Events.Account.Accounts;

public record AccountDeletedApplicationEvent(
	AccountId Id
) : BaseApplicationEvent;
