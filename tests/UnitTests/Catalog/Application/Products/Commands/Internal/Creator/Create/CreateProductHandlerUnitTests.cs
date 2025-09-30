using CustomCADs.Catalog.Application.Products.Commands.Internal.Creator.Create;
using CustomCADs.Catalog.Application.Products.Events.Application.ProductCreated;
using CustomCADs.Catalog.Domain.Products.Enums;
using CustomCADs.Catalog.Domain.Repositories;
using CustomCADs.Catalog.Domain.Repositories.Writes;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Application.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Commands.Internal.Creator.Create;

using static ProductsData;

public class CreateProductHandlerUnitTests : ProductsBaseUnitTests
{
	private readonly CreateProductHandler handler;
	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const decimal Volume = 15;
	private readonly Product product = CreateProductWithId(id: ValidId);

	public CreateProductHandlerUnitTests()
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

		sender.Setup(x => x.SendCommandAsync(
			It.IsAny<CreateCadCommand>(),
			ct
		)).ReturnsAsync(ValidCadId);

		sender.Setup(x => x.SendCommandAsync(
			It.IsAny<CreateImageCommand>(),
			ct
		)).ReturnsAsync(ValidImageId);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(DomainConstants.Roles.Contributor);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
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
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryExistsByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendCommandAsync(
			It.IsAny<CreateCadCommand>(),
			ct
		), Times.Once());
		sender.Verify(x => x.SendCommandAsync(
			It.IsAny<CreateImageCommand>(),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<ProductCreatedApplicationEvent>(x =>
				x.Id == ValidId
				&& x.TagIds.Contains(DomainConstants.Tags.NewId)
			)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldValidateStatus_WhenDesignerRole()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUserRoleByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(DomainConstants.Roles.Designer);

		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

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
		)).ReturnsAsync(DomainConstants.Roles.Designer);

		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<ProductCreatedApplicationEvent>(x => x.TagIds.Contains(DomainConstants.Tags.ProfessionalId))
		), Times.Once());
	}

	[Theory]
	[InlineData(ApplicationConstants.Cads.StlContentType)]
	public async Task Handle_ShouldTagPrintable_WhenAppropriateContentType(string cadContentType)
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: cadContentType,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<ProductCreatedApplicationEvent>(x => x.TagIds.Contains(DomainConstants.Tags.PrintableId))
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Act
		ProductId id = await handler.Handle(command, ct);

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

		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
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

		CreateProductCommand command = new(
			Name: MinValidName,
			Description: MinValidDescription,
			Price: MinValidPrice,
			ImageKey: string.Empty,
			ImageContentType: string.Empty,
			CadKey: string.Empty,
			CadContentType: string.Empty,
			CadVolume: Volume,
			CategoryId: ValidCategoryId,
			CallerId: ValidCreatorId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
