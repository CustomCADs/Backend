
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Create.Normal;

public class Tests : Data.Roles.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldNotThrowException_WhenRoleIsValid(string name, string description)
	{
		Role.Create(name, description);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Create_ShouldPopulateProperties_WhenRoleIsValid(string name, string description)
	{
		var role = Role.Create(name, description);

		using (Assert.Multiple())
		{
			await Assert.That(name).IsEqualTo(role.Name);
			await Assert.That(description).IsEqualTo(role.Description);
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrowException_WhenRoleIsInvalid(string name, string description)
	{
		Assert.Throws<CustomValidationException<Role>>(() => Role.Create(name, description));
	}
}
