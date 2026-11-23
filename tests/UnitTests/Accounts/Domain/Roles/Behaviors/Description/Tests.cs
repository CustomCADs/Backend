
using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Domain.Roles.Behaviors.Description;

public class Tests : Data.Roles.BaseUnitTests
{
	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ShouldNotThrowException_WhenDescriptionIsValid(string description)
	{
		var role = CreateRole();

		role.SetDescription(description);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task SetDescription_SetsDescription_WhenDescriptionIsValid(string description)
	{
		var role = CreateRole();

		role.SetDescription(description);

		await Assert.That(description).IsEqualTo(role.Description);
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void SetDescription_ThrowsException_WhenDescriptionIsInvalid(string description)
	{
		var role = CreateRole();

		Assert.Throws<CustomValidationException<Role>>(() => role.SetDescription(description));
	}
}
