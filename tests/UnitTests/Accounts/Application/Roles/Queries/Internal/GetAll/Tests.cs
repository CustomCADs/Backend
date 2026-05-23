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

		cache.Setup(x => x.GetOrCreateAsync(
			It.IsAny<Func<Task<ICollection<Role>>>>())
		).ReturnsAsync(roles);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(
				It.IsAny<Func<Task<ICollection<Role>>>>()
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(roles.Select(x => x.Id), result.Select(x => x.Id));
	}
}
