using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Infrastructure;
using CustomCADs.Modules.Delivery.Infrastructure.BackgroundJobs;
using CustomCADs.Shared.Infrastructure.Requests;
using Microsoft.Extensions.Options;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddDeliveryService()
			=> services
				.AddSpeedyService((provider) =>
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
				})
				.AddScoped<IDeliveryService>(
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

	extension(IServiceCollectionQuartzConfigurator configurator)
	{
		public void AddDeliveryBackgroundJobs()
		 => configurator.AddTrigger(conf => conf
				.ForJob(configurator.AddJobAndReturnKey<PollShipmentStatusJob>())
				.WithSimpleSchedule(schedule =>
					schedule
						.WithInterval(TimeSpan.FromHours(PollShipmentStatusJob.IntervalHours))
						.RepeatForever()
				)
			);
	}

}
