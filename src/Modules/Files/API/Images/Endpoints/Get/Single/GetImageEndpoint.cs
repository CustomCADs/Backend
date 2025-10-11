
using CustomCADs.Files.Application.Images.Dtos;
using CustomCADs.Files.Application.Images.Queries.Internal.GetById;

namespace CustomCADs.Files.API.Images.Endpoints.Get.Single;

public class GetImageEndpoint(IRequestSender sender) : Endpoint<GetImageRequest, ImageResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<ImagesGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See your Image in detail")
		);
	}

	public override async Task HandleAsync(GetImageRequest req, CancellationToken ct)
	{
		ImageDto response = await sender.SendQueryAsync(
			query: new GetImageByIdQuery(
				Id: ImageId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(response, cad => cad.ToResponse()).ConfigureAwait(false);
	}
}
