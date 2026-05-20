using CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application.Viewed;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly UserViewedProductHandler handler;
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly DateTimeOffset viewedAt = DateTimeOffset.UtcNow;

	public Tests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductViewedApplicationEvent @event = new(ValidProductId, ValidId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		writes.Verify(
			x => x.ViewProductAsync(ValidId, ValidProductId, viewedAt, ct),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}
}
