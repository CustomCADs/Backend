using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Begin;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Fact]
	public void Begin_ShouldSucceed_WhenAccepted()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);

		custom.Begin();

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Begun, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom)
		);
	}

	[Fact]
	public void Begin_ShouldFail_WhenPending()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenBegun()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenReported()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenFinished()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenCompleted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Begin();
		});
	}

	[Fact]
	public void Begin_ShouldFail_WhenRemoved()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Report();
			custom.Remove();

			custom.Begin();
		});
	}
}
