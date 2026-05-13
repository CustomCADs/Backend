namespace CustomCADs.Modules.Accounts.Domain;

public static class Constants
{
	public static class Roles
	{
		public static readonly RoleId CustomerId = RoleId.New(1);
		public static readonly RoleId ContributorId = RoleId.New(2);
		public static readonly RoleId DesignerId = RoleId.New(3);
		public static readonly RoleId AdminId = RoleId.New(4);

		public const string CustomerDescription = "Can buy Products from the Gallery as Cart Items; Can request Customs from our Designers and contact them; Can download purchased CADs and track requested Shipments.";
		public const string ContributorDescription = "Can upload 3D Models to the Gallery as Products; Can sell CADs to our Designers and contact them; Can apply to become a Designer himself.";
		public const string DesignerDescription = "Can accept and work on Customers' Customs; Can validate or report Contributors' Products; Can do everything a Contributor can do.";
		public const string AdminDescription = "Can access all non-sensitive info from all resources; Can ban reported resources - Customs, Products, Users, ...; Can modify Categories and Roles.";
	}
}
