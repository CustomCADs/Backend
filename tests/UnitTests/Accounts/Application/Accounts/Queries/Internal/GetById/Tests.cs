using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetById;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetById;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountByIdHandler handler;
	private readonly GetAccountByIdQuery request = new(ValidId);

	private readonly Mock<IAccountReads> reads = new();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateAccount(id: ValidId));
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result.Id).IsEqualTo(ValidId);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenAccountNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Account);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(() => handler.Handle(request, ct));
	}
}
