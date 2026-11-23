using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.FinishPayment;

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

	public static IEnumerable<bool> GetTestData() => [true, false];

	[Test]
	public void FinishPayment_ShouldNotThrowException()
	{
		custom.FinishPayment();
	}

	[Test]
	[MethodDataSource(nameof(GetTestData))]
	public async Task FinishPayment_ShouldPopulateProperties(bool success)
	{
		custom.FinishPayment(success);
		await Assert.That(custom.CompletedCustom!.PaymentStatus).IsEqualTo(success ? PaymentStatus.Completed : PaymentStatus.Failed);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void FinishPayment_ShouldThrowException_WhenInvalidState(Custom custom)
	{
		Assert.Throws<CustomValidationException<Custom>>(() =>
		{
			custom.FinishPayment();
		});
	}
}
