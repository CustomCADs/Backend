namespace CustomCADs.Catalog.API.Products.Endpoints.Gallery;

using static EndpointsConstants;

public class GalleryGroup : SubGroup<ProductsGroup>
{
	public GalleryGroup()
	{
		Configure(Paths.Gallery, x =>
		{
			x.AllowAnonymous();
			x.Description(x => x.WithTags(Tags[Paths.Gallery]));
		});
	}
}
