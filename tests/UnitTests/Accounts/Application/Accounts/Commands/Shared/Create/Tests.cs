using CustomCADs.Modules.Accounts.Application.Accounts.Commands.Shared;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Shared.Create;

using static Data.Accounts.TestData;

public class Tests : Data.Accounts.BaseUnitTests
{
	private readonly CreateAccountHandler handler;
	private readonly CreateAccountCommand request = new(
		Role: ValidRole,
		Username: ValidUsername,
		Email: ValidEmail1,
		FirstName: ValidFirstName,
		LastName: ValidLastName
	);

	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
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
		)).ReturnsAsync(CreateAccount(id: ValidId));
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Account>(x =>
					x.RoleName == ValidRole
					&& x.Username == ValidUsername
					&& x.Email == ValidEmail1
					&& x.FirstName == ValidFirstName
					&& x.LastName == ValidLastName
				),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		AccountId id = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
