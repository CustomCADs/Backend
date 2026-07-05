using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.Statuses.Finish;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public async Task Finish_ShouldSucceed_WhenBegun()
	{
		Custom custom = CreateCustom();
		custom.Accept(ValidDesignerId);
		custom.Begin();

		custom.Finish(ValidCadId, ValidPrice);


		using (Assert.Multiple())
		{
			await Assert.That(custom.CustomStatus).IsEqualTo(CustomStatus.Finished);
			await Assert.That(custom.AcceptedCustom).IsNotNull();
			await Assert.That(custom.FinishedCustom).IsNotNull();
			await Assert.That(custom.FinishedCustom.CadId).IsEqualTo(ValidCadId);
			await Assert.That(custom.FinishedCustom.Price).IsEqualTo(ValidPrice);
		}
	}

	[Test]
	public void Finish_ShouldFail_WhenInvalidPrice()
	{
		Assert.Throws<CustomValidationException<FinishedCustom>>(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, InvalidPrice);
		});
	}

	[Test]
	public void Finish_ShouldFail_WhenPending()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Test]
	public void Finish_ShouldFail_WhenAccepted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Test]
	public void Finish_ShouldFail_WhenReported()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Report();

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Test]
	public void Finish_ShouldFail_WhenFinished()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Test]
	public void Finish_ShouldFail_WhenCompleted()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Accept(ValidDesignerId);
			custom.Begin();
			custom.Finish(ValidCadId, ValidPrice);
			custom.Complete(ValidCustomizationId);

			custom.Finish(ValidCadId, ValidPrice);
		});
	}

	[Test]
	public void Finish_ShouldFail_WhenRemoved()
	{
		ExpectValidationException(() =>
		{
			Custom custom = CreateCustom();
			custom.Report();
			custom.Remove();

			custom.Finish(ValidCadId, ValidPrice);
		});
	}
}
