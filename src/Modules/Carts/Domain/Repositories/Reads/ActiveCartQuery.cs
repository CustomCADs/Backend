using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Modules.Carts.Domain.Repositories.Reads;

public record ActiveCartQuery(
	Pagination Pagination,
	ProductId? ProductId = null
);
