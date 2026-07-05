using CustomCADs.Shared.Domain.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Create;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	[Test]
	public void Create_ShouldNotThrow()
	{
		CreateIdempotencyKey();
	}

	[Test]
	public async Task Create_ShouldPopulateProperties()
	{
		IdempotencyKey idempotencyKey = CreateIdempotencyKey();
		TimeSpan timeSinceCreation = DateTimeOffset.UtcNow - idempotencyKey.CreatedAt;

		using (Assert.Multiple())
		{
			await Assert.That(idempotencyKey.Id).IsEqualTo(ValidId);
			await Assert.That(idempotencyKey.RequestHash).IsEqualTo(ValidRequestHash);
			await Assert.That(timeSinceCreation < TimeSpan.FromSeconds(1)).IsTrue();
		}
	}

	[Test]
	[MethodDataSource(typeof(InvalidData), nameof(ITheoryData<>.GetTestData))]
	public void Create_ShouldThrow_WhenIdempotencyKeyInvalid(IdempotencyKeyId id, string requestHash)
	{
		Assert.Throws<CustomValidationException<IdempotencyKey>>(() => CreateIdempotencyKey(id, requestHash));
	}
}

