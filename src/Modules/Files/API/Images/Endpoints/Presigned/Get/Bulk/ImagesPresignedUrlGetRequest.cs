using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.Modules.Files.API.Images.Endpoints.Presigned.Get.Bulk;

public record ImagesPresignedUrlGetRequest(
	Guid[] Ids,
	FileContextType RelationType
);
