using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetCadUrl.Put;

public sealed class CreatorGetProductCadPresignedUrlPutHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<CreatorGetProductCadPresignedUrlPutQuery, string>
{
	public async Task<string> Handle(CreatorGetProductCadPresignedUrlPutQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		return await sender.SendQueryAsync(
			query: new GetCadPresignedUrlPutByIdQuery(
				Id: product.CadId,
				NewFile: req.NewCad
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
