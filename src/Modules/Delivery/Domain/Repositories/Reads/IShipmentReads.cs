using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Delivery.Domain.Repositories.Reads;

public interface IShipmentReads
{
	Task<Result<Shipment>> AllAsync(ShipmentQuery query, bool track = true, CancellationToken ct = default);
	Task<ICollection<string?>> AllIdsByStatusAsync(ShipmentStatus status, bool track = true, CancellationToken ct = default);
	Task<Shipment?> SingleByIdAsync(ShipmentId id, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(ShipmentId id, CancellationToken ct = default);
	Task<int> CountAsync(CancellationToken ct = default);
}
