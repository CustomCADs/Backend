using CustomCADs.Shared.Application.Abstractions.Requests.Queries;

namespace CustomCADs.Modules.Files.Application.Cads.Queries.Internal.GetById;

public sealed record GetCadByIdQuery(
	CadId Id
) : IQuery<CadDto>;
