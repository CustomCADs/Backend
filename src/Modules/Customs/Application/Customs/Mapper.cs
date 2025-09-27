using CustomCADs.Customs.Application.Customs.Dtos;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Customs.Domain.Customs.States.Entities;
using CustomCADs.Customs.Domain.Customs.ValueObjects;

namespace CustomCADs.Customs.Application.Customs;

internal static class Mapper
{
	internal static GetAllCustomsDto ToGetAllDto(this Custom custom, string buyerName, string? designerName, string? categoryName)
		=> new(
			Id: custom.Id,
			Name: custom.Name,
			ForDelivery: custom.ForDelivery,
			CustomStatus: custom.CustomStatus,
			OrderedAt: custom.OrderedAt,
			BuyerName: buyerName,
			DesignerName: designerName,
			CategoryName: categoryName
		);

	internal static CustomerGetCustomByIdDto ToCustomerGetByIdDto(this Custom custom, CustomCategoryDto? category, AcceptedCustomDto? accepted)
		=> new(
			Id: custom.Id,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			CustomStatus: custom.CustomStatus,
			Category: category,
			AcceptedCustom: accepted,
			FinishedCustom: custom.FinishedCustom?.ToDto(),
			CompletedCustom: custom.CompletedCustom?.ToDto()
		);

	internal static DesignerGetCustomByIdDto ToDesignerGetByIdDto(this Custom custom, string buyer, AcceptedCustomDto? accepted, CustomCategoryDto? category)
		=> new(
			Id: custom.Id,
			Name: custom.Name,
			Description: custom.Description,
			OrderedAt: custom.OrderedAt,
			ForDelivery: custom.ForDelivery,
			CustomStatus: custom.CustomStatus,
			BuyerName: buyer,
			AcceptedCustom: accepted,
			Category: category,
			FinishedCustom: custom.FinishedCustom?.ToDto(),
			CompletedCustom: custom.CompletedCustom?.ToDto()
		);

	internal static CustomCategoryDto ToDto(this CustomCategory category, string name)
		=> new(
			Id: category.Id,
			Name: name,
			SetAt: category.SetAt,
			Setter: category.Setter
		);

	internal static AcceptedCustomDto ToDto(this AcceptedCustom custom, string designerName)
		=> new(
			DesignerName: designerName,
			AcceptedAt: custom.AcceptedAt
		);

	internal static FinishedCustomDto ToDto(this FinishedCustom custom)
		=> new(
			Price: custom.Price,
			FinishedAt: custom.FinishedAt,
			CadId: custom.CadId
		);

	internal static CompletedCustomDto ToDto(this CompletedCustom custom)
		=> new(
			PaymentStatus: custom.PaymentStatus,
			CustomizationId: custom.CustomizationId,
			ShipmentId: custom.ShipmentId
		);
}
