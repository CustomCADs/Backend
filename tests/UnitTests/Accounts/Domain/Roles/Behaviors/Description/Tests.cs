
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description;

public class Tests : Data.Roles.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid(string description)
	{
		var role = CreateRole();

		role.SetDescription(description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetDescription_SetsDescription_WhenDescriptionIsValid(string description)
	{
		var role = CreateRole();

		role.SetDescription(description);

		Assert.Equal(role.Description, description);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetDescription_ThrowsException_WhenDescriptionIsInvalid(string description)
	{
		var role = CreateRole();

		Assert.Throws<CustomValidationException<Role>>(
			() => role.SetDescription(description)
		);
	}
}
