using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Files.Application.Images.Queries.Internal.PresignedUrls.Put;

public record GetImagePresignedUrlPutQuery(
	ImageId Id,
	UploadFileRequest File,
	FileContextType RelationType,
	AccountId CallerId
) : IQuery<string>;
