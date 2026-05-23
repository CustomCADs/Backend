using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Report;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Fact]
	public void Report_ShouldSucceed_WhenPending()
	{
		Custom custom = CreateCustom();

		custom.Report();

		Assert.Equal(CustomStatus.Reported, custom.CustomStatus);
	}

	[Fact]
	public void Report_ShouldSucceed_WhenAccepted()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);

		custom.Report();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Reported, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Report_ShouldSucceed_WhenBegun()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Begin();

		custom.Report();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Reported, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Report_ShouldFail_WhenReported()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Report();
		});
	}

	[Fact]
	public void Report_ShouldFail_WhenFinished()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Report();
		});
	}

	[Fact]
	public void Report_ShouldFail_WhenCompleted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Report();
		});
	}

	[Fact]
	public void Report_ShouldFail_WhenRemoved()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Report();
			custom.Remove();

			custom.Report();
		});
	}
}
