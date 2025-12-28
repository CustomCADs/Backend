namespace CustomCADs.Modules.Files.Infrastructure;

public record StorageSettings(string AccessKey, string SecretKey, string Endpoint, string BucketName)
{
	public StorageSettings() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
}
