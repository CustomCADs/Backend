using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Behaviors.SetStatusCode;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	[Test]
	public void SetStatusCode_ShouldNotThrow()
	{
		CreateIdempotencyKey().SetStatusCode(MaxValidStatusCode);
	}

	[Test]
	public async Task SetStatusCode_ShouldPopulateProperties()
	{
		IdempotencyKey idempotencyKey = CreateIdempotencyKey();

		idempotencyKey.SetStatusCode(MaxValidStatusCode);

		await Assert.That(idempotencyKey.StatusCode).IsEqualTo(MaxValidStatusCode);
	}

	[Test]
	public void SetStatusCode_ShouldThrow_WhenInvalidStatusCode()
	{
		Assert.Throws<CustomValidationException<IdempotencyKey>>(() => CreateIdempotencyKey().SetStatusCode(MaxInvalidStatusCode));
	}
}
