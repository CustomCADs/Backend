using CustomCADs.Catalog.Application.Tags.Dtos;
using CustomCADs.Catalog.API.Tags.Endpoints.Get.All;
using CustomCADs.Catalog.API.Tags.Endpoints.Get.Single;
using CustomCADs.Catalog.API.Tags.Endpoints.Post;

namespace CustomCADs.Catalog.API.Tags;

internal static class Mapper
{
	internal static CreateTagResponse ToCreateTagResponse(this TagDto tag)
		=> new(
			tag.Id.Value,
			tag.Name
		);

	internal static GetTagByIdResponse ToGetTagByIdResponse(this TagDto tag)
		=> new(
			tag.Id.Value,
			tag.Name
		);

	internal static GetAllTagsResponse ToGetAllTagsResponse(this TagDto tag)
		=> new(
			tag.Id.Value,
			tag.Name
		);
}
