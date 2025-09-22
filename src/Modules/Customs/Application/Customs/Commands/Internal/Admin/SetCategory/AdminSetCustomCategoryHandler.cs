using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Admin.SetCategory;

public sealed class AdminSetCustomCategoryHandler(ICustomReads reads, IUnitOfWork uow)
	: ICommandHandler<AdminSetCustomCategoryCommand>
{
	public async Task Handle(AdminSetCustomCategoryCommand req, CancellationToken ct = default)
	{
		Custom custom = await reads.SingleByIdAsync(req.Id, ct: ct).ConfigureAwait(false)
			?? throw CustomNotFoundException<Custom>.ById(req.Id);

		custom.SetCategory((req.CategoryId, CustomCategorySetter.Admin));
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);
	}
}
