using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

public sealed class GetAllCustomsHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<GetAllCustomsQuery, Result<GetAllCustomsDto>>
{
	public async Task<Result<GetAllCustomsDto>> Handle(GetAllCustomsQuery req, CancellationToken ct)
	{
		Result<Custom> result = await reads.AllAsync(
			query: new(
				ForDelivery: req.ForDelivery,
				CustomStatus: req.CustomStatus,
				CustomerId: req.CustomerId,
				DesignerId: req.DesignerId,
				CategoryId: req.CategoryId,
				Name: req.Name,
				Sorting: req.Sorting,
				Pagination: req.Pagination
			),
			track: false,
			ct: ct
		).ConfigureAwait(false);

		AccountId[] buyerIds = [.. result.Items.Select(x => x.BuyerId)];
		Dictionary<AccountId, string> buyers = await sender.SendQueryAsync(
			query: new GetUsernamesByIdsQuery(buyerIds),
			ct: ct
		).ConfigureAwait(false);

		AccountId[] designerIds = [.. result.Items
			.Where(x => x.AcceptedCustom is not null)
			.Select(x => x.AcceptedCustom!.DesignerId)
		];
		Dictionary<AccountId, string> designers = await sender.SendQueryAsync(
			query: new GetUsernamesByIdsQuery(designerIds),
			ct: ct
		).ConfigureAwait(false);

		CategoryId[] categoryIds = [.. result.Items
			.Where(x => x.Category is not null)
			.Select(x => x.Category!.Id)
		];
		Dictionary<CategoryId, string> categories = await sender.SendQueryAsync(
			query: new GetCategoryNamesByIdsQuery(categoryIds),
			ct: ct
		).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToGetAllDto(
			buyerName: buyers[x.BuyerId],
			designerName: x.AcceptedCustom is null ? null : designers[x.AcceptedCustom.DesignerId],
			categoryName: x.Category is null ? null : categories[x.Category.Id]
		));
	}
}
