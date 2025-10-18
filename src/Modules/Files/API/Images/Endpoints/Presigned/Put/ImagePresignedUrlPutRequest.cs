using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Files.API.Images.Endpoints.Presigned.Put;

public record ImagePresignedUrlPutRequest(
	Guid Id,
	FileContextType RelationType,
	UploadFileRequest File
);
