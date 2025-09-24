using CustomCADs.Customs.Application.Customs.Dtos;
using CustomCADs.Customs.API.Customs.Dtos;
using CustomCADs.Customs.API.Customs.Endpoints.Designer.Patch.Finish;
using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Customs.API.Customs;

internal static class Mapper
{
	internal static (string Key, string ContentType, decimal Volume) ToTuple(this FinishCustomRequest req)
		=> (Key: req.CadKey, ContentType: req.CadContentType, Volume: req.CadVolume);

	internal static PaymentResponse ToResponse(this PaymentDto payment)
		=> new(
			ClientSecret: payment.ClientSecret,
			Message: payment.Message
		);

	internal static CustomCategoryResponse ToResponse(this CustomCategoryDto category)
		=> new(
			Id: category.Id.Value,
			Name: category.Name,
			SetAt: category.SetAt,
			Setter: category.Setter
		);

	internal static AcceptedCustomResponse ToResponse(this AcceptedCustomDto custom)
		=> new(
			DesignerName: custom.DesignerName,
			AcceptedAt: custom.AcceptedAt
		);

	internal static FinishedCustomResponse ToResponse(this FinishedCustomDto custom)
		=> new(
			Price: custom.Price,
			FinishedAt: custom.FinishedAt,
			CadId: custom.CadId.Value
		);

	internal static CompletedCustomResponse ToResponse(this CompletedCustomDto custom)
		=> new(
			PaymentStatus: custom.PaymentStatus,
			CustomizationId: custom.CustomizationId?.Value,
			ShipmentId: custom.ShipmentId?.Value
		);
}
