using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.Bases.Id;

namespace CustomCADs.Shared.Application.Policies;

public static class IFileAccessPolicyExtensions
{
	public static async Task EvaluateAsync<TTypedId>(
		this IEnumerable<IFileDownloadPolicy<TTypedId>> policies,
		IFileDownloadPolicy<TTypedId>.FileContext context,
		FileContextType type
	) where TTypedId : IEntityId<Guid>
	{
		foreach (IFileDownloadPolicy<TTypedId> policy in policies.Where(x => x.Type == type))
		{
			await policy.EnsureDownloadGrantedAsync(context).ConfigureAwait(false);
		}
	}

	public static async Task EvaluateAsync<TTypedId>(
		this IEnumerable<IFileUploadPolicy<TTypedId>> policies,
		IFileUploadPolicy<TTypedId>.FileContext context,
		FileContextType type
	) where TTypedId : IEntityId<Guid>
	{
		foreach (IFileUploadPolicy<TTypedId> policy in policies.Where(x => x.Type == type))
		{
			await policy.EnsureUploadGrantedAsync(context).ConfigureAwait(false);
		}
	}

	public static async Task EvaluateAsync<TTypedId>(
		this IEnumerable<IFileReplacePolicy<TTypedId>> policies,
		IFileReplacePolicy<TTypedId>.FileContext context,
		FileContextType type
	) where TTypedId : IEntityId<Guid>
	{
		var applicablePolicies = policies.Where(x => x.Type == type);
		if (!applicablePolicies.Any())
		{
			throw new CustomException("No Applicable policies found - operation blocked!");
		}

		foreach (IFileReplacePolicy<TTypedId> policy in applicablePolicies)
		{
			await policy.EnsureReplaceGrantedAsync(context).ConfigureAwait(false);
		}
	}
}
