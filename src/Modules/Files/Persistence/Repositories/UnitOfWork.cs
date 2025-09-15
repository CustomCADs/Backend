using CustomCADs.Files.Domain.Cads;
using CustomCADs.Files.Domain.Repositories;
using CustomCADs.Shared.Persistence.Exceptions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Files.Persistence.Repositories;

public class UnitOfWork(FilesContext context) : IUnitOfWork
{
	public async Task SaveChangesAsync(CancellationToken ct = default)
	{
		try
		{
			await context.SaveChangesAsync(ct).ConfigureAwait(false);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			throw DatabaseConflictException.Custom(ex.Message);
		}
		catch (DbUpdateException ex)
		{
			throw DatabaseException.Custom(ex.Message);
		}
	}

	public async Task<ICollection<Cad>> BulkInsertCadsAsync(ICollection<Cad> cads, CancellationToken ct = default)
	{
		try
		{
			await context.BulkInsertAsync(
				entities: cads,
				cancellationToken: ct
			).ConfigureAwait(false);

			return await context.Cads
				.Where(x => cads.Select(x => x.Id).Contains(x.Id))
				.ToArrayAsync(ct)
				.ConfigureAwait(false);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			throw DatabaseConflictException.Custom(ex.Message);
		}
		catch (DbUpdateException ex)
		{
			throw DatabaseException.Custom(ex.Message);
		}
	}
}
