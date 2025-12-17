using CustomCADs.Modules.Printing.Application.Customizations.Queries.Internal.GetById;

namespace CustomCADs.Modules.Printing.API.Customizations.Endpoints.Get;

public class GetCustomizationEndpoint(IRequestSender sender)
	: Endpoint<GetCustomizationRequest, CustomizationResponse>
{
	public override void Configure()
	{
		Get("{id}");
		Group<CustomizationsGroup>();
		Description(x => x
			.WithSummary("Single")
			.WithDescription("Get a Customization")
		);
	}

	public override async Task HandleAsync(GetCustomizationRequest req, CancellationToken ct)
	{
		CustomizationDto customization = await sender.SendQueryAsync(
			query: new GetCustomizationByIdQuery(
				Id: CustomizationId.New(req.Id)
			),
			ct: ct
		).ConfigureAwait(false);

		CustomizationResponse response = customization.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
