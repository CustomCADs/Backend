using CustomCADs.Modules.Customs.Domain.Customs.Enums;

namespace CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;

public sealed class GetCustomSortingsHandler : IQueryHandler<GetCustomSortingsQuery, CustomSortingType[]>
{
	public Task<CustomSortingType[]> Handle(GetCustomSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<CustomSortingType>()
		);
}
