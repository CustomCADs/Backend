using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Designer.RemoveTag;

namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery.Patch.RemoveTag;

using static DomainConstants.Roles;

public class RemoveProductTagEndpoint(IRequestSender sender)
	: Endpoint<RemoveProductTagRequest>
{
	public override void Configure()
	{
		Patch("tags/remove");
		Group<GalleryGroup>();
		Roles(Admin);
		Description(x => x
			.WithSummary("Remove Tag")
			.WithDescription("Removes a Tag from a Product")
		);
	}

	public override async Task HandleAsync(RemoveProductTagRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new RemoveProductTagCommand(
				Id: ProductId.New(req.Id),
				TagId: TagId.New(req.TagId),
				CallerId: User.AccountId
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
