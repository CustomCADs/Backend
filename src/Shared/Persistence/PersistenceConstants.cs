namespace CustomCADs.Shared.Persistence;

public static class PersistenceConstants
{
	public const string ConnectionStringKey = "ApplicationConnection";
	public const string MigrationsTable = "__EFMigrationsHistory";

	public static class Schemes
	{
		public const string Accounts = "Accounts";
		public const string Carts = "Carts";
		public const string Catalog = "Catalog";
		public const string Customs = "Customs";
		public const string Delivery = "Delivery";
		public const string Files = "Files";
		public const string Idempotency = "Idempotency";
		public const string Notifications = "Notifications";
		public const string Printing = "Printing";
	}
}
