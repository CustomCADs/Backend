using CustomCADs.Modules.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application;

public class UserViewedProductHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly UserViewedProductHandler handler;
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly AccountId id = AccountId.New();
	private static readonly ProductId productId = ProductId.New();
	private static readonly DateTimeOffset viewedAt = DateTimeOffset.UtcNow;

	public UserViewedProductHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		UserViewedProductApplicationEvent ie = new(id, productId, viewedAt);

		// Act
		await handler.HandleAsync(ie);

		// Assert
		writes.Verify(x => x.ViewProductAsync(id, productId, viewedAt, ct), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}
}
