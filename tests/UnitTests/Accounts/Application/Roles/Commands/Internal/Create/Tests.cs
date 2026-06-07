using CustomCADs.Modules.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Modules.Accounts.Domain.Repositories;
using CustomCADs.Modules.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Account.Roles;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;

using static Data.Roles.TestData;

public class Tests : Data.Roles.BaseUnitTests
{
	private readonly CreateRoleHandler handler;
	private readonly CreateRoleCommand request = new(ValidName, ValidDescription);

	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRoleWrites> writes = new();

	private readonly Role role = CreateRole(id: ValidId);

	public Tests()
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

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Role>(x => x.Name == ValidName && x.Description == ValidDescription),
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
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, role),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<RoleCreatedApplicationEvent>(x => x.Name == ValidName && x.Description == ValidDescription)
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		RoleId id = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
