using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetCategory;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Test]
	public void SetCategory_ShouldNotThrowException()
	{
		Custom custom = CreateCustom();
		custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer));
	}

	[Test]
	public async Task SetCategory_ShouldPersistProperties()
	{
		Custom custom = CreateCustom();
		custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer));
		using (Assert.Multiple())
		{
			await Assert.That(custom.Id).IsEqualTo(ValidId);
			await Assert.That(custom.Category?.Id).IsEqualTo(ValidCategoryId);
			await Assert.That(custom.Category?.Setter).IsEqualTo(CustomCategorySetter.Customer);
		}
	}

	[Test]
	[Arguments(CustomCategorySetter.Customer, CustomCategorySetter.Customer)]
	[Arguments(CustomCategorySetter.Customer, CustomCategorySetter.Designer)]
	[Arguments(CustomCategorySetter.Customer, CustomCategorySetter.Admin)]
	[Arguments(CustomCategorySetter.Designer, CustomCategorySetter.Designer)]
	[Arguments(CustomCategorySetter.Designer, CustomCategorySetter.Admin)]
	[Arguments(CustomCategorySetter.Admin, CustomCategorySetter.Admin)]
	public void SetCategory_ShouldNotThrowException_WhenValidNewSetter(CustomCategorySetter oldSetter, CustomCategorySetter newSetter)
	{
		Custom custom = CreateCustom(categoryId: ValidCategoryId, setter: oldSetter);
		custom.SetCategory((ValidCategoryId, newSetter));
	}

	[Test]
	[Arguments(CustomCategorySetter.Designer, CustomCategorySetter.Customer)]
	[Arguments(CustomCategorySetter.Admin, CustomCategorySetter.Customer)]
	[Arguments(CustomCategorySetter.Admin, CustomCategorySetter.Designer)]
	public void SetCategory_ShouldThrowException_WhenInvalidNewSetter(CustomCategorySetter oldSetter, CustomCategorySetter newSetter)
	{
		Custom custom = CreateCustom(categoryId: ValidCategoryId, setter: oldSetter);
		Assert.Throws<CustomValidationException<CustomCategory>>(() => custom.SetCategory((ValidCategoryId, newSetter)));
	}

	[Test]
	[Arguments(CustomStatus.Pending)]
	[Arguments(CustomStatus.Accepted)]
	[Arguments(CustomStatus.Begun)]
	public void SetCategory_ShouldNotThrowException_WhenValidCustomStatus(CustomStatus status)
	{
		Custom custom = CreateCustomWithStatus(status);
		custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer));
	}

	[Test]
	[Arguments(CustomStatus.Reported)]
	[Arguments(CustomStatus.Removed)]
	[Arguments(CustomStatus.Finished)]
	[Arguments(CustomStatus.Completed)]
	public void SetCategory_ShouldThrowException_WhenInvalidCustomStatus(CustomStatus status)
	{
		Custom custom = CreateCustomWithStatus(status);

		Assert.Throws<CustomValidationException<Custom>>(() => custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer)));
	}

	private static Custom CreateCustomWithStatus(CustomStatus status)
	{
		Custom custom = CreateCustom();

		switch (status)
		{
			case CustomStatus.Pending:
				break;

			case CustomStatus.Accepted:
				custom.Accept(ValidDesignerId);
				break;

			case CustomStatus.Begun:
				custom.Accept(ValidDesignerId);
				custom.Begin();
				break;

			case CustomStatus.Finished:
				custom.Accept(ValidDesignerId);
				custom.Begin();
				custom.Finish(ValidCadId, ValidPrice);
				break;

			case CustomStatus.Completed:
				custom.Accept(ValidDesignerId);
				custom.Begin();
				custom.Finish(ValidCadId, ValidPrice);
				custom.Complete(customizationId: null);
				break;

			case CustomStatus.Reported:
				custom.Report();
				break;

			case CustomStatus.Removed:
				custom.Report();
				custom.Remove();
				break;
		}

		return custom;
	}
}
