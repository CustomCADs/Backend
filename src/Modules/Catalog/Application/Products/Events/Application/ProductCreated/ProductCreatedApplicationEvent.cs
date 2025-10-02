using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Catalog.Application.Products.Events.Application.ProductCreated;

public sealed record ProductCreatedApplicationEvent(
	ProductId Id,
	TagId[] TagIds
) : BaseApplicationEvent;
