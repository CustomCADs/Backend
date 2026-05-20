
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal;

public class Tests : Data.Roles.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
	{
		Role.Create(name, description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties_WhenRoleIsValid(string name, string description)
	{
		var role = Role.Create(name, description);

		Assert.Multiple(
			() => Assert.Equal(role.Name, name),
			() => Assert.Equal(role.Description, description)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenRoleIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Role>>(
			() => Role.Create(name, description)
		);
	}
}
