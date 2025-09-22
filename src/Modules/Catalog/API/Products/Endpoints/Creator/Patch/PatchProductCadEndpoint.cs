using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCoords;
using CustomCADs.Shared.API.Extensions;
using System.ComponentModel;

namespace CustomCADs.Catalog.API.Products.Endpoints.Creator.Patch;

public sealed class PatchProductCadEndpoint(IRequestSender sender)
	: Endpoint<PatchProductCadRequest>
{
	public override void Configure()
	{
		Patch("");
		Group<CreatorGroup>();
		Description(x => x
			.WithSummary("Set Cad Coordinates")
			.WithDescription("Set the Coordinates of your Product's Cad")
		);
	}

	public override async Task HandleAsync(PatchProductCadRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			req.Type switch
			{
				CoordinateType.Cam => new SetProductCoordsCommand(
					Id: ProductId.New(req.Id),
					CallerId: User.GetAccountId(),
					CamCoordinates: req.Coordinates
				),
				CoordinateType.Pan => new SetProductCoordsCommand(
					Id: ProductId.New(req.Id),
					CallerId: User.GetAccountId(),
					PanCoordinates: req.Coordinates
				),
				_ => throw new InvalidEnumArgumentException("Coordinate Type must be Cam or Pan"),
			},
			ct: ct
		).ConfigureAwait(false);

		await Send.NoContentAsync().ConfigureAwait(false);
	}
}
