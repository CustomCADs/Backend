using CustomCADs.Catalog.Application.Products.Commands.Internal.Designer.AddTag;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery.Patch.AddTag;

using static DomainConstants.Roles;

public class AddProductTagEndpoint(IRequestSender sender)
	: Endpoint<AddProductTagRequest>
{
	public override void Configure()
	{
		Patch("tags/add");
		Group<GalleryGroup>();
		Roles(Admin);
		Description(x => x
			.WithSummary("Add Tag")
			.WithDescription("Adds a Tag to a Product")
		);
	}

	public override async Task HandleAsync(AddProductTagRequest req, CancellationToken ct)
	{
		await sender.SendCommandAsync(
			command: new AddProductTagCommand(
				Id: ProductId.New(req.Id),
				TagId: TagId.New(req.TagId),
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
