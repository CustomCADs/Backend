using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Create.Normal;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private static readonly (CategoryId, CustomCategorySetter) Category = (ValidCategoryId, CustomCategorySetter.Customer);

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenCustomIsValid(string name, string description, bool delivery)
	{
		Custom.Create(name, description, delivery, ValidBuyerId, Category);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties(string name, string description, bool forDelivery)
	{
		var custom = Custom.Create(name, description, forDelivery, ValidBuyerId, Category);

		using (Assert.Multiple())
		{
			await Assert.That(custom.Name).IsEqualTo(name);
			await Assert.That(custom.Description).IsEqualTo(description);
			await Assert.That(custom.ForDelivery).IsEqualTo(forDelivery);
			await Assert.That(custom.BuyerId).IsEqualTo(ValidBuyerId);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenCustomIsInvalid(string name, string description, bool delivery)
	{
		Assert.Throws<CustomValidationException<Custom>>(() => Custom.Create(name, description, delivery, ValidBuyerId, Category));
	}
}
