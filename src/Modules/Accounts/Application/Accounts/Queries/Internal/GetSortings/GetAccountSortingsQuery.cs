using CustomCADs.Modules.Accounts.Domain.Accounts.Enums;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;

namespace CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetSortings;

[AddRequestCaching(ExpirationType.Absolute)]
public sealed record GetAccountSortingsQuery : IQuery<AccountSortingType[]>;
