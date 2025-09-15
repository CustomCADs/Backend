using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetCadUrlPost;

public sealed class GetCustomCadPresignedUrlPostHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<GetCustomCadPresignedUrlPostQuery, UploadFileResponse>
{
	public async Task<UploadFileResponse> Handle(GetCustomCadPresignedUrlPostQuery req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.AcceptedCustom?.DesignerId != req.CallerId)
		{
			throw CustomAuthorizationException<Custom>.ById(custom.Id);
		}

		return await sender.SendQueryAsync(
			query: new GetCadPresignedUrlPostByIdQuery(
				Name: custom.Name,
				File: req.Cad
			),
			ct: ct
		).ConfigureAwait(false);
	}
}
