using CustomCADs.Catalog.Application.Products.Events.Application.ProductViewed;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Catalog.Application.Products.Queries.Internal.Gallery.GetById;

public sealed class GalleryGetProductByIdHandler(IProductReads reads, IRequestSender sender, IEventRaiser raiser)
	: IQueryHandler<GalleryGetProductByIdQuery, GalleryGetProductByIdDto>
{
	public async Task<GalleryGetProductByIdDto> Handle(GalleryGetProductByIdQuery req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.Status is not ProductStatus.Validated)
		{
			throw CustomStatusException<Product>.ById(req.Id);
		}

		if (!req.CallerId.IsEmpty())
		{
			await raiser.RaiseApplicationEventAsync(
				@event: new ProductViewedApplicationEvent(
					Id: req.Id,
					AccountId: req.CallerId
				)
			).ConfigureAwait(false);
		}

		return product.ToGalleryGetByIdDto(
			username: await sender.SendQueryAsync(
				query: new GetUsernameByIdQuery(product.CreatorId),
				ct: ct
			).ConfigureAwait(false),
			categoryName: await sender.SendQueryAsync(
				query: new GetCategoryNameByIdQuery(product.CategoryId),
				ct: ct
			).ConfigureAwait(false),
			tags: await reads.TagsByIdAsync(req.Id, ct).ConfigureAwait(false)
		);
	}
}
