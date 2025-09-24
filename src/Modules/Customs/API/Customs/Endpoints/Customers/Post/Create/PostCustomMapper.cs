using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Post.Create;

public class PostCustomMapper : ResponseMapper<PostCustomResponse, CustomerGetCustomByIdDto>
{
	public override PostCustomResponse FromEntity(CustomerGetCustomByIdDto custom)
		=> new(
			Id: custom.Id.Value,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			Status: custom.CustomStatus,
			Category: custom.Category?.ToResponse()
		);
};
