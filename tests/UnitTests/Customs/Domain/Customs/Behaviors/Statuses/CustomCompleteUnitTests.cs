using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomCompleteUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, InvalidOperationException> expectValidationException
		= Assert.Throws<InvalidOperationException>;

	[Fact]
	public void Complete_ShouldSucceed_WhenFinished()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);

		custom.Complete(customizationId: null);

		Assert.Multiple(
			() => Assert.Equal(CustomStatus.Completed, custom.CustomStatus),
			() => Assert.NotNull(custom.AcceptedCustom),
			() => Assert.NotNull(custom.FinishedCustom),
			() => Assert.NotNull(custom.CompletedCustom),
			() => Assert.Null(custom.CompletedCustom!.CustomizationId)
		);
	}

	[Fact]
	public void Complete_ShouldFail_WhenPending()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Complete(customizationId: null);
		});
	}

	[Fact]
	public void Complete_ShouldFail_WhenAccepted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Complete(customizationId: null);
		});
	}

	[Fact]
	public void Complete_ShouldFail_WhenBegun()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();

			custom.Complete(customizationId: null);
		});
	}

	[Fact]
	public void Complete_ShouldFail_WhenReported()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Complete(customizationId: null);
		});
	}

	[Fact]
	public void Complete_ShouldFail_WhenCompleted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(customizationId: null);

			custom.Complete(customizationId: null);
		});
	}

	[Fact]
	public void Complete_ShouldFail_WhenRemoved()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Report();
			custom.Remove();

			custom.Complete(customizationId: null);
		});
	}
}
