using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.Modules.Files.Application.Cads.Queries.Internal.GetById;

public sealed class GetCadByIdHandler(
	ICadReads reads,
	BaseCachingService<CadId, Cad> cache,
	IRequestSender sender
) : IQueryHandler<GetCadByIdQuery, CadDto>
{
	public async Task<CadDto> Handle(GetCadByIdQuery req, CancellationToken ct = default)
	{
		Cad cad = await cache.GetOrCreateAsync(
			id: req.Id,
			factory: async () => await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
				?? throw CustomNotFoundException<Cad>.ById(req.Id)
		).ConfigureAwait(false);

		return cad.ToDto(
			ownerName: await sender.SendQueryAsync(
				query: new GetUsernameByIdQuery(cad.OwnerId),
				ct: ct
			).ConfigureAwait(false)
		);
	}
}
