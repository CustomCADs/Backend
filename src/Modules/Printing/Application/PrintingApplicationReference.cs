using System.Reflection;

namespace CustomCADs.Modules.Printing.Application;

public class PrintingApplicationReference
{
	public static Assembly Assembly => typeof(PrintingApplicationReference).Assembly;
}
