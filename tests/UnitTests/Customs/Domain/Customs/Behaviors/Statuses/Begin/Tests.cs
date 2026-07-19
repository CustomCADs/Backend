using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Begin;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public async Task Begin_ShouldSucceed_WhenAccepted()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);

		custom.Begin();

		using (Assert.Multiple())
		{
			await Assert.That(custom.CustomStatus).IsEqualTo(CustomStatus.Begun);
			await Assert.That(custom.AcceptedCustom).IsNotNull();
		}
	}

	[Test]
	public void Begin_ShouldFail_WhenPending()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();

			custom.Begin();
		});
	}

	[Test]
	public void Begin_ShouldFail_WhenBegun()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Begin();
		});
	}

	[Test]
	public void Begin_ShouldFail_WhenReported()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Begin();
		});
	}

	[Test]
	public void Begin_ShouldFail_WhenFinished()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Begin();
		});
	}

	[Test]
	public void Begin_ShouldFail_WhenCompleted()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Begin();
		});
	}

	[Test]
	public void Begin_ShouldFail_WhenRemoved()
	{
		Assert.Throws<InvalidOperationException>(() =>
		{
			Custom custom = CreateCustom();
			custom.Report();
			custom.Remove();

			custom.Begin();
		});
	}
}
