namespace CustomCADs.Modules.Printing.Application.Materials.Queries.Internal.GetAll;

public sealed record GetAllMaterialsQuery
	: IQuery<ICollection<MaterialDto>>;
