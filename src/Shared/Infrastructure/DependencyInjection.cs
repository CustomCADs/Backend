using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Currencies;
using CustomCADs.Shared.Infrastructure.BackgroundJobs.Currencies;
using CustomCADs.Shared.Infrastructure.Cache;
using CustomCADs.Shared.Infrastructure.Currencies;
using CustomCADs.Shared.Infrastructure.Email;
using CustomCADs.Shared.Infrastructure.Events;
using CustomCADs.Shared.Infrastructure.Payment;
using CustomCADs.Shared.Infrastructure.Requests;
using FluentValidation;
using JasperFx.CodeGeneration;
using Microsoft.Extensions.Options;
using Quartz;
using System.Reflection;
using System.Runtime.InteropServices;
using Wolverine;
using Wolverine.FluentValidation;


#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddCacheService()
		{
			services.AddMemoryCache();
			services.AddScoped(typeof(ICacheService<>), typeof(MemoryCacheService<>));
			services.AddScoped<ICacheService, MemoryCacheService>();
		}

		public void AddEmailService()
		{
			services.AddScoped<IEmailService>(
				(sp) => new ResilientEmailService(
					inner: new FluentEmailService(
						settings: sp.GetRequiredService<IOptions<EmailSettings>>()
					),
					policy: Polly.Policy.Handle<Exception>().AsyncRetry()
				)
			);
		}

		public void AddMessagingServices(bool codeGen, Assembly entry, params Assembly[] assemblies)
		{
			services.AddValidatorsFromAssemblies(assemblies);

			services.AddWolverine(cfg =>
			{
				foreach (Assembly assembly in assemblies)
				{
					cfg.Discovery.IncludeAssembly(assembly);
				}

				if (codeGen)
				{
					cfg.CodeGeneration.ApplicationAssembly = entry;
					cfg.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
				}

				cfg.UseFluentValidation();
				cfg.Services.AddSingleton(typeof(IFailureAction<>), typeof(CustomFailureAction<>));
			});

			services.AddScoped<IRequestSender, WolverineRequestSender>();
			services.AddScoped<IEventRaiser, WolverineEventRaiser>();
		}

		public void AddPaymentService()
		{
			services.AddScoped<Stripe.PaymentIntentService>();
			services.AddScoped<IPaymentService>(
				(sp) => new ResilientPaymentService(
					inner: new StripeService(
						service: sp.GetRequiredService<Stripe.PaymentIntentService>()
					),
					policy: Polly.Policy.WrapAsync(
						Polly.Policy.Handle<Exception>().AsyncCircuitBreak(),
						Polly.Policy.Handle<Exception>().AsyncRetry()
					)
				)
			);
		}

		public void AddCurrenciesService()
		{
			const string namedClient = "ESB";
			services.AddHttpClient(namedClient,
				client => client.BaseAddress = new("https://www.ecb.europa.eu")
			);

			services.AddScoped<ICurrencyService>(
				(sp) => new ResilientCurrencyService(
					inner: new ECBCurrencyService(
						client: sp.GetRequiredService<IHttpClientFactory>().CreateClient(namedClient)
					),
					policy: Polly.Policy.WrapAsync(
						Polly.Policy.Handle<Exception>().AsyncCircuitBreak(),
						Polly.Policy.Handle<Exception>().AsyncRetry()
					)
				)
			);
		}
	}

	extension(IServiceCollectionQuartzConfigurator configurator)
	{
		public void AddSharedBackgroundJobs()
		{
			string cet = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
					? "Central European Standard Time"
					: "Europe/Berlin";

			configurator.ScheduleJob<UpdateExchangeRatesCacheJob>(
				schedule: CronScheduleBuilder
					.DailyAtHourAndMinute(16, 30)
					.InTimeZone(tz: TimeZoneInfo.FindSystemTimeZoneById(cet))
			);
		}

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
