using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Behaviors.SetResponseBody;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	[Test]
	public void SetResponseBody_ShouldNotThrow()
	{
		CreateIdempotencyKey().SetResponseBody(ValidResponseBody);
	}

	[Test]
	public async Task SetResponseBody_ShouldPopulateProperties()
	{
		IdempotencyKey idempotencyKey = CreateIdempotencyKey();

		idempotencyKey.SetResponseBody(ValidResponseBody);

		await Assert.That(idempotencyKey.ResponseBody).IsEqualTo(ValidResponseBody);
	}

	[Test]
	public void SetResponseBody_ShouldThrow_WhenInvalidResponseBody()
	{
		Assert.Throws<CustomValidationException<IdempotencyKey>>(() => CreateIdempotencyKey().SetResponseBody(InvalidResponseBody));
	}
}
