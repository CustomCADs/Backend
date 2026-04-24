using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;
using System.Text.RegularExpressions;

namespace CustomCADs.Shared.Domain;

public static partial class DomainConstants
{
	public partial class Regexes
	{
		public static Regex Email => EmailRegex();
		public static Regex Phone => PhoneRegex();

		[GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled)]
		private static partial Regex EmailRegex();

		[GeneratedRegex(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled)]
		private static partial Regex PhoneRegex();
	}

	public static class Tags
	{
		public static readonly TagId NewId = TagId.New("6813c4b9-bcde-4f95-a1ce-8e545756c8a4");
		public static readonly TagId ProfessionalId = TagId.New("e67f88d5-330a-414d-b45d-32c6806725ab");
		public static readonly TagId PrintableId = TagId.New("38deab9b-8791-4147-9958-64e9f7ec6d78");
		public static readonly TagId PopularId = TagId.New("9a35cbea-806c-4561-ae71-bb21824f2432");

		public const string New = "New";
		public const string Professional = "Professional";
		public const string Printable = "Printable";
		public const string Popular = "Popular";
	}

	public static class Textures
	{
		public static readonly ImageId PLA = ImageId.New(Guid.Parse("9a35cbea-806c-4561-ae71-bb21824f2432"));
		public static readonly ImageId ABS = ImageId.New(Guid.Parse("bed27a31-107a-4b3f-a50a-cb9cc6f376f1"));
		public static readonly ImageId GlowInDark = ImageId.New(Guid.Parse("190a69a3-1b02-43f0-a4f9-cab22826abf3"));
		public static readonly ImageId TUF = ImageId.New(Guid.Parse("38deab9b-8791-4147-9958-64e9f7ec6d78"));
		public static readonly ImageId Wood = ImageId.New(Guid.Parse("3fe2472c-d2c6-434c-a013-ef117319bed3"));
	}

	public static class Users
	{
		public static readonly AccountId CustomerAccountId = AccountId.New("2da61b05-1a27-4af9-9df2-be4f1f4e835f");
		public static readonly AccountId ContributorAccountId = AccountId.New("6d963818-23dc-4e9a-aaa8-b4c77252bc97");
		public static readonly AccountId DesignerAccountId = AccountId.New("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1");
		public static readonly AccountId AdminAccountId = AccountId.New("e995039c-a535-4f20-8288-7aadcb71b252");

		public static readonly string[] Roles = [CustomerRole, ContributorRole, DesignerRole, AdminRole];
		public const string CustomerRole = "Customer";
		public const string ContributorRole = "Contributor";
		public const string DesignerRole = "Designer";
		public const string AdminRole = "Administrator";

		public const string CustomerUsername = "For7a7a";
		public const string ContributorUsername = "PDMatsaliev20";
		public const string DesignerUsername = "Oracle3000";
		public const string AdminUsername = "NinjataBG";

		public const string CustomerEmail = "ivanzlatinov006@gmail.com";
		public const string ContributorEmail = "PDMatsaliev20@codingburgas.bg";
		public const string DesignerEmail = "boriskolev2006@gmail.com";
		public const string AdminEmail = "ivanangelov414@gmail.com";
	}
}
