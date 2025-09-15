namespace CustomCADs.Catalog.Endpoints.Products.Endpoints.Gallery;

using static EndpointsConstants;

public class GalleryGroup : Group
{
	public GalleryGroup()
	{
		Configure(Paths.ProductsGallery, x =>
		{
			x.AllowAnonymous();
			x.Description(x => x.WithTags(Tags[Paths.ProductsGallery]));
		});
	}
}
