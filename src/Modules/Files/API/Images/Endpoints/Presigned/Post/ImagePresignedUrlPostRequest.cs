using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.API.Images.Endpoints.Presigned.Post;

public record ImagePresignedUrlPostRequest(
	string Name,
	UploadFileRequest File,
	FileContextType RelationType
);
