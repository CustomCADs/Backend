using CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;
using CustomCADs.Modules.Idempotency.Domain.Repositories;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	private readonly CreateIdempotencyKeyHandler handler;
	private readonly CreateIdempotencyKeyCommand request = new(
		IdempotencyKey: ValidId.Value,
		RequestHash: ValidRequestHash
	);

	private readonly Mock<IWrites<IdempotencyKey>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<IdempotencyKey>(x =>
				x.Id == ValidId
				&& x.RequestHash == ValidRequestHash
			),
			ct
		)).ReturnsAsync(CreateIdempotencyKey());
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
				It.Is<IdempotencyKey>(x =>
					x.Id == ValidId
					&& x.RequestHash == ValidRequestHash
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
		IdempotencyKeyId id = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}
}
