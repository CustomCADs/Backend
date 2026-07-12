using CustomCADs.Modules.Accounts.Application.Accounts.Queries.Shared.Id;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.IdsByRole;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly GetAccountIdsByRoleHandler handler;
	private readonly GetAccountIdsByRoleQuery request = new(ValidRole);

	private readonly Mock<IAccountReads> reads = new();

	private static readonly AccountId[] AccountIds = [
		AccountId.New(),
		AccountId.New(),
		AccountId.New(),
	];

	public Tests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.AllIdsByRoleAsync(ValidRole, ct)).ReturnsAsync(AccountIds);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AllIdsByRoleAsync(ValidRole, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		AccountId[] ids = [.. await handler.Handle(request, ct)];

		// Assert
		await Assert.That(ids).IsEquivalentTo(AccountIds);
	}
}
