using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetDelivery;

public class Tests : Data.Customs.BaseUnitTests
{
	public static IEnumerable<bool> GetTestData() => [true, false];

	[Test]
	[MethodDataSource(nameof(GetTestData))]
	public void SetDelivery_ShouldNotThrowException_WhenCustomValid(bool forDelivery)
	{
		CreateCustom().SetDelivery(forDelivery);
	}

	[Test]
	[MethodDataSource(nameof(GetTestData))]
	public async Task SetDelivery_ShouldPopulateProperties(bool forDelivery)
	{
		var custom = CreateCustom();
		custom.SetDelivery(forDelivery);
		await Assert.That(custom.ForDelivery).IsEqualTo(forDelivery);
	}

	[Test]
	[MethodDataSource(nameof(GetTestData))]
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
