namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public sealed record GetCadVolumeByIdQuery(
	CadId Id
) : IQuery<decimal>;
