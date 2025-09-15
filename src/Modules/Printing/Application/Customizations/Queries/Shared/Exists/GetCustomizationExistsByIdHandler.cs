using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Customizations.Queries;

namespace CustomCADs.Printing.Application.Customizations.Queries.Shared.Exists;

public sealed class GetCustomizationExistsByIdHandler(ICustomizationReads reads)
	: IQueryHandler<GetCustomizationExistsByIdQuery, bool>
{
	public async Task<bool> Handle(GetCustomizationExistsByIdQuery req, CancellationToken ct)
		=> await reads.ExistsByIdAsync(req.Id, ct).ConfigureAwait(false);
}
