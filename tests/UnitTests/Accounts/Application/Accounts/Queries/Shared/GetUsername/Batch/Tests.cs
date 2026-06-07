using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.Username;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsername.Batch;

using static Data.Accounts.TestData;
using static DomainConstants.Users;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly BatchGetUsernamesByIdHandler handler;
	private readonly BatchGetUsernamesByIdQuery request = new(Ids);

	private readonly Mock<IAccountReads> reads = new();

	private static readonly AccountId[] Ids = [ValidId, ValidId, ValidId, ValidId];
	private static readonly string[] Usernames = [CustomerUsername, ContributorUsername, DesignerUsername, HeadDesignerUsername, AdminUsername];
	private static readonly AccountQuery Query = new(Pagination: new(1, Ids.Length), Ids: Ids);

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.AllAsync(Query, false, ct)).ReturnsAsync(new Result<Account>(
				Count: Ids.Length,
				Items: [
					CreateAccount(id: AccountId.New(), username: CustomerUsername),
					CreateAccount(id: AccountId.New(), username: ContributorUsername),
					CreateAccount(id: AccountId.New(), username: DesignerUsername),
					CreateAccount(id: AccountId.New(), username: HeadDesignerUsername),
					CreateAccount(id: AccountId.New(), username: AdminUsername),
				]
			));
	}

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		Dictionary<AccountId, string> result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(Usernames, result.Select(kvp => kvp.Value));
	}
}
