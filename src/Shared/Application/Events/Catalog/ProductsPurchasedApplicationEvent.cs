namespace CustomCADs.Shared.Application.Events.Catalog;

public sealed record ProductsPurchasedApplicationEvent(
	ProductId[] Ids
) : BaseApplicationEvent;
