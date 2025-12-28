using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;
using CustomCADs.Shared.API.Attributes;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Presigned.Put;

public class CadPresignedUrlPutEndpoint(IRequestSender sender) : Endpoint<CadPresignedUrlPutRequest, string>
{
	public override void Configure()
	{
		Post("replace");
		Group<PresignedGroup>();
		Description(x => x
			.WithSummary("Replace")
			.WithDescription("Get a URL to replace your Cad")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(CadPresignedUrlPutRequest req, CancellationToken ct)
	{
		string url = await sender.SendQueryAsync(
			query: new GetCadPresignedUrlPutQuery(
				Id: CadId.New(req.Id),
				File: req.File,
				RelationType: req.RelationType,
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(url).ConfigureAwait(false);
	}
}
