using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.WithDelivery;
using CustomCADs.Customs.API.Customs.Dtos;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Domain.TypedIds.Printing;
using CustomCADs.Shared.API.Extensions;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Post.Purchase.WithDelivery;

public sealed class PurchaseCustomWithDeliveryEndpoint(IRequestSender sender)
	: Endpoint<PurchasCustomWithDeliveryRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase-delivery");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Purchase (ForDelivery)")
			.WithDescription("Purchase a Custom with ForDelivery")
		);
	}

	public override async Task HandleAsync(PurchasCustomWithDeliveryRequest req, CancellationToken ct)
	{
		PaymentDto dto = await sender.SendCommandAsync(
			command: new PurchaseCustomWithDeliveryCommand(
				Id: CustomId.New(req.Id),
				PaymentMethodId: req.PaymentMethodId,
				ShipmentService: req.ShipmentService,
				Count: req.Count,
				Address: req.Address,
				Contact: req.Contact,
				CallerId: User.GetAccountId(),
				CustomizationId: CustomizationId.New(req.CustomizationId)
			),
			ct: ct
		).ConfigureAwait(false);

		PaymentResponse response = dto.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
