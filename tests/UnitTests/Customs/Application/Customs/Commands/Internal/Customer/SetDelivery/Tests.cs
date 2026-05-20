using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Customers.SetDelivery;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Customer.SetDelivery;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly SetCustomDeliveryHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly bool NewValue = true;
	private readonly Custom custom = CreateCustom(forDelivery: !NewValue);

	public Tests()
	{
		handler = new(reads.Object, uow.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: ValidId,
			Value: NewValue,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: ValidId,
			Value: NewValue,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
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
			Id: ValidId,
			Value: NewValue,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CustomToggledDelivery)
			),
			Times.Exactly(isPending ? 0 : 1)
		);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		SetCustomDeliveryCommand command = new(
			Id: ValidId,
			Value: NewValue,
			BuyerId: ValidBuyerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(NewValue, custom.ForDelivery);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		SetCustomDeliveryCommand command = new(
			Id: ValidId,
			Value: NewValue,
			BuyerId: ValidBuyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			() => handler.Handle(command, ct)
		);
	}
}
