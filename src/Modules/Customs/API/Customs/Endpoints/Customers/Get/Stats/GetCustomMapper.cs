using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.Count;

namespace CustomCADs.Customs.API.Customs.Endpoints.Customers.Get.Stats;

public class GetCustomsStatsMapper : ResponseMapper<GetCustomsStatsResponse, CountCustomsDto>
{
	public override GetCustomsStatsResponse FromEntity(CountCustomsDto counts)
		=> new(
			PendingCount: counts.Pending,
			AcceptedCount: counts.Accepted,
			BegunCount: counts.Begun,
			FinishedCount: counts.Finished,
			CompletedCount: counts.Completed,
			ReportedCount: counts.Reported
		);
};
