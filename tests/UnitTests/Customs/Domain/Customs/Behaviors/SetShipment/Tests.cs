using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetShipment;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly Custom custom = CreateCustom();

	public Tests()
	{
		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
		custom.Complete(customizationId: null);
	}

	[Test]
	public void SetShipment_ShouldNotThrowException_WhenCustomValid()
	{
		custom.SetShipment(ValidShipmentId);
	}

	[Test]
	public async Task SetShipment_ShouldPopulateProperties()
	{
		custom.SetShipment(ValidShipmentId);
		await Assert.That(custom.CompletedCustom!.ShipmentId).IsEqualTo(ValidShipmentId);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetShipment_ShouldThrowException_WhenInvalidState(Custom custom)
	{
		Assert.Throws<CustomValidationException<Custom>>(() =>
		{
			custom.SetShipment(ValidShipmentId);
		});
	}
}
