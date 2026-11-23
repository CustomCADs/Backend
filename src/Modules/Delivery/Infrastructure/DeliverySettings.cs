using SpeedyNET.Abstractions.UserConfigs;

namespace CustomCADs.Modules.Delivery.Infrastructure;

public record DeliverySettings(
	SpeedyAccount Account,
	SpeedyAddress Address,
	SpeedyContact Contact
)
{
	public DeliverySettings() : this(
		Account: new(string.Empty, string.Empty),
		Address: new(string.Empty, string.Empty, string.Empty),
		Contact: new(string.Empty, string.Empty, string.Empty, null)
	)
	{ }
};
