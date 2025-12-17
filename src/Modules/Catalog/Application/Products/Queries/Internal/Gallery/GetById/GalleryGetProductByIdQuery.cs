using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

public sealed record GalleryGetProductByIdQuery(
	ProductId Id,
	AccountId CallerId
) : IQuery<GalleryGetProductByIdDto>;
