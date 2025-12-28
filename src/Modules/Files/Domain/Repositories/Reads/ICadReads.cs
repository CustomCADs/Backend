using CustomCADs.Modules.Files.Domain.Cads;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.Modules.Files.Domain.Repositories.Reads;

public interface ICadReads
{
	Task<Result<Cad>> AllAsync(CadQuery query, bool track = true, CancellationToken ct = default);
	Task<Cad?> SingleByIdAsync(CadId id, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(CadId id, CancellationToken ct = default);
}
