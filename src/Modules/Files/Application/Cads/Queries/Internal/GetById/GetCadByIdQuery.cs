using CustomCADs.Shared.Application.Abstractions.Requests.Queries;

namespace CustomCADs.Files.Application.Cads.Queries.Internal.GetById;

public sealed record GetCadByIdQuery(
	CadId Id
) : IQuery<CadDto>;
