using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Customs.ValueObjects;
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Customs.Domain.Customs.Behaviors.SetCategory;

using static CustomsData;

public class CustomSetCategoryUnitTests : CustomsBaseUnitTests
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
		Custom custom = CreateCustomWithId();
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
}
