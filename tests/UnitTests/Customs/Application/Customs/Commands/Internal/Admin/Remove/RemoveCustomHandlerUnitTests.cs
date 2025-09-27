using CustomCADs.Customs.Application.Customs.Commands.Internal.Admin.Remove;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Admin.Remove;

using static CustomsData;

public class RemoveCustomHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly RemoveCustomHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly AccountId adminId = AccountId.New();
	private readonly Custom custom = CreateCustom();

	public RemoveCustomHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, raiser.Object);

		custom.Report();
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: adminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: adminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: adminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CustomRemoved)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: adminId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Equal(CustomStatus.Removed, custom.CustomStatus);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		RemoveCustomCommand command = new(
			Id: ValidId,
			CallerId: adminId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
