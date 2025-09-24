using CustomCADs.Customs.API.Customs.Dtos;
using CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;
using CustomCADs.Shared.Application.Abstractions.Payment;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Post.Purchase.Normal;

public sealed class PurchaseCustomEndpoint(IRequestSender sender)
	: Endpoint<PurchaseCustomRequest, PaymentResponse>
{
	public override void Configure()
	{
		Post("purchase");
		Group<CustomerGroup>();
		Description(x => x
			.WithSummary("Purchase")
			.WithDescription("Purchase a Custom")
		);
	}

	public override async Task HandleAsync(PurchaseCustomRequest req, CancellationToken ct)
	{
		PaymentDto dto = await sender.SendCommandAsync(
			command: new PurchaseCustomCommand(
				Id: CustomId.New(req.Id),
				PaymentMethodId: req.PaymentMethodId,
				CallerId: User.GetAccountId()
			),
			ct: ct
		).ConfigureAwait(false);

		PaymentResponse response = dto.ToResponse();
		await Send.OkAsync(response).ConfigureAwait(false);
	}
}
