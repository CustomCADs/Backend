using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetAll;

public sealed class DesignerGetAllProductsHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<DesignerGetAllProductsQuery, Result<DesignerGetAllProductsDto>>
{
	public async Task<Result<DesignerGetAllProductsDto>> Handle(DesignerGetAllProductsQuery req, CancellationToken ct)
	{
		Result<Product> result = await reads.AllAsync(
			query: new(
				CategoryId: req.CategoryId,
				DesignerId: null,
				Status: req.Status,
				TagIds: req.TagIds,
				Name: req.Name,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		AccountId[] userIds = [.. result.Items.Select(x => x.CreatorId).Distinct()];
		Dictionary<AccountId, string> users = await sender.SendQueryAsync(
			query: new GetUsernamesByIdsQuery(userIds),
			ct: ct
		).ConfigureAwait(false);

		CategoryId[] categoryIds = [.. result.Items.Select(x => x.CategoryId).Distinct()];
		Dictionary<CategoryId, string> categories = await sender.SendQueryAsync(
			query: new GetCategoryNamesByIdsQuery(categoryIds),
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToDesignerGetAllDto(
			username: users[x.CreatorId],
			categoryName: categories[x.CategoryId]
		));
	}
}
