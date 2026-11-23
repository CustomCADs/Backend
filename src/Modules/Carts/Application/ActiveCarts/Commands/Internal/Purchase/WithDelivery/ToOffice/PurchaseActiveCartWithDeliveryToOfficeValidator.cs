using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Payment;
using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Modules.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery.ToOffice;

using static ApplicationConstants.FluentMessages;
using static DomainConstants;

public class PurchaseActiveCartWithDeliveryToOfficeValidator : CommandValidator<PurchaseActiveCartWithDeliveryToOfficeCommand, PaymentDto>
{
	public PurchaseActiveCartWithDeliveryToOfficeValidator()
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
