using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Internal.GetAll;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetAll;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAllAccountsHandler handler;
	private readonly GetAllAccountsQuery request = new(Query.Pagination);

	private readonly Mock<IAccountReads> reads = new();

	private static readonly Account[] Accounts = [
		CreateAccount(id: AccountId.New()),
		CreateAccount(id: AccountId.New()),
		CreateAccount(id: AccountId.New()),
		CreateAccount(id: AccountId.New()),
	];
	private static readonly AccountQuery Query = new(Pagination: new(1, Accounts.Length));

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.AllAsync(Query, false, ct))
			.ReturnsAsync(new Result<Account>(1, Accounts));
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(Query, false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		Result<GetAllAccountsDto> accounts = await handler.Handle(request, ct);

		// Assert
		await Assert.That(Accounts.Select(r => r.Id)).IsEquivalentTo(accounts.Items.Select(r => r.Id));
	}
}
