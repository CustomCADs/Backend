namespace CustomCADs.Shared.Application.UseCases.Images.Queries;

public sealed record ImageExistsByIdQuery(
	ImageId Id
) : IQuery<bool>;
