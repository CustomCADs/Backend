using CustomCADs.Customs.Application.Customs.Events.Application.DeliveryRequested;
using CustomCADs.Customs.Application.Customs.Events.Application.PaymentCompleted;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	public static void AddCustomDeliveryPaymentSagaDependencies(this IServiceCollection services)
	{
		services.AddScoped<CustomDeliveryRequestedApplicationEventHandler>();
		services.AddScoped<CustomPaymentCompletedApplicationEventHandler>();
	}
}
