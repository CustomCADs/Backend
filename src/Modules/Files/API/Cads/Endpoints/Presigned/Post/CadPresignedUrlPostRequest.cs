using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Presigned.Post;

public record CadPresignedUrlPostRequest(
	string Name,
	UploadFileRequest File,
	FileContextType RelationType
);
