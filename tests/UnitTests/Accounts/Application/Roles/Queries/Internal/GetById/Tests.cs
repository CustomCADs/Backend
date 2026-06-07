using CustomCADs.Modules.Accounts.Application.Roles.Queries.Internal.GetById;
using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetById;

using static Data.Roles.TestData;

public class Tests : Data.Roles.BaseUnitTests
{
	private readonly GetRoleByIdHandler handler;
	private readonly GetRoleByIdQuery request = new(ValidId);

	private readonly Mock<IRoleReads> reads = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Role>>>()
		)).ReturnsAsync(CreateRole(id: ValidId));
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
				ValidId,
				It.IsAny<Func<Task<Role>>>()
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
		Assert.Equal(ValidId, result.Id);
	}
}
