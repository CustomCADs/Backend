namespace CustomCADs.Shared.Application.Events.Customs;

public record CustomPaymentCompletedApplicationEvent(
	CustomId CustomId,
	AccountId BuyerId
) : BaseApplicationEvent
{
	public Guid Id => CustomId.Value;  // raw Guid for Saga Identity to work;
};
