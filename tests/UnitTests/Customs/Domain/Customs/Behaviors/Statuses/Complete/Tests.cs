using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Complete;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public async Task Complete_ShouldSucceed_WhenFinished()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);

		custom.Complete(customizationId: null);

		using (Assert.Multiple())
		{
			await Assert.That(custom.CustomStatus).IsEqualTo(CustomStatus.Completed);
			await Assert.That(custom.AcceptedCustom).IsNotNull();
			await Assert.That(custom.FinishedCustom).IsNotNull();
			await Assert.That(custom.CompletedCustom).IsNotNull();
			await Assert.That(custom.CompletedCustom!.CustomizationId).IsNull();
		}
	}

	[Test]
	public void Complete_ShouldFail_WhenPending()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Complete(customizationId: null);
		});
	}

	[Test]
	public void Complete_ShouldFail_WhenAccepted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Complete(customizationId: null);
		});
	}

	[Test]
	public void Complete_ShouldFail_WhenBegun()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Complete(customizationId: null);
		});
	}

	[Test]
	public void Complete_ShouldFail_WhenReported()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Complete(customizationId: null);
		});
	}

	[Test]
	public void Complete_ShouldFail_WhenCompleted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(customizationId: null);

			custom.Complete(customizationId: null);
		});
	}

	[Test]
	public void Complete_ShouldFail_WhenRemoved()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Report();
			custom.Remove();

			custom.Complete(customizationId: null);
		});
	}
}
