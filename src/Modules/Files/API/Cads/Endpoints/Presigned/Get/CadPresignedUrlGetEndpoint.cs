using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.Application.Dtos.Files;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Presigned.Get;

public class CadPresignedUrlGetEndpoint(IRequestSender sender) : Endpoint<CadPresignedUrlGetRequest, DownloadFileResponse>
{
	public override void Configure()
	{
		Post("download");
		Group<PresignedGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Download")
			.WithDescription("Get a URL to download your Cad")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(CadPresignedUrlGetRequest req, CancellationToken ct)
	{
		DownloadFileResponse response = await sender.SendQueryAsync(
			query: new GetCadPresignedUrlGetQuery(
				Id: CadId.New(req.Id),
				RelationType: req.RelationType,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
