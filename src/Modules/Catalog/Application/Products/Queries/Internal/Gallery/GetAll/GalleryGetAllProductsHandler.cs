using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetAll;

public sealed class GalleryGetAllProductsHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<GalleryGetAllProductsQuery, Result<GalleryGetAllProductsDto>>
{
	public async Task<Result<GalleryGetAllProductsDto>> Handle(GalleryGetAllProductsQuery req, CancellationToken ct)
	{
		Result<Product> result = await reads.AllAsync(
			query: new(
				CategoryId: req.CategoryId,
				TagIds: req.TagIds,
				Name: req.Name,
				Status: ProductStatus.Validated,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		Dictionary<ProductId, string[]> tags = await reads.TagsByIdsAsync([.. result.Items.Select(x => x.Id)], ct).ConfigureAwait(false);

		AccountId[] userIds = [.. result.Items.Select(x => x.CreatorId).Distinct()];
		Dictionary<AccountId, string> users = await sender.SendQueryAsync(
				new GetUsernamesByIdsQuery(userIds),
				ct: ct
			).ConfigureAwait(false);

		CategoryId[] categoryIds = [.. result.Items.Select(x => x.CategoryId).Distinct()];
		Dictionary<CategoryId, string> categories = await sender.SendQueryAsync(
			query: new GetCategoryNamesByIdsQuery(categoryIds),
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToGalleryGetAllDto(
			username: users[x.CreatorId],
			categoryName: categories[x.CategoryId],
			tags: tags[x.Id]
		));
	}
}
