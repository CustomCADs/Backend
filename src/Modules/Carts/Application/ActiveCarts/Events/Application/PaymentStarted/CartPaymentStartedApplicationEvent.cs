using CustomCADs.Shared.Domain.Bases.Events;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Events.Application.PaymentStarted;

public record CartPaymentStartedApplicationEvent(Guid Id) : BaseApplicationEvent;
