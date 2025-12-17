using CustomCADs.Modules.Catalog.API.Tags.Endpoints.Get.All;
using CustomCADs.Modules.Catalog.API.Tags.Endpoints.Get.Single;
using CustomCADs.Modules.Catalog.API.Tags.Endpoints.Post;
using CustomCADs.Modules.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Modules.Catalog.API.Tags;

internal static class Mapper
{
	extension(TagDto tag)
	{
		internal CreateTagResponse ToCreateTagResponse()
			=> new(
				tag.Id.Value,
				tag.Name
			);

		internal GetTagByIdResponse ToGetTagByIdResponse()
			=> new(
				tag.Id.Value,
				tag.Name
			);

		internal GetAllTagsResponse ToGetAllTagsResponse()
			=> new(
				tag.Id.Value,
				tag.Name
			);
	}

}
