using CustomCADs.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Files.Application.Cads.Queries.Internal.GetAll;

public sealed class GetAllCadsHandler(
	ICadReads reads,
	BaseCachingService<CadId, Cad> cache,
	IRequestSender sender
) : IQueryHandler<GetAllCadsQuery, Result<CadDto>>
{
	public async Task<Result<CadDto>> Handle(GetAllCadsQuery req, CancellationToken ct = default)
	{
		Result<Cad> result = await cache.GetOrCreateAsync(
			factory: async () => await reads.AllAsync(
				query: new(
					Pagination: req.Pagination,
					OwnerId: req.OwnerId
				),
				track: false,
				ct: ct
			).ConfigureAwait(false)
		).ConfigureAwait(false);

		Dictionary<AccountId, string> ownersNames = await sender.SendQueryAsync(
				query: new GetUsernamesByIdsQuery([.. result.Items.Select(x => x.OwnerId)]),
				ct: ct
			).ConfigureAwait(false);

		return result.ToNewResult(x => x.ToDto(
			ownerName: ownersNames[x.OwnerId]
		));
	}
}
