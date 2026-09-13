using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;

namespace CustomCADs.Modules.Delivery.Infrastructure;

public class ResilientDeliveryService(
	IDeliveryService inner,
	Polly.IAsyncPolicy policy
) : IDeliveryService
{
	public Task<bool> ValidateAsync(string? phone, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.ValidateAsync(phone, ct));

	public Task<bool> ValidateAsync(string country, string city, string street, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.ValidateAsync(country, city, street, ct));

	public Task<int?> GetCountryAsync(string name, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.GetCountryAsync(name, ct));

	public Task<CalculationDto[]> CalculateAsync(CalculateRequest req, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.CalculateAsync(req, ct));

	public Task CancelAsync(string shipmentId, string comment, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.CancelAsync(shipmentId, comment, ct));

	public Task<byte[]> PrintAsync(string shipmentId, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.PrintAsync(shipmentId, ct));

	public Task<Dictionary<long, (DateTime? From, DateTime? To)>> PickupAsync(IEnumerable<string> shipmentIds, DateTimeOffset pickupAt, TimeOnly endTime, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.PickupAsync(shipmentIds, pickupAt, endTime, ct));

	public Task<ShipmentDto> ShipAsync(ShipRequestDto req, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.ShipAsync(req, ct));

	public Task<ShipmentTrackDto[]> TrackAsync(string shipmentId, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.TrackAsync(shipmentId, ct));

	public Task<Dictionary<string, ShipmentTrackDto[]>> TrackAsync(string[] shipmentIds, CancellationToken ct = default)
		=> policy.ExecuteAsync(() => inner.TrackAsync(shipmentIds, ct));
}
