namespace CustomCADs.Modules.Identity.Infrastructure;

public static class Constants
{
	public static class Roles
	{
		public static readonly Guid CustomerId = new("762ddec2-25c9-4183-9891-72a19d84a839");
		public static readonly Guid ContributorId = new("e1101e2c-32cc-456f-9c82-4f1d1a65d141");
		public static readonly Guid DesignerId = new("f3ad41d3-ee90-4988-9195-8b2a8f4f2733");
		public static readonly Guid AdminId = new("fad1b19d-5333-4633-bd84-d67c64649f65");
	}

	public static class Users
	{
		public static readonly Guid CustomerId = new("e38c495f-b1f3-4226-d289-08dd11623eb9");
		public static readonly Guid ContributorId = new("af840410-f3f2-4a3b-d28a-08dd11623eb9");
		public static readonly Guid DesignerId = new("a8145f5f-a3a4-4f06-9461-9f24b9f23fde");
		public static readonly Guid HeadDesignerId = new("4337a774-2c5c-4c27-d28b-08dd11623eb9");
		public static readonly Guid AdminId = new("cb7749fb-3fff-4902-d28c-08dd11623eb9");
	}
}
