namespace CustomCADs.Shared.Application.Events.Catalog;

public record UserViewedProductApplicationEvent(
	AccountId AccountId,
	ProductId Id,
	DateTimeOffset ViewedAt
) : BaseApplicationEvent;
