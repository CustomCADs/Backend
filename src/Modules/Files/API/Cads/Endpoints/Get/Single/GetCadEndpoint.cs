
using CustomCADs.Modules.Files.Application.Cads.Dtos;
using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.GetById;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Get.Single;

public class GetCadEndpoint(IRequestSender sender) : Endpoint<GetCadRequest, CadResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<CadsGroup>();
		AllowAnonymous();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("See your Cad in detail")
		);
	}

	public override async Task HandleAsync(GetCadRequest req, CancellationToken ct)
	{
		CadDto response = await sender.SendQueryAsync(
			query: new GetCadByIdQuery(
				Id: CadId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(response, cad => cad.ToResponse()).ConfigureAwait(false);
	}
}
