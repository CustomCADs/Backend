using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetReference;

namespace CustomCADs.Modules.Delivery.API.Shipments.Endpoints.Queries.Get.Reference;

public class GetReferenceMapper : ResponseMapper<GetReferenceResponse, GetReferenceDto>
{
	public override GetReferenceResponse FromEntity(GetReferenceDto reference)
		=> new(
			Id: reference.Id,
			Service: reference.Service
		);
}
