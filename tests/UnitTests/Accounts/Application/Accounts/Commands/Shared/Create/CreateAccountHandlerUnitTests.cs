using CustomCADs.Modules.Accounts.Application.Accounts.Commands.Shared;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

using static CustomCADs.UnitTests.Accounts.Data.AccountsData;
using static CustomCADs.Shared.Domain.DomainConstants;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly CreateAccountHandler handler;
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public CreateAccountHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Account>(x =>
				x.RoleName == ValidRole
				&& x.Username == ValidUsername
				&& x.Email == ValidEmail1
				&& x.FirstName == ValidFirstName
				&& x.LastName == ValidLastName
			),
			ct
		)).ReturnsAsync(CreateAccountWithId(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateAccountCommand command = new(
			Role: ValidRole,
			Username: ValidUsername,
			Email: ValidEmail1,
			FirstName: ValidFirstName,
			LastName: ValidLastName
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Account>(x =>
				x.RoleName == ValidRole
				&& x.Username == ValidUsername
				&& x.Email == ValidEmail1
				&& x.FirstName == ValidFirstName
				&& x.LastName == ValidLastName
			),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateAccountCommand command = new(
			Role: ValidRole,
			Username: ValidUsername,
			Email: ValidEmail1,
			FirstName: ValidFirstName,
			LastName: ValidLastName
		);

		// Act
		AccountId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
