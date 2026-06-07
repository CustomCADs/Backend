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
		TrackViewedProducts: null
	);

	private readonly Mock<IAccountReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(CreateAccount());
	}

	[Fact]
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

	[Fact]
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

	[Fact]
	public async Task Handle_ShoulThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Account);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			() => handler.HandleAsync(request)
		);
	}
}
