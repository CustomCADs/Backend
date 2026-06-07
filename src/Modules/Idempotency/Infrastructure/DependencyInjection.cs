using CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Modules.Idempotency.Infrastructure.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static IdempotencyKeyConstants;

public static class DependencyInjection
{
	extension(IServiceCollectionQuartzConfigurator configurator)
	{
		public void AddIdempotencyBackgroundJobs()
			=> configurator.ScheduleJob<ClearIdempotencyKeysJob>(
				schedule => schedule
					.WithInterval(TimeSpan.FromHours(ClearIdempotencyKeysIntervalHours))
					.RepeatForever()
			);
	}

}
