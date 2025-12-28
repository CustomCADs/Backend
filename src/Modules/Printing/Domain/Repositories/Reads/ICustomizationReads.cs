using CustomCADs.Modules.Printing.Domain.Customizations;

namespace CustomCADs.Modules.Printing.Domain.Repositories.Reads;

public interface ICustomizationReads
{
	Task<ICollection<Customization>> AllAsync(CustomizationId[] ids, bool track = true, CancellationToken ct = default);
	Task<Customization?> SingleByIdAsync(CustomizationId id, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(CustomizationId id, CancellationToken ct = default);
}
