using CustomCADs.Files.Domain.Cads;

namespace CustomCADs.Files.Domain.Repositories;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken ct = default);
	Task<ICollection<Cad>> BulkInsertCadsAsync(ICollection<Cad> cads, CancellationToken ct = default);
}
