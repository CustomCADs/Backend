using CustomCADs.Files.Application.Cads.Queries.Internal.PresignedUrls.Post;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Files.API.Cads.Endpoints.Presigned.Post;

public class CadPresignedUrlPostEndpoint(IRequestSender sender) : Endpoint<CadPresignedUrlPostRequest, UploadFileResponse>
{
	public override void Configure()
	{
		Post("upload");
		Group<PresignedGroup>();
		Description(x => x
			.WithSummary("Upload")
			.WithDescription("Get a URL to upload your Cad")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(CadPresignedUrlPostRequest req, CancellationToken ct)
	{
		UploadFileResponse response = await sender.SendQueryAsync(
			query: new GetCadPresignedUrlPostQuery(
				Name: req.Name,
				File: req.File,
				RelationType: req.RelationType,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
