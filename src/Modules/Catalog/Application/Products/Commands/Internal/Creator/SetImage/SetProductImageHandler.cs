using CustomCADs.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Commands;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.SetImage;

public sealed class SetProductImageHandler(
	IProductReads reads,
	IRequestSender sender
) : ICommandHandler<SetProductImageCommand>
{
	public async Task Handle(SetProductImageCommand req, CancellationToken ct)
	{
		Product product = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Product>.ById(req.Id);

		if (product.CreatorId != req.CallerId)
		{
			throw CustomAuthorizationException<Product>.ById(req.Id);
		}

		await sender.SendCommandAsync(
			command: new EditImageCommand(
				Id: product.ImageId,
				ContentType: req.ContentType
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
