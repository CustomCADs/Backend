using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Customs.Application.Customs.Policies;

public class CustomCadUploadPolicy(IRequestSender sender) : IFileUploadPolicy<CadId>
{
	public FileContextType Type => FileContextType.Custom;

	public async Task EnsureUploadGrantedAsync(IFileUploadPolicy<CadId>.FileContext context)
	{
		string role = await sender.SendQueryAsync(
			query: new GetUserRoleByIdQuery(context.CallerId)
		).ConfigureAwait(false);

		if (role is not DomainConstants.Roles.Designer)
		{
			throw CustomAuthorizationException<Custom>.Custom("Must be a Designer to upload a Custom's CAD.");
		}
	}
}
