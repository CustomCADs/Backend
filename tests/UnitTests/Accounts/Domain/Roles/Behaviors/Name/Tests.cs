
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name;

public class Tests : Data.Roles.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_ShouldNotThrowException_WhenNameIsValid(string name)
	{
		var role = CreateRole();

		role.SetName(name);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void SetName_SetsName_WhenNameIsValid(string name)
	{
		var role = CreateRole();

		role.SetName(name);

		Assert.Equal(role.Name, name);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void SetName_ThrowsException_WhenNameIsInvalid(string name)
	{
		var role = CreateRole();

		Assert.Throws<CustomValidationException<Role>>(
			() => role.SetName(name)
		);
	}
}
