using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;
using System.Text.RegularExpressions;

namespace CustomCADs.Modules.Identity.Domain;

public static partial class Constants
{
	public static class Tokens
	{
		public const int JwtDurationInMinutes = 15;
		public const int RtDurationInDays = 7;
		public const int LongerRtDurationInDays = 15;
	}
}
