using CustomCADs.Shared.Application.Abstractions.Requests.Queries;

namespace CustomCADs.Modules.Files.Application.Images.Queries.Internal.GetById;

public sealed record GetImageByIdQuery(
	ImageId Id
) : IQuery<ImageDto>;
