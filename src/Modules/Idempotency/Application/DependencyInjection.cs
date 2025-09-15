using CustomCADs.Idempotency.Application.IdempotencyKeys.BackgroundJobs;
using Quartz;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

using static IdempotencyKeyConstants;

public static class DependencyInjection
{
	public static IServiceCollection AddIdempotencyBackgroundJobs(this IServiceCollection services)
	{
		services.AddQuartz(configurator =>
		{
			configurator.AddTrigger(conf => conf
				.ForJob(configurator.AddJob<ClearIdempotencyKeysJob>())
				.WithSimpleSchedule(schedule =>
					schedule
						.WithInterval(TimeSpan.FromHours(ClearIdempotencyKeysIntervalHours))
						.RepeatForever()
				));
		});

		return services;
	}
}
