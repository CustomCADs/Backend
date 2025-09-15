using CustomCADs.Customs.Domain.Customs.Enums;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Shared.GetSortings;

public sealed class GetCustomSortingsHandler : IQueryHandler<GetCustomSortingsQuery, CustomSortingType[]>
{
	public Task<CustomSortingType[]> Handle(GetCustomSortingsQuery req, CancellationToken ct)
		=> Task.FromResult(
			Enum.GetValues<CustomSortingType>()
		);
}
