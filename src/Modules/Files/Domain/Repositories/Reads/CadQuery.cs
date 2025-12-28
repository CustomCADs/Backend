using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Domain.Repositories.Reads;

public record CadQuery(
	Pagination Pagination,
	AccountId? OwnerId = null,
	CadId[]? Ids = null
);
