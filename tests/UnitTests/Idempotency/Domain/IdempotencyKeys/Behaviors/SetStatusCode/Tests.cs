using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Idempotency.Domain.IdempotencyKeys.Behaviors.SetStatusCode;

using static Data.IdempotencyKeys.TestData;

public class Tests : Data.IdempotencyKeys.BaseUnitTests
{
	[Fact]
	public void SetStatusCode_ShouldNotThrow()
	{
		CreateIdempotencyKey().SetStatusCode(MaxValidStatusCode);
	}

	[Fact]
	public void SetStatusCode_ShouldPopulateProperties()
	{
		IdempotencyKey idempotencyKey = CreateIdempotencyKey();

		idempotencyKey.SetStatusCode(MaxValidStatusCode);

		Assert.Equal(MaxValidStatusCode, idempotencyKey.StatusCode);
	}

	[Fact]
	public void SetStatusCode_ShouldThrow_WhenInvalidStatusCode()
	{
		Assert.Throws<CustomValidationException<IdempotencyKey>>(
			() => CreateIdempotencyKey().SetStatusCode(MaxInvalidStatusCode)
		);
	}
}
