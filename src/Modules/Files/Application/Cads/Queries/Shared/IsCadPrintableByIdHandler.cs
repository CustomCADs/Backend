using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Shared;

public sealed class IsCadPrintableByIdHandler(ICadReads reads, BaseCachingService<CadId, Cad> cache)
	: IQueryHandler<IsCadPrintableByIdQuery, bool>
{
	public async Task<bool> Handle(IsCadPrintableByIdQuery req, CancellationToken ct)
	{
		Cad cad = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Cad>.ById(req.Id)
		).ConfigureAwait(false);

		return ApplicationConstants.Cads.PrintableContentTypes.Contains(cad.ContentType);
	}
}
