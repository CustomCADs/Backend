using CustomCADs.Modules.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Account.Roles;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;

using static RolesData;

public class CreateRoleHandlerUnitTests : RolesBaseUnitTests
{
	private readonly CreateRoleHandler handler;
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRoleWrites> writes = new();

	private static readonly Role role = CreateRoleWithId(id: ValidId);

	public CreateRoleHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object, raiser.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Role>(x => x.Name == ValidName && x.Description == ValidDescription),
			ct
		)).ReturnsAsync(role);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateRoleCommand command = new(ValidName, ValidDescription);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Role>(x => x.Name == ValidName && x.Description == ValidDescription),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		CreateRoleCommand command = new(ValidName, ValidDescription);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.UpdateAsync(ValidId, role), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		CreateRoleCommand command = new(ValidName, ValidDescription);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<RoleCreatedApplicationEvent>(x => x.Name == ValidName && x.Description == ValidDescription)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateRoleCommand command = new(ValidName, ValidDescription);

		// Act
		RoleId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
