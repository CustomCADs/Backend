using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.All;

public class GetCustomsMapper : ResponseMapper<GetCustomsResponse, GetAllCustomsDto>
{
	public override GetCustomsResponse FromEntity(GetAllCustomsDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus,
			DesignerName: custom.DesignerName,
			CategoryName: custom.CategoryName
		);
};
