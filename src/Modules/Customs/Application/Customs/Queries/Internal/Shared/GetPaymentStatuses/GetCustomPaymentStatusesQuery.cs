using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetCustomPaymentStatusesQuery : IQuery<PaymentStatus[]>;
