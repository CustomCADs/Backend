using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetCategory;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	[Fact]
	public void SetCategory_ShouldNotThrowException()
	{
		Custom custom = CreateCustom();
		custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer));
	}

	[Fact]
	public void SetCategory_ShouldPersistProperties()
	{
		Custom custom = CreateCustom();
		custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer));
		Assert.Multiple(
			() => Assert.Equal(ValidCategoryId, custom.Category?.Id),
			() => Assert.Equal(ValidId, custom.Category?.CustomId),
			() => Assert.Equal(CustomCategorySetter.Customer, custom.Category?.Setter)
		);
	}

	[Theory]
	[InlineData(CustomCategorySetter.Customer, CustomCategorySetter.Customer)]
	[InlineData(CustomCategorySetter.Customer, CustomCategorySetter.Designer)]
	[InlineData(CustomCategorySetter.Customer, CustomCategorySetter.Admin)]
	[InlineData(CustomCategorySetter.Designer, CustomCategorySetter.Designer)]
	[InlineData(CustomCategorySetter.Designer, CustomCategorySetter.Admin)]
	[InlineData(CustomCategorySetter.Admin, CustomCategorySetter.Admin)]
	public void SetCategory_ShouldNotThrowException_WhenValidNewSetter(CustomCategorySetter oldSetter, CustomCategorySetter newSetter)
	{
		Custom custom = CreateCustom(categoryId: ValidCategoryId, setter: oldSetter);
		custom.SetCategory((ValidCategoryId, newSetter));
	}

	[Theory]
	[InlineData(CustomCategorySetter.Designer, CustomCategorySetter.Customer)]
	[InlineData(CustomCategorySetter.Admin, CustomCategorySetter.Customer)]
	[InlineData(CustomCategorySetter.Admin, CustomCategorySetter.Designer)]
	public void SetCategory_ShouldThrowException_WhenInvalidNewSetter(CustomCategorySetter oldSetter, CustomCategorySetter newSetter)
	{
		Custom custom = CreateCustom(categoryId: ValidCategoryId, setter: oldSetter);
		Assert.Throws<CustomValidationException<CustomCategory>>(
			() => custom.SetCategory((ValidCategoryId, newSetter))
		);
	}

	[Theory]
	[InlineData(CustomStatus.Pending)]
	[InlineData(CustomStatus.Accepted)]
	[InlineData(CustomStatus.Begun)]
	public void SetCategory_ShouldNotThrowException_WhenValidCustomStatus(CustomStatus status)
	{
		Custom custom = CreateCustomWithStatus(status);
		custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer));
	}

	[Theory]
	[InlineData(CustomStatus.Reported)]
	[InlineData(CustomStatus.Removed)]
	[InlineData(CustomStatus.Finished)]
	[InlineData(CustomStatus.Completed)]
	public void SetCategory_ShouldThrowException_WhenInvalidCustomStatus(CustomStatus status)
	{
		Custom custom = CreateCustomWithStatus(status);

		Assert.Throws<CustomValidationException<Custom>>(
			() => custom.SetCategory((ValidCategoryId, CustomCategorySetter.Customer))
		);
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
