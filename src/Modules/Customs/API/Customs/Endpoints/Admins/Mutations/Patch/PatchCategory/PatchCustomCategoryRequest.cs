namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Admins.Mutations.Patch.PatchCategory;

public record PatchCustomCategoryRequest(
	Guid Id,
	int CategoryId
);
