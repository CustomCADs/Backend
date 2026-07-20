using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetName;

using static Data.Customs.TestData;

public class ValidData : ITheoryData<Func<Custom>>
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
		var finished = CreateCustom();
		finished.Accept(ValidDesignerId);
		finished.Begin();
		finished.Finish(ValidCadId, ValidPrice);
		yield return () => finished;

		var completed = CreateCustom();
		completed.Accept(ValidDesignerId);
		completed.Begin();
		completed.Finish(ValidCadId, ValidPrice);
		completed.Complete(null);
		yield return () => completed;

		var reported = CreateCustom();
		reported.Report();
		yield return () => reported;

		var removed = CreateCustom();
		removed.Report();
		removed.Remove();
		yield return () => removed;
	}
}
