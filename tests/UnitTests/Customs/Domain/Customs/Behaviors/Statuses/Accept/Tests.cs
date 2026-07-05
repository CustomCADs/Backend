using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Accept;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public async Task Accept_ShouldSucceed_WhenPending()
	{
		Custom custom = CreateCustom();

		custom.Accept(ValidDesignerId);

		using (Assert.Multiple())
		{
			await Assert.That(custom.CustomStatus).IsEqualTo(CustomStatus.Accepted);
			await Assert.That(custom.AcceptedCustom).IsNotNull();
		}
	}

	[Test]
	public void Accept_ShouldFail_WhenAccepted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Accept(ValidDesignerId);
		});
	}

	[Test]
	public void Accept_ShouldFail_WhenBegun()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Accept(ValidDesignerId);
		});
	}

	[Test]
	public void Accept_ShouldFail_WhenReported()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Accept(ValidDesignerId);
		});
	}

	[Test]
	public void Accept_ShouldFail_WhenFinished()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Accept(ValidDesignerId);
		});
	}

	[Test]
	public void Accept_ShouldFail_WhenCompleted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Accept(ValidDesignerId);
		});
	}

	[Test]
	public void Accept_ShouldFail_WhenRemoved()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Report();
			custom.Remove();

			custom.Accept(ValidDesignerId);
		});
	}
}
