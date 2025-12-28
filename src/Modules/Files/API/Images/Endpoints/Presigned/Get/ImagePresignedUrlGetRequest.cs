using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.API.Images.Endpoints.Presigned.Get;

public record ImagePresignedUrlGetRequest(
	Guid Id,
	FileContextType RelationType
);
