#pragma warning disable IDE0130
using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Infrastructure.BackgroundJobs;
using CustomCADs.Modules.Identity.Infrastructure.Identity;
using CustomCADs.Modules.Identity.Infrastructure.Identity.Context;
using CustomCADs.Modules.Identity.Infrastructure.Tokens;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Quartz;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceProvider provider)
	{
		public async Task<IServiceProvider> UpdateIdentityContextAsync()
		{
			IdentityContext context = provider.GetRequiredService<IdentityContext>();
			await context.Database.MigrateAsync().ConfigureAwait(false);

			return provider;
		}
	}

	extension(IServiceCollection services)
	{
		public IServiceCollection AddTokensService()
			=> services.AddScoped<ITokenService, IdentityTokenService>();

		public IServiceCollection AddIdentityServices(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddScoped<IUserService, AppUserService>()
				.AddScoped<IRoleService, AppRoleService>();

		private IServiceCollection AddContext(string connectionString)
		{
			services.AddDbContext<IdentityContext>(options =>
				options.UseNpgsql(
					dataSource: new NpgsqlDataSourceBuilder(connectionString).EnableDynamicJson().Build(),
					npgsqlOptionsAction: opt => opt.MigrationsHistoryTable("__EFMigrationsHistory", IdentityContext.Schema)
				)
			);

			return services;
		}
	}

	extension(IServiceCollectionQuartzConfigurator configurator)
	{
		public void AddIdentityBackgroundJobs()
		{
			configurator.AddTrigger(conf => conf
				.ForJob(configurator.AddJobAndReturnKey<ClearRefreshTokensJob>())
				.WithSimpleSchedule(schedule =>
					schedule
						.WithInterval(TimeSpan.FromDays(ClearRefreshTokensJob.IntervalDays))
						.RepeatForever()
				));
		}
	}

	extension(IServiceCollectionQuartzConfigurator q)
	{
		private JobKey AddJobAndReturnKey<TJob>(string? name = null)
		where TJob : IJob
		{
			JobKey key = new(name ?? typeof(TJob).Name);
			q.AddJob<TJob>(conf => conf.WithIdentity(key));
			return key;
		}
	}

}
