using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCoords;

public sealed class SetProductCoordsHandler(
	IProductReads reads,
	IRequestSender sender
) : ICommandHandler<SetProductCoordsCommand>
{
	public async Task Handle(SetProductCoordsCommand req, CancellationToken ct = default)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		await sender.SendCommandAsync(
			command: new SetCadCoordsCommand(
				Id: product.CadId,
				CamCoordinates: req.CamCoordinates,
				PanCoordinates: req.PanCoordinates
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
