namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designers.Mutations.Patch.Finish;

public sealed record FinishCustomRequest(
	Guid Id,
	decimal Price,
	Guid CadId
);
