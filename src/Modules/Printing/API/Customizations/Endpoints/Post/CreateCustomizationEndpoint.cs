using CustomCADs.Modules.Printing.API.Customizations.Endpoints.Get;
using CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Create;
using CustomCADs.Modules.Printing.Application.Customizations.Queries.Internal.GetById;

namespace CustomCADs.Modules.Printing.API.Customizations.Endpoints.Post;

public class CreateCustomizationEndpoint(IRequestSender sender)
	: Endpoint<CreateCustomizationRequest, CustomizationResponse>
{
	public override void Configure()
	{
		Post("");
		Group<CustomizationsGroup>();
		Description(x => x
			.WithSummary("Create")
			.WithDescription("Create a Customization")
		);
	}

	public override async Task HandleAsync(CreateCustomizationRequest req, CancellationToken ct)
	{
		CustomizationId customizationId = await sender.SendCommandAsync(
			command: new CreateCustomizationCommand(
				Scale: req.Scale,
				Infill: req.Infill,
				Volume: req.Volume,
				Color: req.Color,
				MaterialId: MaterialId.New(req.MaterialId)
			),
			ct: ct
		).ConfigureAwait(false);

		CustomizationDto customization = await sender.SendQueryAsync(
			query: new GetCustomizationByIdQuery(customizationId)
		, ct).ConfigureAwait(false);

		CustomizationResponse response = customization.ToResponse();
		await Send.CreatedAtAsync<GetCustomizationEndpoint>(new { id = customizationId.Value }, response).ConfigureAwait(false);
	}
}
