using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Modules.Files.Application.Images.Queries.Shared;

public sealed class ImageExistsByIdHandler(IImageReads reads)
	: IQueryHandler<ImageExistsByIdQuery, bool>
{
	public async Task<bool> Handle(ImageExistsByIdQuery req, CancellationToken ct)
		=> await reads.ExistsByIdAsync(req.Id, ct: ct).ConfigureAwait(false);
}
