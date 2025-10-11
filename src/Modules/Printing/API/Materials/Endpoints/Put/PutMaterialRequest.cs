namespace CustomCADs.Printing.API.Materials.Endpoints.Put;

public sealed record PutMaterialRequest(
	int Id,
	string Name,
	decimal Density,
	decimal Cost
);
