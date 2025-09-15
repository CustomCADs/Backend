using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Images.Queries;

public sealed record GetImagePresignedUrlPostByIdQuery(
	string Name,
	UploadFileRequest File
) : IQuery<UploadFileResponse>;
