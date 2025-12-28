using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetAll;

namespace CustomCADs.Modules.Customs.API.Customs.Endpoints.Customers.Get.Recent;

public class RecentCustomsMapper : ResponseMapper<RecentCustomsResponse, GetAllCustomsDto>
{
	public override RecentCustomsResponse FromEntity(GetAllCustomsDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			OrderedAt: custom.OrderedAt,
			DesignerName: custom.DesignerName
		);
};
