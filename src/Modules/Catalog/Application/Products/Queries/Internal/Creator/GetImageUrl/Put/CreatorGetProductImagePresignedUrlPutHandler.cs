using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Creator.GetImageUrl.Put;

public sealed class CreatorGetProductImagePresignedUrlPutHandler(IProductReads reads, IRequestSender sender)
	: IQueryHandler<CreatorGetProductImagePresignedUrlPutQuery, string>
{
	public async Task<string> Handle(CreatorGetProductImagePresignedUrlPutQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		return await sender.SendQueryAsync(
			query: new GetImagePresignedUrlPutByIdQuery(
				Id: product.ImageId,
				NewFile: req.NewImage
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
