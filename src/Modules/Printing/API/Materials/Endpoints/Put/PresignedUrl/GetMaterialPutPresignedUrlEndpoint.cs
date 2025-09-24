using CustomCADs.Printing.Application.Materials.Queries.Internal.GetTextureUrl.Put;
using CustomCADs.Shared.API.Attributes;
using Microsoft.AspNetCore.Builder;

namespace CustomCADs.Printing.API.Materials.Endpoints.Put.PresignedUrl;

public sealed class GetMaterialPutPresignedUrlEndpoint(IRequestSender sender)
	: Endpoint<GetMaterialPutPresignedUrlRequest, string>
{
	public override void Configure()
	{
		Post("presignedUrls/replace");
		Group<MaterialsGroup>();
		Description(x => x
			.WithSummary("Change Texture")
			.WithDescription("Change your Material's Texture")
			.WithMetadata(new SkipIdempotencyAttribute())
		);
	}

	public override async Task HandleAsync(GetMaterialPutPresignedUrlRequest req, CancellationToken ct)
	{
		string url = await sender.SendQueryAsync(
			query: new GetMaterialTexturePresignedUrlPutQuery(
				Id: MaterialId.New(req.Id),
				NewImage: req.File
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(url).ConfigureAwait(false);
	}
}
