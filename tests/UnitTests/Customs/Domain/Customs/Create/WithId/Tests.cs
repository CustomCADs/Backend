using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.WithId;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
	{
		CreateCustom(name, description, delivery);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties(string name, string description, bool forDelivery)
	{
		var custom = CreateCustom(name, description, forDelivery);

		using (Assert.Multiple())
		{
			await Assert.That(custom.Id).IsEqualTo(ValidId);
			await Assert.That(custom.Name).IsEqualTo(name);
			await Assert.That(custom.Description).IsEqualTo(description);
			await Assert.That(custom.ForDelivery).IsEqualTo(forDelivery);
			await Assert.That(custom.BuyerId).IsEqualTo(ValidBuyerId);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool forDelivery)
	{
		Assert.Throws<CustomValidationException<Custom>>(() =>
		{
			CreateCustom(name, description, forDelivery);
		});
	}
}
