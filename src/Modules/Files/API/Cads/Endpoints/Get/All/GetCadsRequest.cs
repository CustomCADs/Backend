namespace CustomCADs.Modules.Files.API.Cads.Endpoints.Get.All;

public record GetCadsRequest(
	int Page = 1,
	int Limit = 20
);
