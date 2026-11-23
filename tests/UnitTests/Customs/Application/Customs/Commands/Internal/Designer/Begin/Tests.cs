using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.Begin;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Dtos.Notifications;
using CustomCADs.Shared.Application.Events.Notifications;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.Begin;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly BeginCustomHandler handler;
	private readonly BeginCustomCommand request = new(ValidId, ValidDesignerId);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private readonly Custom custom = CreateCustom();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, raiser.Object);

		custom.Accept(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, true, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<NotificationRequestedEvent>(x => x.Type == NotificationType.CustomBegun)
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(custom.AcceptedCustom?.DesignerId).IsEqualTo(ValidDesignerId);
			await Assert.That(custom.CustomStatus).IsEqualTo(CustomStatus.Begun);
		}
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		var custom = CreateCustom();
		custom.Accept(AccountId.New());
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(() => handler.Handle(request, ct));
	}
}
