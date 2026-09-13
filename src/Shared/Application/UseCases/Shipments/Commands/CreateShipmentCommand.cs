using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Shared.Application.UseCases.Shipments.Commands;

public sealed record CreateShipmentToAddressCommand(
	string Service,
	AddressDto Address,
	ShipmentInfoDto Info,
	ContactDto Contact,
	AccountId BuyerId
) : ICommand<ShipmentId>;

public sealed record CreateShipmentToOfficeCommand(
	string Service,
	int OfficeId,
	ShipmentInfoDto Info,
	ContactDto Contact,
	AccountId BuyerId
) : ICommand<ShipmentId>;
