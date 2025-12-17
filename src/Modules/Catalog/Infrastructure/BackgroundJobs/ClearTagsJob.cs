using CustomCADs.Modules.Catalog.Domain.Products;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using Quartz;

namespace CustomCADs.Modules.Catalog.Infrastructure.BackgroundJobs;

using static DomainConstants;
using static ProductConstants;

public class ClearTagsJob(IProductReads reads, IUnitOfWork uow) : IJob
{
	public async Task Execute(IJobExecutionContext context)
	{
		CancellationToken ct = context.CancellationToken;

		Product? product = await reads.OldestByTagAsync(Tags.New, ct).ConfigureAwait(false);
		if (product is null)
		{
			return; // no products with such tag
		}

		ProductId[] ids = await reads.AllAsync(
			before: DateTimeOffset.UtcNow.AddDays(-ClearTagsBeforeDays),
			after: product.UploadedAt,
			ct: ct
		).ConfigureAwait(false);
		await uow.ClearProductTagsAsync(ids, Tags.New, ct).ConfigureAwait(false);
	}
}
