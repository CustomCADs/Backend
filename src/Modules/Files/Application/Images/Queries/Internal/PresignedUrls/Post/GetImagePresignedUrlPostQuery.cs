using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Post;

public record GetImagePresignedUrlPostQuery(
	string Name,
	UploadFileRequest File,
	FileContextType RelationType,
	AccountId CallerId
) : IQuery<UploadFileResponse>;
