using CustomCADs.Identity.Infrastructure.Identity.Context;
using CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace CustomCADs.Identity.Infrastructure.BackgroundJobs;

public class ClearRefreshTokensJob(IdentityContext dbContext) : IJob
{
	public const int IntervalDays = 1;

	public async Task Execute(IJobExecutionContext jobContext)
	{
		await dbContext.Set<AppRefreshToken>()
			.Where(x => x.ExpiresAt < DateTimeOffset.UtcNow)
			.ExecuteDeleteAsync(jobContext.CancellationToken)
			.ConfigureAwait(false);
	}
}
