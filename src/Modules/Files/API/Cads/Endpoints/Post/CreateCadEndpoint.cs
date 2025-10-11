using CustomCADs.Files.API.Cads.Endpoints.Get.Single;
using CustomCADs.Files.Application.Cads.Commands.Internal.Create;
using CustomCADs.Files.Application.Cads.Dtos;
using CustomCADs.Files.Application.Cads.Queries.Internal.GetById;

namespace CustomCADs.Files.API.Cads.Endpoints.Post;

public class CreateCadEndpoint(IRequestSender sender) : Endpoint<CreateCadRequest, CadResponse>
{
	public override void Configure()
	{
		Post("");
		Group<CadsGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Create a CAD")
		);
	}

	public override async Task HandleAsync(CreateCadRequest req, CancellationToken ct)
	{
		CadId id = await sender.SendCommandAsync(
			command: new CreateCadCommand(
				Key: req.Key,
				ContentType: req.ContentType,
				Volume: req.Volume,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		CadDto response = await sender.SendQueryAsync(
			query: new GetCadByIdQuery(id),
			ct: ct
		).ConfigureAwait(false);

		await Send.CreatedAtAsync<GetCadEndpoint>(new { id = id.Value }, response.ToResponse()).ConfigureAwait(false);
	}
}
