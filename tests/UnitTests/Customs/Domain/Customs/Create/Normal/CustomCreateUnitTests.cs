namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;

using CustomCADs.Shared.Domain.Exceptions;
using Data;
using static CustomsData;

public class CustomCreateUnitTests : CustomsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(CustomCreateValidData))]
	public void Create_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
	{
		CreateCustom(name, description, delivery, ValidBuyerId);
	}

	[Theory]
	[ClassData(typeof(CustomCreateValidData))]
	public void Create_ShouldPopulateProperties(string name, string description, bool forDelivery)
	{
		var custom = CreateCustom(name, description, forDelivery, ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(name, custom.Name),
			() => Assert.Equal(description, custom.Description),
			() => Assert.Equal(forDelivery, custom.ForDelivery),
			() => Assert.Equal(ValidBuyerId, custom.BuyerId)
		);
	}

	[Theory]
	[ClassData(typeof(CustomCreateInvalidNameData))]
	[ClassData(typeof(CustomCreateInvalidDescriptionData))]
	public void Create_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool delivery)
	{
		Assert.Throws<CustomValidationException<Custom>>(
			() => CreateCustom(name, description, delivery, ValidBuyerId)
		);
	}
}
