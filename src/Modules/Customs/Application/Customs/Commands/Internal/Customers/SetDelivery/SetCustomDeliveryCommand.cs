using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.SetDelivery;

public sealed record SetCustomDeliveryCommand(
	CustomId Id,
	bool Value,
	AccountId BuyerId
) : ICommand;
