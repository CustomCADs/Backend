using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Designer.GetById;

public sealed class DesignerGetProductByIdHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<DesignerGetProductByIdQuery, DesignerGetProductByIdDto>
{
	public async Task<DesignerGetProductByIdDto> Handle(DesignerGetProductByIdQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		return product.ToDesignerGetByIdDto(
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
