using CustomCADs.Modules.Accounts.Application.Roles.Queries.Internal.GetAll;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetAll;

public class Tests : Data.Roles.BaseUnitTests
{
	private readonly GetAllRolesHandler handler;
	private readonly GetAllRolesQuery request = new();

	private readonly Mock<IRoleReads> reads = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();

	private readonly Role[] roles = [
		CreateRole(),
		CreateRole(),
		CreateRole(),
		CreateRole(),
	];

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Role>>>>()))
			.Returns(async (Func<Task<ICollection<Role>>> factory) => await factory());

		reads.Setup(x => x.AllAsync(false, ct)).ReturnsAsync(roles);
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Role>>>>()),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(x => x.AllAsync(false, ct), Times.Once());
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result.Select(x => x.Id)).IsEquivalentTo(roles.Select(x => x.Id));
	}
}
