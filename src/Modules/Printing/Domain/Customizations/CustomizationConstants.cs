using System.Text.RegularExpressions;

namespace CustomCADs.Modules.Printing.Domain.Customizations;

public static partial class CustomizationConstants
{
	public const decimal InfillMin = 0.2m;
	public const decimal InfillMax = 1m;

	public const decimal VolumeMin = 0;
	public const decimal VolumeMax = 14_500_000; // 14.5 dm³

	public const decimal BaseMin = 10m / 1000m; // 0.01
	public const decimal BaseMax = 10m / 0.1m; // 100

	public const decimal ScaleMin = 1m;
	public const decimal ScaleMax = 100m;

	public static Regex Color => ColorRegex();

	[GeneratedRegex(@"^#[0-9a-fA-F]{6}$", RegexOptions.Compiled)]
	private static partial Regex ColorRegex();
}
