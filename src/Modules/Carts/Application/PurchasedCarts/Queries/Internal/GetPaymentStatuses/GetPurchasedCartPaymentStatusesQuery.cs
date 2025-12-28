using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetPurchasedCartPaymentStatusesQuery : IQuery<PaymentStatus[]>;
