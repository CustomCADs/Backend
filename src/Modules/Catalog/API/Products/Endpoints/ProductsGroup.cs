namespace CustomCADs.Modules.Catalog.API.Products.Endpoints;

using static APIConstants;

public class ProductsGroup : Group
{
	public ProductsGroup()
	{
		Configure(Paths.Products, _ => { });
	}
}
