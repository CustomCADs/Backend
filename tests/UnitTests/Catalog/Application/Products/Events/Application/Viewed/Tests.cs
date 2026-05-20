using CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductViewed;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Application.Viewed;

using static DomainConstants;
using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly ProductViewedHandler handler;
	private readonly Mock<IProductReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const string Username = Users.CustomerUsername;
	private readonly AccountInfoDto info = new(
		Id: ValidCreatorId,
		CreatedAt: default,
		TrackViewedProducts: true,
		FirstName: null,
		LastName: null
	);
	private readonly Product product = CreateProduct();
	private static readonly DateTimeOffset viewedAt = DateTimeOffset.UtcNow;

	public Tests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(product);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		)).ReturnsAsync(Username);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == Username),
			ct
		)).ReturnsAsync(info);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		)).ReturnsAsync(false);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidCreatorId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == Username),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<ProductViewedApplicationEvent>(x => x.Id == ValidId && x.AccountId == ValidCreatorId)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		Assert.Equal(1, product.Counts.Views);
	}

	[Fact]
	public async Task Handle_ShouldReturnEarly_WhenUserDoesNotTrackViewedProducts()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == Username),
			ct
		)).ReturnsAsync(info with { TrackViewedProducts = false });
		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Never());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Never());
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<ProductViewedApplicationEvent>(x => x.Id == ValidId && x.AccountId == ValidCreatorId)
		), Times.Never());
		Assert.Equal(0, product.Counts.Views);
	}

	[Fact]
	public async Task Handle_ShouldReturnEarly_WhenUserAlreadyViewedProduct()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductQuery>(x => x.Id == ValidCreatorId && x.ProductId == ValidId),
			ct
		)).ReturnsAsync(true);
		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == Username),
			ct
		), Times.Never());

		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Never());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Never());

		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<ProductViewedApplicationEvent>(x => x.Id == ValidId && x.AccountId == ValidCreatorId)
		), Times.Never());
		Assert.Equal(0, product.Counts.Views);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenProductNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Product);

		ProductViewedApplicationEvent @event = new(ValidId, ValidCreatorId, viewedAt);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Product>>(
			// Act
			async () => await handler.HandleAsync(@event)
		);
	}
}
