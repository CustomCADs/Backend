using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;

using static CustomsData;

public class CustomCreateWitIdUnitTests : CustomsBaseUnitTests
{
	[Theory]
	[ClassData(typeof(Data.CustomCreateValidData))]
	public void CreateWithId_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
	{
		CreateCustomWithId(ValidId, name, description, delivery, ValidBuyerId);
	}

	[Theory]
	[ClassData(typeof(Data.CustomCreateValidData))]
	public void CreateWithId_ShouldPopulateProperties(string name, string description, bool forDelivery)
	{
		var custom = CreateCustomWithId(ValidId, name, description, forDelivery, ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, custom.Id),
			() => Assert.Equal(name, custom.Name),
			() => Assert.Equal(description, custom.Description),
			() => Assert.Equal(forDelivery, custom.ForDelivery),
			() => Assert.Equal(ValidBuyerId, custom.BuyerId)
		);
	}

	[Theory]
	[ClassData(typeof(Data.CustomCreateInvalidNameData))]
	[ClassData(typeof(Data.CustomCreateInvalidDescriptionData))]
	public void CreateWithId_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool delivery)
	{
		Assert.Throws<CustomValidationException<Custom>>((Action)(() =>
		{
			CustomsBaseUnitTests.CreateCustomWithId(CustomsData.ValidId, name, description, (bool?)delivery, ValidBuyerId);
			CreateCustom(name, description, delivery, ValidBuyerId);
		}));
	}
}
