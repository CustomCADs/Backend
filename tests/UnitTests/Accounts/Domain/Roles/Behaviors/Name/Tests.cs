
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Name;

public class Tests : Data.Roles.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ShouldNotThrowException_WhenNameIsValid(string name)
	{
		var role = CreateRole();

		role.SetName(name);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetName_SetsName_WhenNameIsValid(string name)
	{
		var role = CreateRole();

		role.SetName(name);

		await Assert.That(name).IsEqualTo(role.Name);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetName_ThrowsException_WhenNameIsInvalid(string name)
	{
		var role = CreateRole();

		Assert.Throws<CustomValidationException<Role>>(() => role.SetName(name));
	}
}
