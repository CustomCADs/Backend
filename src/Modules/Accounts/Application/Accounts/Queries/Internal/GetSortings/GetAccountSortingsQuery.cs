using CustomCADs.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetAccountSortingsQuery : IQuery<AccountSortingType[]>;
