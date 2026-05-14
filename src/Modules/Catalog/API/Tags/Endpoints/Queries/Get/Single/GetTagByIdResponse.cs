namespace CustomCADs.Modules.Catalog.API.Tags.Endpoints.Queries.Get.Single;

public record GetTagByIdResponse(
	Guid Id,
	string Name
);
