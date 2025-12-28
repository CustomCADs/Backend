using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Modules.Catalog.Application.Products.Queries.Internal.Creator.GetById;

public sealed class CreatorGetProductByIdHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<CreatorGetProductByIdQuery, CreatorGetProductByIdDto>
{
	public async Task<CreatorGetProductByIdDto> Handle(CreatorGetProductByIdQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		return product.ToCreatorGetByIdDto(
			username: await sender.SendQueryAsync(
				query: new GetUsernameByIdQuery(product.CreatorId),
				ct: ct
			).ConfigureAwait(false),
			categoryName: await sender.SendQueryAsync(
				query: new GetCategoryNameByIdQuery(product.CategoryId),
				ct: ct
			).ConfigureAwait(false)
		);
	}
}
