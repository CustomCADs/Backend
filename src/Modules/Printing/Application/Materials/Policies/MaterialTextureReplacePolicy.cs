using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Printing.Application.Materials.Policies;

public class MaterialTextureReplacePolicy(IRequestSender sender) : IFileReplacePolicy<ImageId>
{
	public FileContextType Type => FileContextType.Material;

	public async Task EnsureReplaceGrantedAsync(IFileReplacePolicy<ImageId>.FileContext context)
	{
		string role = await sender.SendQueryAsync(
			query: new GetUserRoleByIdQuery(context.CallerId)
		).ConfigureAwait(false);

		if (role is not DomainConstants.Roles.Admin)
		{
			throw CustomAuthorizationException<Material>.ById(context.FileId, "Texture");
		}
	}
}
