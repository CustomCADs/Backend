using System.Reflection;

namespace CustomCADs.Modules.Catalog.Application;

public class CatalogApplicationReference
{
	public static Assembly Assembly => typeof(CatalogApplicationReference).Assembly;
}
