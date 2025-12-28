using CustomCADs.Modules.Files.Domain.Images;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.TypedIds.Files;
using CustomCADs.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Modules.Files.Persistence.Repositories.Images;

public sealed class Reads(FilesContext context) : IImageReads
{
	public async Task<Image?> SingleByIdAsync(ImageId id, bool track = true, CancellationToken ct = default)
		=> await context.Images
			.WithTracking(track)
			.FirstOrDefaultAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);

	public async Task<bool> ExistsByIdAsync(ImageId id, CancellationToken ct = default)
		=> await context.Images
			.WithTracking(false)
			.AnyAsync(x => x.Id == id, ct)
			.ConfigureAwait(false);
}
