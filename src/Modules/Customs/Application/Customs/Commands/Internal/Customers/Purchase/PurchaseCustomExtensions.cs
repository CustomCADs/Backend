using CustomCADs.Customs.Domain.Customs.States.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using System.Diagnostics.CodeAnalysis;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase;

internal static class PurchaseCustomExtensions
{
	internal static void EnsureCustomCanBePurchased(
		bool isDeliveryWrong,
		AccountId callerId,
		AccountId ownerId,
		[NotNull] AcceptedCustom? acceptedCustom,
		[NotNull] FinishedCustom? finishedCustom
	)
	{
		if (isDeliveryWrong)
		{
			throw CustomException.Delivery<Custom>(isDeliveryWrong);
		}

		if (ownerId != callerId)
		{
			throw CustomAuthorizationException<Custom>.ById(callerId);
		}

		if (acceptedCustom is null)
		{
			throw CustomException.NullProp<Custom>(nameof(AcceptedCustom));
		}

		if (finishedCustom is null)
		{
			throw CustomException.NullProp<Custom>(nameof(FinishedCustom));
		}
	}
}
