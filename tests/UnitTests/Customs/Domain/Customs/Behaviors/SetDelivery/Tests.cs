using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDelivery;

public class Tests : Data.Customs.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDelivery_ShouldNotThrowException_WhenCustomValid(bool forDelivery)
	{
		CreateCustom().SetDelivery(forDelivery);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDelivery_ShouldPopulateProperties(bool forDelivery)
	{
		var Custom = CreateCustom();
		Custom.SetDelivery(forDelivery);
		Assert.Equal(forDelivery, Custom.ForDelivery);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDelivery_ShouldThrowException_WhenNameInvalid(bool forDelivery)
	{
		Assert.Throws<CustomValidationException<Custom>>(() =>
		{
			var custom = CreateCustom();
			custom.Report();
			custom.SetDelivery(forDelivery);
		});
	}
}
