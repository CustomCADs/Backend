using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Catalog.Application.Products.Policies;

public class ProductCadUploadPolicy(IRequestSender sender) : IFileUploadPolicy<CadId>
{
	public FileContextType Type => FileContextType.Product;

	public async Task EnsureUploadGrantedAsync(IFileUploadPolicy<CadId>.FileContext context)
	{
		string role = await sender.SendQueryAsync(
			query: new GetUserRoleByIdQuery(context.CallerId)
		).ConfigureAwait(false);

		if (role is not DomainConstants.Roles.Contributor or DomainConstants.Roles.Designer)
		{
			throw CustomAuthorizationException<Product>.Custom("Must be a Contributor/Designer to upload Product CADs");
		}
	}
}
