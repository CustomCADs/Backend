namespace CustomCADs.Shared.Application.Events.Catalog;

public record ProductDeletedApplicationEvent(
	ProductId Id,
	ImageId ImageId,
	CadId CadId
) : BaseApplicationEvent;
