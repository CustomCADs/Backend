using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Presigned.Put;

public record CadPresignedUrlPutRequest(
	Guid Id,
	UploadFileRequest File,
	FileContextType RelationType
);
