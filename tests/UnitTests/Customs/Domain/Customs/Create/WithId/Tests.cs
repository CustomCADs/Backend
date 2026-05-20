using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
	{
		CreateCustom(name, description, delivery, ValidBuyerId);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties(string name, string description, bool forDelivery)
	{
		var custom = CreateCustom(name, description, forDelivery, ValidBuyerId);

		Assert.Multiple(
			() => Assert.Equal(ValidId, custom.Id),
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
		Assert.Throws<CustomValidationException<Custom>>((Action)(() =>
		{
			Data.Customs.BaseUnitTests.CreateCustom(name, description, (bool?)delivery, ValidBuyerId);
		}));
	}
}
