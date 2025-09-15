namespace CustomCADs.Printing.Application.Materials.Queries.Internal.GetById;

public sealed record GetMaterialByIdQuery(
	MaterialId Id
) : IQuery<MaterialDto>;
