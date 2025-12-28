using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentStarted;

public record CustomPaymentStartedApplicationEvent(Guid Id) : BaseApplicationEvent;
