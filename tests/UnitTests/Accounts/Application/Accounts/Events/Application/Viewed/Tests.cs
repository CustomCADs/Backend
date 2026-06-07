using CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Catalog;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application.Viewed;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly ProductViewedHandler handler;
	private readonly ProductViewedApplicationEvent request = new(ValidProductId, ValidId, ViewedAt);

	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly DateTimeOffset ViewedAt = DateTimeOffset.UtcNow;

	public Tests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		writes.Verify(
			x => x.ViewProductAsync(ValidId, ValidProductId, ViewedAt, ct),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}
}
