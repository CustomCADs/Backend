using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Contracts.Dtos;
using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Delivery.Domain.Shipments.Enums;
using Quartz;

namespace CustomCADs.Delivery.Infrastructure.BackgroundJobs;

public class PollShipmentStatusJob(IShipmentReads reads, IUnitOfWork uow, IDeliveryService delivery) : IJob
{
	public const int IntervalHours = 24;

	public async Task Execute(IJobExecutionContext context)
	{
		CancellationToken ct = context.CancellationToken;

		string?[] activeShipmentReferenceIds = [..
			await reads.AllIdsByStatusAsync(ShipmentStatus.Active, ct: ct).ConfigureAwait(false)
		];

		Dictionary<string, ShipmentTrackDto[]> statuses = await delivery.TrackAsync(
			[.. activeShipmentReferenceIds.Where(x => x is not null).Select(x => x!)]
		).ConfigureAwait(false);

		await uow.UpdateStatusByReferenceIdAsync(
			referenceIds: [.. statuses.Where(x => x.Value.Any(x => x.IsDelivered)).Select(x => x.Key)],
			status: ShipmentStatus.Delivered,
			ct: ct
		).ConfigureAwait(false);
	}
}
