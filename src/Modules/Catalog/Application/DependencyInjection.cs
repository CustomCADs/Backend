using CustomCADs.Catalog.Application.Products.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static ProductConstants;

public static partial class DependencyInjection
{
	public static IServiceCollection AddCatalogBackgroundJobs(this IServiceCollection services)
	{
		services.AddQuartz(configurator =>
		{
			configurator.AddTrigger(conf => conf
				.ForJob(configurator.AddJob<ClearTagsJob>())
				.WithSimpleSchedule(schedule =>
					schedule
						.WithInterval(TimeSpan.FromDays(ClearTagsIntervalDays))
						.RepeatForever()
				));
		});

		return services;
	}
}
