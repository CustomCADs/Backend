namespace CustomCADs.Modules.Printing.API.Materials.Endpoints.Mutations.Put;

public sealed record PutMaterialRequest(
	int Id,
	string Name,
	decimal Density,
	decimal Cost
);
