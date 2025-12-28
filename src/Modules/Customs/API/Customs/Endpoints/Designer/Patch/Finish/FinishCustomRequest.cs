namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Patch.Finish;

public sealed record FinishCustomRequest(
	Guid Id,
	decimal Price,
	Guid CadId
);
