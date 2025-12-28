using CustomCADs.Modules.Catalog.Application.Tags.Dtos;
using CustomCADs.Modules.Catalog.Domain.Tags;

namespace CustomCADs.Modules.Catalog.Application.Tags;

internal static class Mapper
{
	extension(Tag tag)
	{
		internal TagDto ToDto()
			=> new(
				Id: tag.Id,
				Name: tag.Name
			);
	}

}
