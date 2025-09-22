namespace CustomCADs.Catalog.API.Tags.Endpoints.Get.Single;

public record GetTagByIdResponse(
	Guid Id,
	string Name
);
