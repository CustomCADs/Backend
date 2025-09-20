using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Delivery.Application.Contracts.Dtos;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using SpeedyNET.Abstractions.Contracts.Calculation;
using SpeedyNET.Abstractions.Contracts.Shipment;
using SpeedyNET.Abstractions.Contracts.Track;
using SpeedyNET.Core.Enums;

namespace CustomCADs.Delivery.Infrastructure;

using ISpeedyClient = SpeedyNET.Sdk.ISpeedyService;

internal sealed class SpeedyDeliveryService(ISpeedyClient speedy) : IDeliveryService
{
	private const Payer Payer = SpeedyNET.Core.Enums.Payer.RECIPIENT;
	private const PaperSize Paper = PaperSize.A4;
	private const int Count = 1;

	public async Task<CalculationDto[]> CalculateAsync(CalculateRequest req, CancellationToken ct = default)
	{
		CalculateModel[] response = await speedy.CalculateAsync(
			payer: Payer,
			weights: req.Weights,
			country: req.Country,
			site: req.City,
			street: req.Street,
			ct: ct
		).ConfigureAwait(false);

		return [.. response.Select(x => new CalculationDto(
			Service: x.Service,
			Price: new(
				Amount: x.Price.Amount,
				Vat: x.Price.Vat,
				Total: x.Price.Total,
				Currency: x.Price.Currency
			),
			PickupDate: x.PickupDate,
			DeliveryDeadline: x.DeliveryDeadline
		))];
	}

	public async Task<bool> ValidateAsync(
		string country,
		string city,
		string street,
		string? phone,
		CancellationToken ct = default
	) => await IsAddressValid(country, city, street, ct).ConfigureAwait(false)
		&& await IsPhoneValid(phone, ct).ConfigureAwait(false);

	public async Task<ShipmentDto> ShipAsync(
		ShipRequestDto req,
		CancellationToken ct = default
	)
	{
		WrittenShipmentModel response = await speedy.CreateShipmentAsync(
			payer: Payer,
			package: req.Package,
			contents: req.Contents,
			parcelCount: Count,
			totalWeight: req.TotalWeight,
			country: req.Country,
			site: req.City,
			street: req.Street,
			name: req.Name,
			service: req.Service,
			email: req.Email,
			phoneNumber: req.Phone,
			ct: ct
		).ConfigureAwait(false);

		return new(
			Id: response.Id,
			ParcelIds: [.. response.Parcels.Select(x => x.Id)],
			Price: Convert.ToDecimal(response.Price.Amount),
			PickupDate: response.PickupDate,
			DeliveryDeadline: response.DeliveryDeadline
		);
	}

	public async Task CancelAsync(string shipmentId, string comment, CancellationToken ct = default)
		=> await speedy.CancelShipmentAsync(
			shipmentId: shipmentId,
			comment: comment,
			ct: ct
		).ConfigureAwait(false);

	public async Task<byte[]> PrintAsync(string shipmentId, CancellationToken ct = default)
		=> await speedy.PrintAsync(
			paperSize: Paper,
			shipmentId: shipmentId,
			ct: ct
		).ConfigureAwait(false);

	public async Task<ShipmentTrackDto[]> TrackAsync(string shipmentId, CancellationToken ct = default)
	{
		Dictionary<string, ICollection<TrackedParcelModel>> response = await speedy.TrackAsync(
			shipmentIds: [shipmentId],
			ct: ct
		).ConfigureAwait(false);

		return ConvertParcelToStatuses(response.Single().Value);
	}

	public async Task<Dictionary<string, ShipmentTrackDto[]>> TrackAsync(string[] shipmentIds, CancellationToken ct = default)
	{
		Dictionary<string, ICollection<TrackedParcelModel>> response = await speedy.TrackAsync(
			shipmentIds: shipmentIds,
			ct: ct
		).ConfigureAwait(false);

		return response.ToDictionary(
			x => x.Key,
			x => ConvertParcelToStatuses(x.Value)
		);
	}

	private async Task<bool> IsAddressValid(string country, string city, string street, CancellationToken ct = default)
		=> await speedy.ValidateAddress(country, city, street, ct).ConfigureAwait(false);

	private async Task<bool> IsPhoneValid(string? phone, CancellationToken ct = default)
		=> phone is null || await speedy.ValidatePhone(phone, ct).ConfigureAwait(false);

	private static ShipmentTrackDto[] ConvertParcelToStatuses(ICollection<TrackedParcelModel> parcels)
		=> [.. parcels
			.SelectMany(x => x.Operations)
			.Select(x => new ShipmentTrackDto(
				DateTime: x.DateTime,
				IsDelivered: x.IsDelivered,
				Place: x.Place,
				Message: x.Operation
			))
		];
}
