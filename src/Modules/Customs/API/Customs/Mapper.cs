using CustomCADs.Modules.Customs.API.Customs.Dtos;
using CustomCADs.Modules.Customs.Application.Customs.Dtos;
using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Modules.Customs.API.Customs;

internal static class Mapper
{
	extension(PaymentDto payment)
	{
		internal PaymentResponse ToResponse()
			=> new(
				ClientSecret: payment.ClientSecret,
				Message: payment.Message
			);
	}

	extension(CustomCategoryDto category)
	{
		internal CustomCategoryResponse ToResponse()
			=> new(
				Id: category.Id.Value,
				Name: category.Name,
				SetAt: category.SetAt,
				Setter: category.Setter
			);
	}

	extension(AcceptedCustomDto custom)
	{
		internal AcceptedCustomResponse ToResponse()
			=> new(
				DesignerName: custom.DesignerName,
				AcceptedAt: custom.AcceptedAt
			);
	}

	extension(FinishedCustomDto custom)
	{
		internal FinishedCustomResponse ToResponse()
			=> new(
				Price: custom.Price,
				FinishedAt: custom.FinishedAt,
				CadId: custom.CadId.Value
			);
	}

	extension(CompletedCustomDto custom)
	{
		internal CompletedCustomResponse ToResponse()
			=> new(
				PaymentStatus: custom.PaymentStatus,
				CustomizationId: custom.CustomizationId?.Value,
				ShipmentId: custom.ShipmentId?.Value
			);
	}

}
