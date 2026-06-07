using CustomCADs.Modules.Catalog.Domain.Products;
using CustomCADs.Modules.Catalog.Infrastructure.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static ProductConstants;

public static class DependencyInjection
{
	extension(IServiceCollectionQuartzConfigurator configurator)
	{
		public void AddCatalogBackgroundJobs()
			=> configurator.ScheduleJob<ClearTagsJob>(
				schedule => schedule
					.WithInterval(TimeSpan.FromDays(ClearTagsIntervalDays))
					.RepeatForever()
			);
	}
}
