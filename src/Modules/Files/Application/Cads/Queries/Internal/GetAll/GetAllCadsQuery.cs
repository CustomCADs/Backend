using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Cads.Queries.Internal.GetAll;

public sealed record GetAllCadsQuery(
	AccountId OwnerId,
	Pagination Pagination
) : IQuery<Result<CadDto>>;
