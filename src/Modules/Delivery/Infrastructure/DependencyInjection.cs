using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Infrastructure;
using CustomCADs.Delivery.Infrastructure.BackgroundJobs;
using CustomCADs.Shared.Infrastructure;
using Microsoft.Extensions.Options;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddDeliveryService(this IServiceCollection services)
	{
		services.AddSpeedyService((provider) =>
		{
			using IServiceScope scope = provider.CreateScope();
			DeliverySettings settings = scope.ServiceProvider
				.GetRequiredService<IOptions<DeliverySettings>>()
				.Value;

			return new(
				Account: settings.Account,
				Pickup: settings.Pickup,
				Contact: settings.Contact
			);
		});

		services.AddScoped<IDeliveryService>(
			(sp) => new ResilientDeliveryService(
				inner: new SpeedyDeliveryService(
					speedy: sp.GetRequiredService<SpeedyNET.Sdk.ISpeedyService>()
				),
				policy: Polly.Policy.WrapAsync(
					Polly.Policy.Handle<Exception>().AsyncCircuitBreak(),
					Polly.Policy.Handle<Exception>().AsyncRetry()
				)
			)
		);
	}

	public static void AddDeliveryBackgroundJobs(this IServiceCollectionQuartzConfigurator configurator)
	{
		configurator.AddTrigger(conf => conf
			.ForJob(configurator.AddJobAndReturnKey<PollShipmentStatusJob>())
			.WithSimpleSchedule(schedule =>
				schedule
					.WithInterval(TimeSpan.FromHours(PollShipmentStatusJob.IntervalHours))
					.RepeatForever()
			));
	}
}
