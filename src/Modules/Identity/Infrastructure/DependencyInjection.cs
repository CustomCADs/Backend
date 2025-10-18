#pragma warning disable IDE0130
using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Infrastructure.BackgroundJobs;
using CustomCADs.Identity.Infrastructure.Identity;
using CustomCADs.Identity.Infrastructure.Identity.Context;
using CustomCADs.Identity.Infrastructure.Tokens;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Quartz;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static async Task<IServiceProvider> UpdateIdentityContextAsync(this IServiceProvider provider)
	{
		IdentityContext context = provider.GetRequiredService<IdentityContext>();
		await context.Database.MigrateAsync().ConfigureAwait(false);

		return provider;
	}

	private static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
	{
		services.AddDbContext<IdentityContext>(options =>
			options.UseNpgsql(
				dataSource: new NpgsqlDataSourceBuilder(connectionString).EnableDynamicJson().Build(),
				npgsqlOptionsAction: opt => opt.MigrationsHistoryTable("__EFMigrationsHistory", "Identity")
			)
		);

		return services;
	}

	public static IServiceCollection AddTokensService(this IServiceCollection services)
	{
		services.AddScoped<ITokenService, IdentityTokenService>();

		return services;
	}

	public static IServiceCollection AddIdentityServices(this IServiceCollection services, string connectionString)
	{
		services.AddContext(connectionString);

		services.AddScoped<IUserService, AppUserService>();
		services.AddScoped<IRoleService, AppRoleService>();

		return services;
	}

	public static void AddIdentityBackgroundJobs(this IServiceCollectionQuartzConfigurator configurator)
	{
		configurator.AddTrigger(conf => conf
			.ForJob(configurator.AddJobAndReturnKey<ClearRefreshTokensJob>())
			.WithSimpleSchedule(schedule =>
				schedule
					.WithInterval(TimeSpan.FromDays(ClearRefreshTokensJob.IntervalDays))
					.RepeatForever()
			));
	}

	private static JobKey AddJobAndReturnKey<TJob>(this IServiceCollectionQuartzConfigurator q, string? name = null)
		where TJob : IJob
	{
		JobKey key = new(name ?? typeof(TJob).Name);
		q.AddJob<TJob>(conf => conf.WithIdentity(key));
		return key;
	}
}
