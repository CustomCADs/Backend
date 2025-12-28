using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Files.Application.Images.Queries.Internal.GetById;

public sealed class GetImageByIdHandler(
	IImageReads reads,
	BaseCachingService<ImageId, Image> cache,
	IRequestSender sender
) : IQueryHandler<GetImageByIdQuery, ImageDto>
{
	public async Task<ImageDto> Handle(GetImageByIdQuery req, CancellationToken ct = default)
	{
		Image image = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Image>.ById(req.Id)
		).ConfigureAwait(false);

		return image.ToDto(
			ownerName: await sender.SendQueryAsync(
				query: new GetUsernameByIdQuery(image.OwnerId),
				ct: ct
			).ConfigureAwait(false)
		);
	}
}
