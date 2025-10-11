namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public sealed record CadExistsByIdQuery(CadId Id) : IQuery<bool>;
