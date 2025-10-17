using CustomCADs.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;
using CustomCADs.Carts.Application.PurchasedCarts.Events.Application.PaymentCompleted;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddCartDeliveryPaymentSagaDependencies(this IServiceCollection services)
	{
		services.AddScoped<ActiveCartDeliveryRequestedApplicationEventHandler>();
		services.AddScoped<CartPaymentCompletedApplicationEventHandler>();
	}
}
