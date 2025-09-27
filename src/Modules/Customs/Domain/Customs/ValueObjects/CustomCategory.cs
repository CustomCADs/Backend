using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.Customs.Domain.Customs.ValueObjects;

public record CustomCategory(
	CategoryId Id,
	CustomId CustomId,
	DateTimeOffset SetAt,
	CustomCategorySetter Setter
)
{
	internal CustomCategory() : this(
		Id: CategoryId.New(),
		CustomId: CustomId.New(),
		SetAt: DateTimeOffset.UtcNow,
		Setter: CustomCategorySetter.Customer
	)
	{ }
}
