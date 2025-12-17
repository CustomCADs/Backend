using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Put;

public sealed record GetCadPresignedUrlPutQuery(
	CadId Id,
	UploadFileRequest File,
	FileContextType RelationType,
	AccountId CallerId
) : IQuery<string>;
