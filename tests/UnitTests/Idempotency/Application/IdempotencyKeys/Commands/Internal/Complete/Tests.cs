using CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;
using CustomCADs.Modules.Idempotency.Domain.Repositories;
using CustomCADs.Modules.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	private readonly CompleteIdempotencyKeyHandler handler;
	private readonly CompleteIdempotencyKeyCommand request = new(
		Id: ValidId,
		RequestHash: ValidRequestHash,
		ResponseBody: ValidResponseBody,
		StatusCode: MaxValidStatusCode
	);

	private readonly Mock<IIdempotencyKeyReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(reads.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(
			ValidId,
			ValidRequestHash,
			true,
			ct
		)).ReturnsAsync(CreateIdempotencyKey());
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(
				ValidId,
				ValidRequestHash,
				true,
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenIdempotencyKeyNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, ValidRequestHash, true, ct)).ReturnsAsync(null as IdempotencyKey);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<IdempotencyKey>>(() => handler.Handle(request, ct));
	}
}
