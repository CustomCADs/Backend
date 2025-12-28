using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses;

using static CustomsData;

public class CustomRemoveUnitTests : CustomsBaseUnitTests
{
	private static readonly Func<Action, InvalidOperationException> expectValidationException
		= Assert.Throws<InvalidOperationException>;

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
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Remove();
		});
	}

	[Fact]
	public void Remove_ShouldFail_WhenAccepted()
	{
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Remove();
		});
	}

	[Fact]
	public void Remove_ShouldFail_WhenBegun()
	{
		expectValidationException(() =>
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
		expectValidationException(() =>
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
		expectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Remove();
		});
	}
}
