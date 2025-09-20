using CustomCADs.Delivery.Domain.Repositories;
using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Delivery.Persistence.Repositories;

public class UnitOfWork(DeliveryContext context) : IUnitOfWork
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

	public async Task UpdateStatusByReferenceIdAsync(string[] referenceIds, ShipmentStatus status, CancellationToken ct = default)
	{
		try
		{
			await context.Shipments
				.Where(x => referenceIds.Contains(x.Reference.Id))
				.ExecuteUpdateAsync(
					setPropertyCalls: x => x.SetProperty(x => x.Status, status),
					cancellationToken: ct
				)
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
