using CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Edit;

namespace CustomCADs.Modules.Printing.API.Customizations.Endpoints.Put;

public class EditCustomizationEndpoint(IRequestSender sender)
	: Endpoint<EditCustomizationRequest>
{
	public override void Configure()
	{
		Put("");
		Group<CustomizationsGroup>();
		Description(x => x
			.WithSummary("Edit")
			.WithDescription("Edit your Customization")
		);
	}

	public override async Task HandleAsync(EditCustomizationRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new EditCustomizationCommand(
				Id: CustomizationId.New(req.Id),
				Scale: req.Scale,
				Infill: req.Infill,
				Volume: req.Volume,
				Color: req.Color,
				MaterialId: MaterialId.New(req.MaterialId)
			),
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
