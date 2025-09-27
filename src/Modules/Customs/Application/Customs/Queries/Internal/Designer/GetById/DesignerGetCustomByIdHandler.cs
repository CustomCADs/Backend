using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

public sealed class DesignerGetCustomByIdHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<DesignerGetCustomByIdQuery, DesignerGetCustomByIdDto>
{
	public async Task<DesignerGetCustomByIdDto> Handle(DesignerGetCustomByIdQuery req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.CustomStatus is not CustomStatus.Pending
			&& custom.AcceptedCustom?.DesignerId != req.CallerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		return custom.ToDesignerGetByIdDto(
			buyer: await sender.SendQueryAsync(
				new GetUsernameByIdQuery(custom.BuyerId),
				ct: ct
			).ConfigureAwait(false),
			category: custom.Category?.ToDto(
				name: await sender.SendQueryAsync(
					query: new GetCategoryNameByIdQuery(custom.Category.Id),
					ct: ct
				).ConfigureAwait(false)
			),
			accepted: custom.AcceptedCustom?.ToDto(
				designerName: await sender.SendQueryAsync(
					new GetUsernameByIdQuery(custom.AcceptedCustom.DesignerId),
					ct: ct
				).ConfigureAwait(false)
			)
		);
	}
}
