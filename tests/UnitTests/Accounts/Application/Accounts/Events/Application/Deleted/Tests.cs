using CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Identity;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application.Deleted;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly UserDeletedHandler handler;
	private readonly UserDeletedApplicationEvent request = new(ValidId);

	private readonly Mock<IAccountReads> reads = new();
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Account account = CreateAccount();

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

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
		writes.Verify(
			x => x.Remove(account),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
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
