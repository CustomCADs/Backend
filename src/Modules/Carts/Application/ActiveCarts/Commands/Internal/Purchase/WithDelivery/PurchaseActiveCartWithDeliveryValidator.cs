using CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.ToOffice;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

using static ApplicationConstants.FluentMessages;
using static DomainConstants;

public class PurchaseActiveCartWithDeliveryValidator : CommandValidator<PurchaseActiveCartWithDeliveryToOfficeCommand, PaymentDto>
{
	public PurchaseActiveCartWithDeliveryValidator()
	{
		RuleFor(x => x.ShipmentService)
			.NotEmpty().WithMessage(RequiredError);

		RuleFor(x => x.Contact)
			.ChildRules(x =>
			{
				x.RuleFor(x => x.Email)
					.Matches(Regexes.Email).WithMessage(EmailError);

				x.RuleFor(x => x.Phone)
					.Matches(Regexes.Phone).WithMessage(PhoneError);
			});
	}
}
