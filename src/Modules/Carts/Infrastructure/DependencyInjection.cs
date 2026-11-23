using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested.ToAddress;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested.ToOffice;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddCartDeliveryPaymentSagaDependencies()
			=> services
				.AddScoped<ActiveCartDeliveryToAddressRequestedHandler>()
				.AddScoped<ActiveCartDeliveryToOfficeRequestedHandler>()
				.AddScoped<CartPaymentCompletedHandler>();
	}
}
