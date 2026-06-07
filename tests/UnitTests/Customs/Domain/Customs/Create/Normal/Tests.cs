using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private static readonly (CategoryId, CustomCategorySetter) Category = (ValidCategoryId, CustomCategorySetter.Customer);

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
	{
		Custom.Create(name, description, delivery, ValidBuyerId, Category);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties(string name, string description, bool forDelivery)
	{
		var custom = Custom.Create(name, description, forDelivery, ValidBuyerId, Category);

		Assert.Multiple(
			() => Assert.Equal(name, custom.Name),
			() => Assert.Equal(description, custom.Description),
			() => Assert.Equal(forDelivery, custom.ForDelivery),
			() => Assert.Equal(ValidBuyerId, custom.BuyerId)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool delivery)
	{
		Assert.Throws<CustomValidationException<Custom>>(
			() => Custom.Create(name, description, delivery, ValidBuyerId, Category)
		);
	}
}
