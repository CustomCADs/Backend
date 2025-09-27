using CustomCADs.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Idempotency.Infrastructure.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static IdempotencyKeyConstants;

public static class DependencyInjection
{
	public static void AddIdempotencyBackgroundJobs(this IServiceCollectionQuartzConfigurator configurator)
	{
		configurator.AddTrigger(conf => conf
			.ForJob(configurator.AddJobAndReturnKey<ClearIdempotencyKeysJob>())
			.WithSimpleSchedule(schedule =>
				schedule
					.WithInterval(TimeSpan.FromHours(ClearIdempotencyKeysIntervalHours))
					.RepeatForever()
			));
	}
}
