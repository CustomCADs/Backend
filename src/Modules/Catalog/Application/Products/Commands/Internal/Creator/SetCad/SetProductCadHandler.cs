using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetCad;

public sealed class SetProductCadHandler(
	IProductReads reads,
	IRequestSender sender
) : ICommandHandler<SetProductCadCommand>
{
	public async Task Handle(SetProductCadCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		await sender.SendCommandAsync(
			command: new EditCadCommand(
				Id: product.CadId,
				ContentType: req.ContentType,
				Volume: req.Volume
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
