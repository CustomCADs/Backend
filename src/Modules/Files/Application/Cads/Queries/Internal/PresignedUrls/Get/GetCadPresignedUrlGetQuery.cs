using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Get;

public sealed record GetCadPresignedUrlGetQuery(
	CadId Id,
	FileContextType RelationType,
	AccountId CallerId
) : IQuery<DownloadFileResponse>;
