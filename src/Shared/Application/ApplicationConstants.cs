namespace CustomCADs.Shared.Application;

public static class ApplicationConstants
{
	public static class FluentMessages
	{
		public const string RequiredError = "{PropertyName} is required";
		public const string LengthError = "{PropertyName} length must be between {MinLength} and {MaxLength} characters";
		public const string MinimumError = "{PropertyName} length must be more than {MinLength} characters";
		public const string RangeError = "{PropertyName} must be between {From} and {To}";
		public const string EmailError = "Invalid Email";
		public const string PhoneError = "Invalid Phone";
	}

	public static class Cads
	{
		public const string StlContentType = "model/stl";
		public const string GlbContentType = "model/gltf-binary";
		public const string GltfContentType = "model/gltf+json";

		public static readonly string[] PrintableContentTypes = [
			StlContentType,
		];
	}

	public static class Pages
	{
		public const string ConfirmEmailPage = "{0}/confirm-email?username={1}&token={2}";
		public const string ResetPasswordPage = "{0}/reset-password?email={1}&token={2}";
	}

	public static class Notifications
	{
		public static class Messages
		{
			public const string ProductEdited = "Product '{0}', which you've added to your Cart, has been edited!";
			public const string ProductDeleted = "Product '{0}', which you've added to your Cart, has been deleted!";
			public const string ProductValidated = "Your Product has been validated by {0}!";
			public const string ProductReported = "Your Product has been reported by {0}!";
			public const string ProductRemoved = "Product '{0}' has been removed!";
			public const string ProductTagAdded = "Your Product has been assigned a new Tag!";
			public const string ProductTagRemoved = "Your Product has had a Tag revoked!";
			public const string CustomCreated = "A new Custom 3D Model has been requested!";
			public const string CustomEdited = "A Custom 3D Model Request has been edited!";
			public const string CustomDeleted = "A Custom 3D Model Request has been deleted!";
			public const string CustomToggledDelivery = "A Custom 3D Model Request's delivery has been turned {0}!";
			public const string CustomAccepted = "Your Custom 3D Model Request has been accepted by {0}!";
			public const string CustomReported = "Your Custom 3D Model Request has been reported by {0}!";
			public const string CustomRemoved = "A Custom 3D Model Request has been removed by an Admin!";
			public const string CustomBegun = "Your Custom 3D Model Request has been begun!";
			public const string CustomCanceled = "Your Custom 3D Model Request has been canceled!";
			public const string CustomFinished = "Your Custom 3D Model Request has been finished!";
			public const string CustomCompleted = "The Custom 3D Model Request: {0} from: {1} has been completed!";
			public const string CartPurchased = "Your Cart has been purchased!";
			public const string PaymentCompleted = "Your Payment has been completed successfully!";
			public const string PaymentFailed = "Your Payment has failed!";
		}

		public static class Links
		{
			public const string ProductEdited = "/gallery/{0}";
			public const string ProductDeleted = "/cart";
			public const string? ProductValidated = null;
			public const string? ProductReported = null;
			public const string? ProductRemoved = null;
			public const string? ProductTagAdded = null;
			public const string? ProductTagRemoved = null;
			public const string? CustomToggledDelivery = null;
			public const string? CustomCreated = null;
			public const string? CustomEdited = null;
			public const string? CustomDeleted = null;
			public const string? CustomAccepted = null;
			public const string? CustomReported = null;
			public const string? CustomRemoved = null;
			public const string? CustomBegun = null;
			public const string? CustomCanceled = null;
			public const string? CustomFinished = null;
			public const string? CustomCompleted = null;
			public const string? CartPurchased = null;
			public const string PaymentCompleted = null;
			public const string? PaymentFailed = null;
		}
	}
}
