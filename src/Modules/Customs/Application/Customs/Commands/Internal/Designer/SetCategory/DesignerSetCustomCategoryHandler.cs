using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.SetCategory;

public sealed class DesignerSetCustomCategoryHandler(ICustomReads reads, IUnitOfWork uow)
	: ICommandHandler<DesignerSetCustomCategoryCommand>
{
	public async Task Handle(DesignerSetCustomCategoryCommand req, CancellationToken ct = default)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		if (custom.CustomStatus is not CustomStatus.Pending
			&& custom.AcceptedCustom?.DesignerId != req.CallerId)
		{
			throw CustomAuthorizationException<Custom>.ById(req.Id);
		}

		custom.SetCategory((req.CategoryId, CustomCategorySetter.Designer));
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
