using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Remove;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Fact]
	public void Remove_ShouldSucceed_WhenReported()
	{
		Custom custom = CreateCustom();
		custom.Report();

		custom.Remove();

		Assert.Equal(CustomStatus.Removed, custom.CustomStatus);
	}

	[Fact]
	public void Remove_ShouldFail_WhenPending()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Remove();
		});
	}

	[Fact]
	public void Remove_ShouldFail_WhenAccepted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Remove();
		});
	}

	[Fact]
	public void Remove_ShouldFail_WhenBegun()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Remove();
		});
	}

	[Fact]
	public void Remove_ShouldFail_WhenFinished()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Remove();
		});
	}

	[Fact]
	public void Remove_ShouldFail_WhenCompleted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Remove();
		});
	}
}
