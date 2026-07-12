using CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Events.Identity;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application.Edited;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly UserEditedHandler handler;
	private readonly UserEditedApplicationEvent request = new(
		Id: ValidId,
		Username: null,
		Names: null,
		TrackViewedProducts: null
	);

	private readonly Mock<IAccountReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Account account = CreateAccount();

	public Tests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(account);
	}

	[Test]
	public async Task Handle_ShoulQueryDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShoulPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	[MethodDataSource(typeof(ValidData), nameof(ITheoryData<>.GetTestData))]
	public async Task Handle_ShouldApplyChanges(Theory theory)
	{
		// Arrange
		Theory oldAccount = new(
			Username: account.Username,
			Names: new(account.FirstName, account.LastName),
			Track: account.TrackViewedProducts
		);

		// Act
		await handler.HandleAsync(
			request with { Username = theory.Username, Names = theory.Names, TrackViewedProducts = theory.Track }
		);

		// Assert
		using (Assert.Multiple())
		{
			if (theory.Username is null) await Assert.That(account.Username).IsEqualTo(oldAccount.Username);
			else await Assert.That(account.Username).IsEqualTo(theory.Username);

			if (theory.Track is null) await Assert.That(account.TrackViewedProducts).IsEqualTo(oldAccount.Track);
			else await Assert.That(account.TrackViewedProducts).IsEqualTo(theory.Track);

			if (theory.Names is null)
			{
				using (Assert.Multiple())
				{
					await Assert.That(account.FirstName).IsEqualTo(oldAccount.Names!.FirstName);
					await Assert.That(account.LastName).IsEqualTo(oldAccount.Names!.LastName);
				}
			}
			else
			{
				await Assert.That(account.FirstName).IsEqualTo(theory.Names.FirstName);
				await Assert.That(account.LastName).IsEqualTo(theory.Names.LastName);
			}
		}
	}

	[Test]
	public async Task Handle_ShoulThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Account);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(() => handler.HandleAsync(request));
	}
}
