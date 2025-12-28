using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Designer.Get.All;

public class GetCustomsMapper : ResponseMapper<GetCustomsResponse, GetAllCustomsDto>
{
	public override GetCustomsResponse FromEntity(GetAllCustomsDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			OrderedAt: custom.OrderedAt,
			Status: custom.CustomStatus,
			ForDelivery: custom.ForDelivery,
			BuyerName: custom.BuyerName,
			CategoryName: custom.CategoryName
		);
};
