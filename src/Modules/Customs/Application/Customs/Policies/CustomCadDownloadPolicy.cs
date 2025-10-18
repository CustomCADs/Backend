using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Customs.Application.Customs.Policies;

public class CustomCadDownloadPolicy(ICustomReads reads, IRequestSender sender) : IFileDownloadPolicy<CadId>
{
	public FileContextType Type => FileContextType.Custom;

	public async Task EnsureDownloadGrantedAsync(IFileDownloadPolicy<CadId>.FileContext context)
	{
		if (!await reads.ExistsByCadIdAsync(context.FileId).ConfigureAwait(false)) return;

		string role = await sender.SendQueryAsync(
			query: new GetUserRoleByIdQuery(context.CallerId)
		).ConfigureAwait(false);

		switch (role)
		{
			case DomainConstants.Roles.Customer:
				await EnsureCustomerDownloadGrantedAsync(context).ConfigureAwait(false);
				break;

			case DomainConstants.Roles.Designer:
				await EnsureDesignerDownloadGrantedAsync(context).ConfigureAwait(false);
				break;

			default: throw CustomAuthorizationException<Custom>.Custom("Your role is not suitable for access to a Custom's CAD");
		}
	}

	private async Task EnsureCustomerDownloadGrantedAsync(IFileDownloadPolicy<CadId>.FileContext context)
	{
		Custom custom = await reads.SingleByCadIdAsync(context.FileId, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ByProp(nameof(Custom.FinishedCustom.CadId), context.FileId);

		if (custom.BuyerId != context.CallerId)
		{
			throw CustomAuthorizationException<Custom>.Custom($"Cannot access another Buyer's Custom: {custom.Id}.");
		}

		if (custom.CompletedCustom is null)
		{
			throw CustomStatusException<Custom>.Custom($"Custom is not completed: {custom.Id}.");
		}

		if (custom.CompletedCustom.PaymentStatus is not PaymentStatus.Completed)
		{
			throw CustomException.NotPaid<Custom>();
		}
	}

	private async Task EnsureDesignerDownloadGrantedAsync(IFileDownloadPolicy<CadId>.FileContext context)
	{
		Custom custom = await reads.SingleByCadIdAsync(context.FileId, track: false).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ByProp(nameof(Custom.FinishedCustom.CadId), context.FileId);

		if (custom is { CustomStatus: CustomStatus.Pending, AcceptedCustom: null })
		{
			return; // doesn't have an assigned Designer yet
		}

		if (custom.AcceptedCustom?.DesignerId == context.CallerId)
		{
			return; // the assigned Designer is making this call
		}

		throw CustomAuthorizationException<Custom>.ById(custom.Id);
	}
}
