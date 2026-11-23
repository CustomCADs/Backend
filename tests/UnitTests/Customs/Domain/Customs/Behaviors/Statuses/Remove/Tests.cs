using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Remove;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public async Task Remove_ShouldSucceed_WhenReported()
	{
		Custom custom = CreateCustom();
		custom.Report();

		custom.Remove();

		await Assert.That(custom.CustomStatus).IsEqualTo(CustomStatus.Removed);
	}

	[Test]
	public void Remove_ShouldFail_WhenPending()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();

			custom.Remove();
		});
	}

	[Test]
	public void Remove_ShouldFail_WhenAccepted()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Remove();
		});
	}

	[Test]
	public void Remove_ShouldFail_WhenBegun()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Remove();
		});
	}

	[Test]
	public void Remove_ShouldFail_WhenFinished()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Remove();
		});
	}

	[Test]
	public void Remove_ShouldFail_WhenCompleted()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Remove();
		});
	}
}
