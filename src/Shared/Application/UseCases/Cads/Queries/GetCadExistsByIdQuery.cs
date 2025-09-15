namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public sealed record GetCadExistsByIdQuery(CadId Id) : IQuery<bool>;
