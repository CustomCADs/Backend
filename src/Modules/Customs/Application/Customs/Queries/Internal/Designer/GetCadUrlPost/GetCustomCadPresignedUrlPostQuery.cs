using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

public sealed record GetCustomCadPresignedUrlPostQuery(
	CustomId Id,
	UploadFileRequest Cad,
	AccountId CallerId
) : IQuery<UploadFileResponse>;
