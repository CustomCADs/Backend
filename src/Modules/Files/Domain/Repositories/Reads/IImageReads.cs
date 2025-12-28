using CustomCADs.Modules.Files.Domain.Images;

namespace CustomCADs.Modules.Files.Domain.Repositories.Reads;

public interface IImageReads
{
	Task<Image?> SingleByIdAsync(ImageId id, bool track = true, CancellationToken ct = default);
	Task<bool> ExistsByIdAsync(ImageId id, CancellationToken ct = default);
}
