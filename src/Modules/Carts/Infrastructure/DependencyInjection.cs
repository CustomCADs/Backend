using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;
using CustomCADs.Modules.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(IServiceCollection services)
	{
		public void AddCartDeliveryPaymentSagaDependencies()
			=> services
				.AddScoped<ActiveCartDeliveryRequestedApplicationEventHandler>()
				.AddScoped<CartPaymentCompletedApplicationEventHandler>();
	}
}
