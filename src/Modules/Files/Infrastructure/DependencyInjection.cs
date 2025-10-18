using Amazon.Runtime;
using Amazon.S3;
using CustomCADs.Files.Application.Contracts;
using CustomCADs.Files.Infrastructure;
using Microsoft.Extensions.Options;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddStorageService(this IServiceCollection services)
	{
		services.AddSingleton<IAmazonS3>(sp =>
		{
			StorageSettings settings = sp.GetRequiredService<IOptions<StorageSettings>>().Value;

			AmazonS3Config config = new()
			{
				ServiceURL = settings.Endpoint,
				ForcePathStyle = true,
			};

			BasicAWSCredentials credentials = new(settings.AccessKey, settings.SecretKey);
			return new AmazonS3Client(credentials, config);
		});
		services.AddScoped<IStorageService, AmazonStorageService>();
	}
}
