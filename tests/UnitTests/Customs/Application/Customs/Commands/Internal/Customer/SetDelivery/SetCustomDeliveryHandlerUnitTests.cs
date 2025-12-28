using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.SetDelivery;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.SetDelivery;

using static CustomsData;

public class SetCustomDeliveryHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly SetCustomDeliveryHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly CustomId id = CustomId.New();
	private static readonly bool value = true;
	private static readonly AccountId buyerId = AccountId.New();
	private readonly Custom custom = CreateCustom(forDelivery: !value);

	public SetCustomDeliveryHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public async Task Handle_ShouldRaiseEvents(bool isPending)
	{
		// Arrange
		if (!isPending)
		{
			custom.Accept(ValidDesignerId);
		}

		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CustomToggledDelivery)
		), Times.Exactly(isPending ? 0 : 1));
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(value, custom.ForDelivery);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Custom);

		SetCustomDeliveryCommand command = new(
			Id: id,
			Value: value,
			BuyerId: buyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
