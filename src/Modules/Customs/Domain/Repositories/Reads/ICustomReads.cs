using CustomCADs.Modules.Customs.Domain.Customs;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Customs.Domain.Repositories.Reads;

public interface ICustomReads
{
	Task<Result<Custom>> AllAsync(CustomQuery query, bool track = true, CancellationToken ct = default);
	Task<Custom?> SingleByIdAsync(CustomId id, bool track = true, CancellationToken ct = default);
	Task<Custom?> SingleByCadIdAsync(CadId cadId, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(CustomId id, CancellationToken ct = default);
	Task<bool> ExistsByCadIdAsync(CadId cadId, CancellationToken ct = default);
	Task<Dictionary<CustomStatus, int>> CountAsync(AccountId buyerId, CancellationToken ct = default);
}
