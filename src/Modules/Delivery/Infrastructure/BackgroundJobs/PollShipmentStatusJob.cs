using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using CustomCADs.Modules.Delivery.Domain.Repositories;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Modules.Delivery.Domain.Shipments.Enums;
using Quartz;

namespace CustomCADs.Modules.Delivery.Infrastructure.BackgroundJobs;

public class PollShipmentStatusJob(IShipmentReads reads, IUnitOfWork uow, IDeliveryService delivery) : IJob
{
	public const int IntervalHours = 24;

	public async Task Execute(IJobExecutionContext context)
	{
		CancellationToken ct = context.CancellationToken;

		string?[] activeShipmentReferenceIds = [..
			await reads.AllIdsByStatusAsync(ShipmentStatus.Active, ct: ct).ConfigureAwait(false)
		];
		if (activeShipmentReferenceIds.Length == 0) return;

		Dictionary<string, ShipmentTrackDto[]> statuses = await delivery.TrackAsync(
			[.. activeShipmentReferenceIds.Where(x => x is not null).Select(x => x!)]
		).ConfigureAwait(false);

		string[] deliveredRefs = [.. statuses.Where(x => x.Value.Any(x => x.IsDelivered)).Select(x => x.Key)];
		if (deliveredRefs.Length != 0)
		{
			await uow.UpdateStatusByReferenceIdAsync(deliveredRefs, ShipmentStatus.Delivered, ct).ConfigureAwait(false);
		}
	}
}
