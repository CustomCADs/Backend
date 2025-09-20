using CustomCADs.Delivery.Application.Shipments.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static IServiceCollection AddDeliveryBackgroundJobs(this IServiceCollection services)
	{
		services.AddQuartz(configurator =>
		{
			configurator.AddTrigger(conf => conf
				.ForJob(configurator.AddJob<PollShipmentStatusJob>())
				.WithSimpleSchedule(schedule =>
					schedule
						.WithInterval(TimeSpan.FromHours(PollShipmentStatusJob.IntervalHours))
						.RepeatForever()
				));
		});

		return services;
	}

}
