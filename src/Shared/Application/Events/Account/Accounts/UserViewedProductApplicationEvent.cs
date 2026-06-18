namespace CustomCADs.Shared.Application.Events.Account.Accounts;

public record UserViewedProductApplicationEvent(
	ProductId Id,
	AccountId AccountId,
	DateTimeOffset ViewedAt
) : BaseApplicationEvent;
