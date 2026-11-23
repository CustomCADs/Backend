namespace CustomCADs.Modules.Catalog.API.Products.Endpoints.Gallery;

using static APIConstants;

public class GalleryGroup : SubGroup<ProductsGroup>
{
	public GalleryGroup()
	{
		Configure(Paths.Gallery, x =>
		{
			x.Description(x => x.WithTags(Tags[Paths.Gallery]));
		});
	}
}
