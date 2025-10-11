using CustomCADs.Catalog.Application.Products.Events.Application.ProductCreated;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static DomainConstants;

public sealed class CreateProductHandler(
	IProductWrites writes,
	IUnitOfWork uow,
	IRequestSender sender,
	IEventRaiser raiser
) : ICommandHandler<CreateProductCommand, ProductId>
{
	public async Task<ProductId> Handle(CreateProductCommand req, CancellationToken ct)
	{
		if (!await sender.SendQueryAsync(new GetAccountExistsByIdQuery(req.CallerId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CallerId, "User");
		}

		if (!await sender.SendQueryAsync(new CadExistsByIdQuery(req.CadId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CadId, "Cad");
		}

		if (!await sender.SendQueryAsync(new ImageExistsByIdQuery(req.ImageId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.ImageId, "Image");
		}

		if (!await sender.SendQueryAsync(new GetCategoryExistsByIdQuery(req.CategoryId), ct).ConfigureAwait(false))
		{
			throw CustomNotFoundException<Product>.ById(req.CategoryId, "Category");
		}

		Product product = await writes.AddAsync(
			product: Product.Create(
				name: req.Name,
				description: req.Description,
				price: req.Price,
				categoryId: req.CategoryId,
				creatorId: req.CallerId,
				imageId: req.ImageId,
				cadId: req.CadId
			),
			ct: ct
		).ConfigureAwait(false);
		await uow.SaveChangesAsync(ct).ConfigureAwait(false);

		string role = await sender.SendQueryAsync(
			query: new GetUserRoleByIdQuery(req.CallerId),
			ct: ct
		).ConfigureAwait(false);

		if (role is Roles.Designer)
		{
			product.Validate(req.CallerId);
		}

		await raiser.RaiseApplicationEventAsync(
			@event: new ProductCreatedApplicationEvent(
				Id: product.Id,
				TagIds: TagId.Filter(new()
				{
					[Tags.NewId] = true,
					[Tags.ProfessionalId] = role is Roles.Designer,
					[Tags.PrintableId] = await sender.SendQueryAsync(
						query: new IsCadPrintableByIdQuery(req.CadId),
						ct: ct
					).ConfigureAwait(false),
				})
			)
		).ConfigureAwait(false);

		return product.Id;
	}
}
