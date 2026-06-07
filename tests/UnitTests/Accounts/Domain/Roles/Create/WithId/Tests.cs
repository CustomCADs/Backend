using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.WithId;

using static Data.Roles.TestData;

public class Tests : Data.Roles.BaseUnitTests
{
	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
	{
		CreateRole(name, description);
	}

	[Theory]
	[ClassData(typeof(ValidData))]
	public void Create_ShouldPopulateProperties_WhenRoleIsValid(string name, string description)
	{
		var role = CreateRole(name, description, id: ValidId);

		Assert.Multiple(
			() => Assert.Equal(role.Id, ValidId),
			() => Assert.Equal(role.Name, name),
			() => Assert.Equal(role.Description, description)
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrowException_WhenRoleIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Role>>(
			() => CreateRole(name, description)
		);
	}
}
