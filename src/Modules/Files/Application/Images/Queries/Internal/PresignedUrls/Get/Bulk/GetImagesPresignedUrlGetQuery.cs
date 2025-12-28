using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Get.Bulk;

public record GetImagesPresignedUrlGetQuery(
	ImageId[] Ids,
	FileContextType RelationType,
	AccountId CallerId
) : IQuery<DownloadFileResponse[]>;
