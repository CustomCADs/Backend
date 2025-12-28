using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Modules.Files.Persistence;
using CustomCADs.Modules.Files.Persistence.Repositories;
using CustomCADs.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static PersistenceConstants;
using CadReads = CustomCADs.Modules.Files.Persistence.Repositories.Cads.Reads;
using ImageReads = CustomCADs.Modules.Files.Persistence.Repositories.Images.Reads;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateFilesContextAsync()
		{
			FilesContext context = provider.GetRequiredService<FilesContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddFilesPersistence(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddReads()
				.AddWrites()
				.AddUnitOfWork();

		private IServiceCollection AddContext(string connectionString)
			=> services.AddDbContext<FilesContext>(options =>
				options.UseNpgsql(connectionString, opt
					=> opt.MigrationsHistoryTable(MigrationsTable, Schemes.Files)
				)
			);

		public IServiceCollection AddReads()
			=> services
				.AddScoped<ICadReads, CadReads>()
				.AddScoped<IImageReads, ImageReads>();

		public IServiceCollection AddWrites()
			=> services.AddScoped(typeof(IWrites<>), typeof(Writes<>));

		public IServiceCollection AddUnitOfWork()
			=> services.AddScoped<IUnitOfWork, UnitOfWork>();
	}

}
