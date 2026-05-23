using CustomCADs.Modules.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductCreated;
using CustomCADs.Modules.Catalog.Domain.Products.Enums;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly CreateProductHandler handler;
	private readonly CreateProductCommand request = new(
		Name: MinValidName,
		Description: MinValidDescription,
		Price: MinValidPrice,
		CategoryId: ValidCategoryId,
		ImageId: ValidImageId,
		CadId: ValidCadId,
		CallerId: ValidCreatorId
	);

	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const decimal Volume = 15;
	private readonly Product product = CreateProduct(id: ValidId);

	public Tests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, raiser.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Product>(x =>
				x.Name == MinValidName &&
				x.Description == MinValidDescription &&
				x.Price == MinValidPrice &&
				x.Status == ProductStatus.Unchecked &&
				x.CreatorId == ValidCreatorId &&
				x.CategoryId == ValidCategoryId &&
				x.ImageId == ValidImageId &&
				x.CadId == ValidCadId
			),
			ct
		)).ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync("role");
		sender.Setup(x => x.SendQueryAsync(
			It.Is<IsCadPrintableByIdQuery>(x => x.Id == ValidCadId),
			ct
		)).ReturnsAsync(false);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<ImageExistsByIdQuery>(x => x.Id == ValidImageId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<CadExistsByIdQuery>(x => x.Id == ValidCadId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Product>(x =>
					x.Name == MinValidName &&
					x.Description == MinValidDescription &&
					x.Price == MinValidPrice &&
					x.Status == ProductStatus.Unchecked &&
					x.CreatorId == ValidCreatorId &&
					x.CategoryId == ValidCategoryId &&
					x.ImageId == ValidImageId &&
					x.CadId == ValidCadId
				),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidCreatorId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<ImageExistsByIdQuery>(x => x.Id == ValidImageId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<CadExistsByIdQuery>(x => x.Id == ValidCadId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUserRoleByIdQuery>(x => x.Id == ValidCreatorId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<IsCadPrintableByIdQuery>(x => x.Id == ValidCadId),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<ProductCreatedApplicationEvent>(x =>
					x.Id == ValidId
					&& x.TagIds.Contains(DomainConstants.Tags.NewId)
				)
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldValidateStatus_WhenDesignerRole()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(DomainConstants.Users.DesignerRole);

		// Act
		await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ProductStatus.Validated, product.Status);
	}

	[Fact]
	public async Task Handle_ShouldTagProfessional_WhenDesignerRole()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(DomainConstants.Users.DesignerRole);

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<ProductCreatedApplicationEvent>(x => x.TagIds.Contains(DomainConstants.Tags.ProfessionalId))
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldTagPrintable_WhenAppropriateContentType()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<IsCadPrintableByIdQuery>(x => x.Id == ValidCadId),
			ct
		)).ReturnsAsync(true);

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<ProductCreatedApplicationEvent>(x => x.TagIds.Contains(DomainConstants.Tags.PrintableId))
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ProductId id = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCategoryNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(false);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
