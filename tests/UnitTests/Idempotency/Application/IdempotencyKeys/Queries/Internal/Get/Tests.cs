using CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;
using CustomCADs.Modules.Idempotency.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	private readonly GetIdempotencyKeyHandler handler;
	private readonly GetIdempotencyKeyQuery request = new(ValidId.Value, ValidRequestHash);

	private readonly Mock<IIdempotencyKeyReads> reads = new();

	private readonly IdempotencyKey idempotencyKey = CreateIdempotencyKey();

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, ValidRequestHash, false, ct))
			.ReturnsAsync(idempotencyKey);
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
				false,
				ct
			),
			Times.Once()
		);
	}

	[Test]
	[Arguments(false)]
	[Arguments(true)]
	public async Task Handle_ShouldReturnResult(bool isIdempotencyKeyCompleted)
	{
		// Arrange
		if (isIdempotencyKeyCompleted)
		{
			idempotencyKey.SetResponseBody(ValidResponseBody);
			idempotencyKey.SetStatusCode(MaxValidStatusCode);
		}

		// Act
		GetIdempotencyKeyDto? result = await handler.Handle(request, ct);

		// Assert
		if (isIdempotencyKeyCompleted)
		{
			using (Assert.Multiple())
			{
				await Assert.That(result!.ResponseBody).IsEqualTo(idempotencyKey.ResponseBody);
				await Assert.That(result!.StatusCode).IsEqualTo(idempotencyKey.StatusCode);
			}
		}
		else
		{
			await Assert.That(result).IsNull();
		}
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenIdempotencyKeyNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, ValidRequestHash, false, ct)).ReturnsAsync(null as IdempotencyKey);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<IdempotencyKey>>(() => handler.Handle(request, ct));
	}
}
