using CustomCADs.Delivery.Application.Contracts.Dtos;

namespace CustomCADs.Delivery.Application.Contracts;

public interface IDeliveryService
{
	Task<bool> ValidateAsync(string country, string city, string street, string? phone, CancellationToken ct = default);
	Task<CalculationDto[]> CalculateAsync(CalculateRequest req, CancellationToken ct = default);
	Task<ShipmentDto> ShipAsync(ShipRequestDto req, CancellationToken ct = default);
	Task CancelAsync(string shipmentId, string comment, CancellationToken ct = default);
	Task<ShipmentTrackDto[]> TrackAsync(string shipmentId, CancellationToken ct = default);
	Task<Dictionary<string, ShipmentTrackDto[]>> TrackAsync(string[] shipmentIds, CancellationToken ct = default);
	Task<byte[]> PrintAsync(string shipmentId, CancellationToken ct = default);
}
