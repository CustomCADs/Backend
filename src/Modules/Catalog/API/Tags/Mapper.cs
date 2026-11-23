using CustomCADs.Modules.Catalog.API.Tags.Dtos;
using CustomCADs.Modules.Catalog.Application.Tags.Dtos;

namespace CustomCADs.Modules.Catalog.API.Tags;

internal static class Mapper
{
	extension(TagDto tag)
	{
		internal TagResponse ToTagResponse()
			=> new(
				tag.Id.Value,
				tag.Name
			);
	}

}
