namespace CustomCADs.Customs.API.Customs.Endpoints.Admins.Patch.PatchCategory;

public record PatchCustomCategoryRequest(
	Guid Id,
	int CategoryId
);
