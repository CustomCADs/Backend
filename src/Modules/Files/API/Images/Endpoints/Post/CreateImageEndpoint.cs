using CustomCADs.Files.API.Images.Endpoints.Get.Single;
using CustomCADs.Files.Application.Images.Commands.Internal.Create;
using CustomCADs.Files.Application.Images.Dtos;
using CustomCADs.Files.Application.Images.Queries.Internal.GetById;

namespace CustomCADs.Files.API.Images.Endpoints.Post;

public class CreateImageEndpoint(IRequestSender sender) : Endpoint<CreateImageRequest, ImageResponse>
{
	public override void Configure()
	{
		Post("");
		Group<ImagesGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Create a Image")
		);
	}

	public override async Task HandleAsync(CreateImageRequest req, CancellationToken ct)
	{
		ImageId id = await sender.SendCommandAsync(
			command: new CreateImageCommand(
				GeneratedKey: req.GeneratedKey,
				ContentType: req.ContentType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		ImageDto response = await sender.SendQueryAsync(
			query: new GetImageByIdQuery(id),
			ct: ct
		).ConfigureAwait(false);

		await Send.CreatedAtAsync<GetImageEndpoint>(new { id = id.Value }, response.ToResponse()).ConfigureAwait(false);
	}
}
