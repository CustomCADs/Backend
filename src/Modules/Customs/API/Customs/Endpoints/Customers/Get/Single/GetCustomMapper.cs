using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.GetById;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Get.Single;

public class GetCustomsStatsMapper : ResponseMapper<GetCustomResponse, CustomerGetCustomByIdDto>
{
	public override GetCustomResponse FromEntity(CustomerGetCustomByIdDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus,
			Category: custom.Category?.ToResponse(),
			AcceptedCustom: custom.AcceptedCustom?.ToResponse(),
			FinishedCustom: custom.FinishedCustom?.ToResponse(),
			CompletedCustom: custom.CompletedCustom?.ToResponse()
		);
};
