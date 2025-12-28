using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Admins.Get.All;

public class GetCustomsMapper : ResponseMapper<GetCustomsResponse, GetAllCustomsDto>
{
	public override GetCustomsResponse FromEntity(GetAllCustomsDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus,
			OrderedAt: custom.OrderedAt,
			BuyerName: custom.BuyerName,
			DesignerName: custom.DesignerName,
			CategoryName: custom.CategoryName
		);
};
