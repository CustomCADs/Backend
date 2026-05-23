using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Shared.GetPaymentStatuses;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly GetCustomPaymentStatusesHandler handler = new();
	private readonly GetCustomPaymentStatusesQuery request = new();

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		PaymentStatus[] sortings = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(sortings, Enum.GetValues<PaymentStatus>());
	}
}
