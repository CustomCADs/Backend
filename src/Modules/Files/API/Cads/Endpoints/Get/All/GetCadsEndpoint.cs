
using CustomCADs.Modules.Files.Application.Cads.Dtos;
using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.GetAll;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Get.All;

public class GetCadsEndpoint(IRequestSender sender) : Endpoint<GetCadsRequest, Result<CadResponse>>
{
	public override void Configure()
	{
		Get("");
		Group<CadsGroup>();
		Description(x => x
			.WithSummary("All")
			.WithDescription("See all your Cads")
		);
	}

	public override async Task HandleAsync(GetCadsRequest req, CancellationToken ct)
	{
		Result<CadDto> response = await sender.SendQueryAsync(
			query: new GetAllCadsQuery(
				OwnerId: User.AccountId,
				Pagination: new(Page: req.Page, Limit: req.Limit)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.MappedAsync(response, x => x.ToResponse()).ConfigureAwait(false);
	}
}
