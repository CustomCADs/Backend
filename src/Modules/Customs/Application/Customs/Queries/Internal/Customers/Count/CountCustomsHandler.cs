using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;

public sealed class CountCustomsHandler(ICustomReads reads)
	: IQueryHandler<CountCustomsQuery, CountCustomsDto>
{
	public async Task<CountCustomsDto> Handle(CountCustomsQuery req, CancellationToken ct)
	{
		Dictionary<CustomStatus, int> counts = await reads
			.CountAsync(req.CallerId, ct: ct)
			.ConfigureAwait(false);

		return new(
			Pending: counts.GetCountOrZero(CustomStatus.Pending),
			Accepted: counts.GetCountOrZero(CustomStatus.Accepted),
			Begun: counts.GetCountOrZero(CustomStatus.Begun),
			Finished: counts.GetCountOrZero(CustomStatus.Finished),
			Completed: counts.GetCountOrZero(CustomStatus.Completed),
			Reported: counts.GetCountOrZero(CustomStatus.Reported)
		);
	}
}
