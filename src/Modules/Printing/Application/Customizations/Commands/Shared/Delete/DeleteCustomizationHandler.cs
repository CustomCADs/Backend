using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Customizations.Commands;

namespace CustomCADs.Modules.Printing.Application.Customizations.Commands.Shared.Delete;

public sealed class DeleteCustomizationHandler(
	ICustomizationReads reads,
	IWrites<Customization> writes,
	IUnitOfWork uow
) : ICommandHandler<DeleteCustomizationByIdCommand>
{
	public async Task Handle(DeleteCustomizationByIdCommand req, CancellationToken ct)
	{
		Customization customization = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Customization>.ById(req.Id);

		writes.Remove(customization);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
