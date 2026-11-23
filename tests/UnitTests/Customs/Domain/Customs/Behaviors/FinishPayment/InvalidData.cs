using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.FinishPayment;

using static Data.Customs.TestData;

public class InvalidData : ITheoryData<Func<Custom>>
{
	private static Custom CreateCustom() => Custom.Create(
		name: MinValidName,
		description: MinValidDescription,
		forDelivery: false,
		buyerId: ValidBuyerId,
		category: (ValidCategoryId, CustomCategorySetter.Customer)
	);

	public static IEnumerable<Func<Custom>> GetTestData()
	{
		var pending = CreateCustom();
		yield return () => pending;

		var accepted = CreateCustom();
		accepted.Accept(ValidDesignerId);
		yield return () => accepted;

		var begun = CreateCustom();
		begun.Accept(ValidDesignerId);
		begun.Begin();
		yield return () => begun;

		var finished = CreateCustom();
		finished.Accept(ValidDesignerId);
		finished.Begin();
		finished.Finish(ValidCadId, ValidPrice);
		yield return () => finished;

		var reported = CreateCustom();
		reported.Report();
		yield return () => reported;

		var removed = CreateCustom();
		removed.Report();
		removed.Remove();
		yield return () => removed;
	}
}
