#pragma warning disable IDE0130
using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Infrastructure.BackgroundJobs;
using CustomCADs.Modules.Identity.Infrastructure.Fingerprints;
using CustomCADs.Modules.Identity.Infrastructure.Identity.Context;
using CustomCADs.Modules.Identity.Infrastructure.Identity.Services;
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
		public IServiceCollection AddFingerprintsService()
			=> services.AddScoped<IFingerprintService, DeviceDetectorFingerprintService>();

		public IServiceCollection AddTokensService()
			=> services.AddScoped<ITokenService, JwtTokenService>();

		public IServiceCollection AddIdentityServices(string connectionString)
			=> services
				.AddContext(connectionString)
				.AddScoped<IUserService, AppUserService>()
				.AddScoped<IRoleService, AppRoleService>();

		private IServiceCollection AddContext(string connectionString)
		{
			services.AddSingleton(
				sp => new NpgsqlDataSourceBuilder(connectionString).EnableDynamicJson().Build()
			);

			services.AddDbContext<IdentityContext>((sp, options) =>
				options.UseNpgsql(
					dataSource: sp.GetRequiredService<NpgsqlDataSource>(),
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
			configurator.ScheduleJob<ClearRefreshTokensJob>(
				schedule => schedule
					.WithInterval(TimeSpan.FromDays(ClearRefreshTokensJob.IntervalDays))
					.RepeatForever()
			);
		}
	}

	extension(IServiceCollectionQuartzConfigurator configurator)
	{
		public void ScheduleJob<TJob>(IScheduleBuilder schedule, string? name = null) where TJob : IJob
		{
			configurator.ScheduleJob<TJob>(
				trigger => trigger.WithSchedule(schedule),
				job => job.WithIdentity(name ?? typeof(TJob).Name)
			);
		}

		public void ScheduleJob<TJob>(Action<SimpleScheduleBuilder> schedule, string? name = null) where TJob : IJob
		{
			var builder = SimpleScheduleBuilder.Create();
			schedule(builder);
			ScheduleJob<TJob>(configurator, builder, name);
		}
	}

}
