using CustomCADs.Modules.Delivery.Application.Contracts;
using CustomCADs.Modules.Delivery.Application.Contracts.Dtos;
using SpeedyNET.Abstractions.Contracts.Calculation;
using SpeedyNET.Abstractions.Contracts.Pickup;
using SpeedyNET.Abstractions.Contracts.Shipment;
using SpeedyNET.Abstractions.Contracts.Track;
using SpeedyNET.Core.Enums;

namespace CustomCADs.Modules.Delivery.Infrastructure;

using ISpeedyClient = SpeedyNET.Sdk.ISpeedyService;

internal sealed class SpeedyDeliveryService(ISpeedyClient speedy) : IDeliveryService
{
	private const Payer DefaultPayer = Payer.SENDER;
	private const PaperSize Paper = PaperSize.A4;
	private const int Count = 1;

	public async Task<int?> GetCountryAsync(string name, CancellationToken ct = default)
	{
		var country = await speedy.FindCountryAsync(name, ct: ct).ConfigureAwait(false);

		return country.FirstOrDefault()?.Id;
	}

	public async Task<CalculationDto[]> CalculateAsync(CalculateRequest req, CancellationToken ct = default)
	{
		double weight = Math.Round(req.TotalWeight, digits: 3, mode: MidpointRounding.ToPositiveInfinity);
		CalculateModel[] response = await speedy.CalculateAsync(
			pickup: new(req.OfficeId, req.Address?.Country, req.Address?.Site),
			parcels: new(Count, req.TotalWeight),
			payer: DefaultPayer,
			destination: req.Destination.ToSpeedy(),
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
		string? phone,
		CancellationToken ct = default
	) => await IsPhoneValidAsync(phone, ct).ConfigureAwait(false);

	public async Task<bool> ValidateAsync(
		string country,
		string city,
		string street,
		CancellationToken ct = default
	) => await IsAddressValidAsync(country, city, street, ct).ConfigureAwait(false);

	public async Task<ShipmentDto> ShipAsync(
		ShipRequestDto req,
		CancellationToken ct = default
	)
	{
		WrittenShipmentModel response = await speedy.CreateShipmentAsync(
			pickup: new(req.OfficeId, req.Address?.ToSpeedy()),
			contact: new(req.Name, req.Email, req.Phone, null),
			parcels: new(Count, req.TotalWeight, req.Package, req.Contents),
			payer: DefaultPayer,
			destination: req.Destination.ToSpeedy(),
			service: req.Service,
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

	public async Task<Dictionary<long, (DateTime? From, DateTime? To)>> PickupAsync(IEnumerable<string> shipmentIds, DateTimeOffset pickupAt, TimeOnly endTime, CancellationToken ct = default)
	{
		PickupModel[] result = await speedy.Pickup(
			pickup: new(pickupAt.Date, endTime, true),
			scope: PickupScope.EXPLICIT_SHIPMENT_ID_LIST,
			shipmentIds: [.. shipmentIds],
			ct: ct
		).ConfigureAwait(false);

		return result.ToDictionary(
			x => x.Id,
			x => (x.PickupPeriodFrom, x.PickupPeriodTo)
		);
	}

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

	private async Task<bool> IsAddressValidAsync(string country, string city, string street, CancellationToken ct = default)
		=> await speedy.ValidateAddress(country, city, street, ct).ConfigureAwait(false);

	private async Task<bool> IsPhoneValidAsync(string? phone, CancellationToken ct = default)
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
