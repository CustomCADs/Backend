namespace CustomCADs.Shared.Application.Events.Catalog;

public record ProductViewedApplicationEvent(
	ProductId Id,
	AccountId AccountId,
	DateTimeOffset ViewedAt
) : BaseApplicationEvent;
