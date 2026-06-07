using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Create;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrow()
	{
		CreateIdempotencyKey();
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		IdempotencyKey idempotencyKey = CreateIdempotencyKey();
		TimeSpan timeSinceCreation = DateTimeOffset.UtcNow - idempotencyKey.CreatedAt;

		Assert.Multiple(
			() => Assert.Equal(ValidId, idempotencyKey.Id),
			() => Assert.Equal(ValidRequestHash, idempotencyKey.RequestHash),
			() => Assert.True(timeSinceCreation < TimeSpan.FromSeconds(1))
		);
	}

	[Theory]
	[ClassData(typeof(InvalidData))]
	public void Create_ShouldThrow_WhenIdempotencyKeyInvalid(IdempotencyKeyId id, string requestHash)
	{
		Assert.Throws<CustomValidationException<IdempotencyKey>>(
			() => CreateIdempotencyKey(id, requestHash)
		);
	}
}
