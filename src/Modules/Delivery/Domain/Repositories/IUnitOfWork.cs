using CustomCADs.Delivery.Domain.Shipments.Enums;

namespace CustomCADs.Delivery.Domain.Repositories;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken ct = default);
	Task UpdateStatusByReferenceIdAsync(string[] referenceIds, ShipmentStatus status, CancellationToken ct = default);
}
