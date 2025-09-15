using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetAll;

public sealed class CreatorGetAllProductsHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<CreatorGetAllProductsQuery, Result<CreatorGetAllProductsDto>>
{
	public async Task<Result<CreatorGetAllProductsDto>> Handle(CreatorGetAllProductsQuery req, CancellationToken ct)
	{
		Result<Product> result = await reads.AllAsync(
			query: new(
				CategoryId: req.CategoryId,
				CreatorId: req.CallerId,
				TagIds: req.TagIds,
				Name: req.Name,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		CategoryId[] categoryIds = [.. result.Items.Select(x => x.CategoryId).Distinct()];
		Dictionary<CategoryId, string> categories = await sender.SendQueryAsync(
			query: new GetCategoryNamesByIdsQuery(categoryIds),
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToCreatorGetAllDto(categoryName: categories[x.CategoryId]));
	}
}
