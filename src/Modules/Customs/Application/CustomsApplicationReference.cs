using System.Reflection;

namespace CustomCADs.Modules.Customs.Application;

public class CustomsApplicationReference
{
	public static Assembly Assembly => typeof(CustomsApplicationReference).Assembly;
}
