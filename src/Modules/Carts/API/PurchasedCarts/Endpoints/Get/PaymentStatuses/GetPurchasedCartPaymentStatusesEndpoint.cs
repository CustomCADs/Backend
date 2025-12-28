using CustomCADs.Modules.Carts.Application.PurchasedCarts.Queries.Internal.GetPaymentStatuses;
using CustomCADs.Modules.Carts.Domain.PurchasedCarts.Enums;

namespace CustomCADs.Modules.Carts.API.PurchasedCarts.Endpoints.Get.PaymentStatuses;

public sealed class GetPurchasedCartPaymentStatusesEndpoint(IRequestSender sender)
	: EndpointWithoutRequest<PaymentStatus[]>
{
	public override void Configure()
	{
		Get("payment-statuses");
		Group<PurchasedCartsGroup>();
		Description(x => x
			.WithSummary("Payment Statuses")
			.WithDescription("See all Purchased Cart Payment Status types")
		);
	}

	public override async Task HandleAsync(CancellationToken ct)
	{
		PaymentStatus[] result = await sender.SendQueryAsync(
			query: new GetPurchasedCartPaymentStatusesQuery(),
			ct: ct
		).ConfigureAwait(false);

		await Send.OkAsync(result).ConfigureAwait(false);
	}
}
