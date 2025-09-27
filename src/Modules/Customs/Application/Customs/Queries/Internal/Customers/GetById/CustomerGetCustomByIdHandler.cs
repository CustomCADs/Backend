using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

public sealed class CustomerGetCustomByIdHandler(ICustomReads reads, IRequestSender sender)
	: IQueryHandler<CustomerGetCustomByIdQuery, CustomerGetCustomByIdDto>
{
	public async Task<CustomerGetCustomByIdDto> Handle(CustomerGetCustomByIdQuery req, CancellationToken ct)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, track: false, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.BuyerId != req.CallerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		return custom.ToCustomerGetByIdDto(
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
