using CustomCADs.Shared.Application.Dtos.Files;

namespace CustomCADs.Shared.Application.UseCases.Cads.Queries;

public sealed record GetCadPresignedUrlGetByIdQuery(
	CadId Id
) : IQuery<DownloadFileResponse>;
