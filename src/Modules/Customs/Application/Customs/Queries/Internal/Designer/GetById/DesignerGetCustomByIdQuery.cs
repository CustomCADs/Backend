using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Designer.GetById;

public sealed record DesignerGetCustomByIdQuery(
	CustomId Id,
	AccountId CallerId
) : IQuery<DesignerGetCustomByIdDto>;
