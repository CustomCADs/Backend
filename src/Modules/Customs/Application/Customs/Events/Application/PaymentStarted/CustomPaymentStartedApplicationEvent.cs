using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Customs.Application.Customs.Events.Application.PaymentStarted;

public record CustomPaymentStartedApplicationEvent(Guid Id) : BaseApplicationEvent;
