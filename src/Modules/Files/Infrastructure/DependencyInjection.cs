using Amazon.Runtime;
using Amazon.S3;
using CustomCADs.Modules.Files.Application.Contracts;
using CustomCADs.Modules.Files.Infrastructure;
using Microsoft.Extensions.Options;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddStorageService()
			=> services
				.AddSingleton<IAmazonS3>(sp =>
				{
					StorageSettings settings = sp.GetRequiredService<IOptions<StorageSettings>>().Value;

					BasicAWSCredentials credentials = new(settings.AccessKey, settings.SecretKey);
					return new AmazonS3Client(credentials, new AmazonS3Config()
					{
						ServiceURL = settings.Endpoint,
						ForcePathStyle = true,
					});
				})
				.AddScoped<IStorageService, AmazonStorageService>();
	}
}
