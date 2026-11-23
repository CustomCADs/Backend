using CustomCADs.Modules.Printing.Domain.Repositories;

namespace CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Create;

public sealed class CreateCustomizationHandler(
	IWrites<Customization> writes,
	IUnitOfWork uow
) : ICommandHandler<CreateCustomizationCommand, CustomizationId>
{
	public async Task<CustomizationId> Handle(CreateCustomizationCommand req, CancellationToken ct)
	{
		Customization customization = await writes.AddAsync(
			entity: Customization.Create(
				infill: req.Infill,
				volume: new(Value: req.Volume, Base: req.Base, Scale: req.Scale),
				color: req.Color,
				materialId: req.MaterialId
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		return customization.Id;
	}
}
