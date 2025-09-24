using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;

namespace CustomCADs.Customs.API.Customs.Endpoints.Designer.Get.Single;

public class GetCustomMapper : ResponseMapper<GetCustomResponse, DesignerGetCustomByIdDto>
{
	public override GetCustomResponse FromEntity(DesignerGetCustomByIdDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus,
			BuyerName: custom.BuyerName,
			Category: custom.Category?.ToResponse(),
			AcceptedCustom: custom.AcceptedCustom?.ToResponse(),
			FinishedCustom: custom.FinishedCustom?.ToResponse(),
			CompletedCustom: custom.CompletedCustom?.ToResponse()
		);
};
