using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;

public class Tests : Data.PurchasedCarts.BaseUnitTests
{
	private readonly GetPurchasedCartPaymentStatusesHandler handler = new();
	private readonly GetPurchasedCartPaymentStatusesQuery request = new();

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		PaymentStatus[] statuses = await handler.Handle(request, ct);

		// Assert
		await Assert.That(statuses).IsEquivalentTo(Enum.GetValues<PaymentStatus>());
	}
}
