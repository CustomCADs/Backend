using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using CustomCADs.Shared.Persistence.Extensions;

namespace CustomCADs.Modules.Printing.Persistence.Repositories.Customizations;

public class Reads(PrintingContext context) : ICustomizationReads
{
	public async Task<ICollection<Customization>> AllAsync(CustomizationId[] ids, bool track = true, CancellationToken ct = default)
		=> await context.Customizations
			.WithTracking(track)
			.Where(x => ids.Contains(x.Id))
			.ToArrayAsync(ct)
			.ConfigureAwait(false);

	public async Task<Customization?> SingleByIdAsync(CustomizationId id, bool track = true, CancellationToken ct = default)
		=> await context.Customizations
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(CustomizationId id, CancellationToken ct = default)
		=> await context.Customizations
			.WithTracking(false)
			.AnyAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);
}
