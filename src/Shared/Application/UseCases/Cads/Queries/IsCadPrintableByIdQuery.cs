namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public sealed record IsCadPrintableByIdQuery(
	CadId Id
) : IQuery<bool>;
