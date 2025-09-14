using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;
using CustomCADs.Shared.Endpoints.Extensions;

namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery.Patch.RemoveTag;

using static DomainConstants.Roles;

public class RemoveProductTagEndpoint(IRequestSender sender)
	: Endpoint<RemoveProductTagRequest>
{
	public override void Configure()
	{
		Patch("tags/remove");
		Group<GalleryGroup>();
		Roles(Admin);
		Description(d => d
			.WithSummary("Remove Tag")
			.WithDescription("Removes a Tag from a Product")
		);
	}

	public override async Task HandleAsync(RemoveProductTagRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			new RemoveProductTagCommand(
				Id: ProductId.New(req.Id),
				TagId: TagId.New(req.TagId),
				CallerId: User.GetAccountId()
			),
			ct
		).ConfigureAwait(false);
	}
}
