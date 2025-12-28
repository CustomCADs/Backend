using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Presigned.Get;

public record CadPresignedUrlGetRequest(
	Guid Id,
	FileContextType RelationType
);
