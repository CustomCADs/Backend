using CustomCADs.Modules.Catalog.API.Tags.Endpoints.Queries.Get.All;
using CustomCADs.Modules.Catalog.API.Tags.Endpoints.Queries.Get.Single;
using CustomCADs.Modules.Catalog.API.Tags.Endpoints.Mutations.Post;
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
