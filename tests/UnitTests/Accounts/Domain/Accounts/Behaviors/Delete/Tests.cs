namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.Delete;

public class Tests : Data.Accounts.BaseUnitTests
{
	[Test]
	public void Delete_ShouldNotThrowException()
	{
		Account account = CreateAccount();

		account.Delete();
	}

	[Test]
	public async Task Delete_ShouldEraseData()
	{
		Account account = CreateAccount();

		account.Delete();

		using (Assert.Multiple())
		{
			await Assert.That(account.IsDeleted).IsTrue();
			await Assert.That(account.DeletedAt).IsNotNull();

			await Assert.That(account.Username).IsEqualTo(account.Id.ToString());
			await Assert.That(account.Email).IsEqualTo(account.Id.ToString());

			await Assert.That(account.FirstName).IsNull();
			await Assert.That(account.LastName).IsNull();
		}
	}
}
