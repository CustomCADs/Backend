using CustomCADs.Catalog.Domain.Products;
using CustomCADs.Catalog.Infrastructure.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static ProductConstants;

public static class DependencyInjection
{
	public static void AddCatalogBackgroundJobs(this IServiceCollectionQuartzConfigurator configurator)
	{
		configurator.AddTrigger(conf => conf
			.ForJob(configurator.AddJobAndReturnKey<ClearTagsJob>())
			.WithSimpleSchedule(schedule =>
				schedule
					.WithInterval(TimeSpan.FromDays(ClearTagsIntervalDays))
					.RepeatForever()
			));
	}
}
