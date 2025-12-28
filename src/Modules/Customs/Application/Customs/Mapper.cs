using CustomCADs.Modules.Customs.Application.Customs.Dtos;
using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetAll;
using CustomCADs.Modules.Customs.Domain.Customs.States.Entities;
using CustomCADs.Modules.Customs.Domain.Customs.ValueObjects;

namespace CustomCADs.Modules.Customs.Application.Customs;

internal static class Mapper
{
	extension(Custom custom)
	{
		internal GetAllCustomsDto ToGetAllDto(string buyerName, string? designerName, string? categoryName)
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

		internal CustomerGetCustomByIdDto ToCustomerGetByIdDto(CustomCategoryDto? category, AcceptedCustomDto? accepted)
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

		internal DesignerGetCustomByIdDto ToDesignerGetByIdDto(string buyer, AcceptedCustomDto? accepted, CustomCategoryDto? category)
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
	}

	extension(CustomCategory category)
	{
		internal CustomCategoryDto ToDto(string name)
			=> new(
				Id: category.Id,
				Name: name,
				SetAt: category.SetAt,
				Setter: category.Setter
			);
	}

	extension(AcceptedCustom custom)
	{
		internal AcceptedCustomDto ToDto(string designerName)
			=> new(
				DesignerName: designerName,
				AcceptedAt: custom.AcceptedAt
			);
	}

	extension(FinishedCustom custom)
	{
		internal FinishedCustomDto ToDto()
			=> new(
				Price: custom.Price,
				FinishedAt: custom.FinishedAt,
				CadId: custom.CadId
			);
	}

	extension(CompletedCustom custom)
	{
		internal CompletedCustomDto ToDto()
			=> new(
				PaymentStatus: custom.PaymentStatus,
				CustomizationId: custom.CustomizationId,
				ShipmentId: custom.ShipmentId
			);
	}

}
