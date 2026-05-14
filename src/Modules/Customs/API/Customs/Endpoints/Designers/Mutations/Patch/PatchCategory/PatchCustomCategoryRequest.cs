namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designers.Mutations.Patch.PatchCategory;

public record PatchCustomCategoryRequest(
	Guid Id,
	int CategoryId
);
