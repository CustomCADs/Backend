namespace CustomCADs.Shared.API;

public static class EndpointsConstants
{
	public const string RateLimitPolicy = "user-based";

	public static class Paths
	{
		public const string Stripe = "stripe";
		public const string ExchangeRates = "exchange-rates";
		public const string Identity = "identity";
		public const string Notifications = "notifications";
		public const string Products = "products";
		public const string Gallery = "gallery";
		public const string ActiveCarts = "carts/active";
		public const string Customizations = "customizations";
		public const string PurchasedCarts = "carts/purchased";
		public const string Shipments = "shipments";
		public const string Customs = "customs";
		public const string Accounts = "accounts";
		public const string Roles = "roles";
		public const string Categories = "categories";
		public const string Materials = "materials";
		public const string Tags = "tags";
		public const string Customer = "customer";
		public const string Creator = "creator";
		public const string Designer = "designer";
		public const string Admin = "admin";
	}

	/// <summary>
	/// 	[Key]: Value -> [Relative Path]: Documentation Tag
	/// </summary>
	public static readonly Dictionary<string, string> Tags = new()
	{
		// Ungrouped
		[Paths.Stripe] = "-1.1. Stripe",
		[Paths.ExchangeRates] = "-1.2. Exchange Rates",

		// AllowAnonymous
		[Paths.Identity] = "0.1. Identity",
		[Paths.Gallery] = "0.2. Product Gallery",
		[Paths.ActiveCarts] = "0.3. Active Carts",
		[Paths.Customizations] = "0.4. Customizations",
		[Paths.Notifications] = "0.5 Notifications",

		// Customers
		[Paths.PurchasedCarts] = "1.1. Purchased Carts",
		[Paths.Shipments] = "1.2. Shipments",
		[$"{Paths.Customs}/{Paths.Customer}"] = "1.3. Customs Collection",

		// Contributors
		[$"{Paths.Products}/{Paths.Creator}"] = "2.1. Product Collection",

		// Designers
		[$"{Paths.Customs}/{Paths.Designer}"] = "3.1. Customs Management",
		[$"{Paths.Products}/{Paths.Designer}"] = "3.2. Products Management",

		// Admins
		[Paths.Accounts] = "4.1. Accounts Dashboard",
		[Paths.Roles] = "4.2. Roles Dashboard",
		[Paths.Categories] = "4.3. Categories Dashboard",
		[Paths.Materials] = "4.4. Materials Dashboard",
		[Paths.Tags] = "4.5. Tags Dashboard",
		[$"{Paths.Customs}/{Paths.Admin}"] = "4.6. Customs Dashboard",
	};
}
