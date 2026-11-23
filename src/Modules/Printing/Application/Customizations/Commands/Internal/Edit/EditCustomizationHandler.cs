using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Edit;

public sealed class EditCustomizationHandler(
	ICustomizationReads reads,
	IUnitOfWork uow
) : ICommandHandler<EditCustomizationCommand>
{
	public async Task Handle(EditCustomizationCommand req, CancellationToken ct)
	{
		Customization customization = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		customization
			.SetInfill(req.Infill)
			.SetVolume(req.Volume, req.Base, req.Scale)
			.SetColor(req.Color)
			.SetMaterialId(req.MaterialId);

		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
