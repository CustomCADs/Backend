using CustomCADs.Shared.Domain.Bases.Id;

namespace CustomCADs.Shared.Application.Policies;

public static class IFileAccessPolicyExtensions
{
	extension<TTypedId>(IEnumerable<IFileDownloadPolicy<TTypedId>> policies) where TTypedId : IEntityId<Guid>
	{
		public async Task EvaluateAsync(IFileDownloadPolicy<TTypedId>.FileContext context, FileContextType type)
		{
			foreach (IFileDownloadPolicy<TTypedId> policy in policies.Where(x => x.Type == type))
			{
				await policy.EnsureDownloadGrantedAsync(context).ConfigureAwait(false);
			}
		}
	}

	extension<TTypedId>(IEnumerable<IFileUploadPolicy<TTypedId>> policies) where TTypedId : IEntityId<Guid>
	{
		public async Task EvaluateAsync(IFileUploadPolicy<TTypedId>.FileContext context, FileContextType type)
		{
			foreach (IFileUploadPolicy<TTypedId> policy in policies.Where(x => x.Type == type))
			{
				await policy.EnsureUploadGrantedAsync(context).ConfigureAwait(false);
			}
		}
	}

	extension<TTypedId>(IEnumerable<IFileReplacePolicy<TTypedId>> policies) where TTypedId : IEntityId<Guid>
	{
		public async Task EvaluateAsync(IFileReplacePolicy<TTypedId>.FileContext context, FileContextType type)
		{
			foreach (IFileReplacePolicy<TTypedId> policy in policies.Where(x => x.Type == type))
			{
				await policy.EnsureReplaceGrantedAsync(context).ConfigureAwait(false);
			}
		}
	}

}
