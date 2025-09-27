namespace CustomCADs.Catalog.API.Products.Endpoints;

using static EndpointsConstants;

public class ProductsGroup : Group
{
	public ProductsGroup()
	{
		Configure(Paths.Products, _ => { });
	}
}
