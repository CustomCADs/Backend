using CustomCADs.Modules.Customs.Application.Customs.Events.Application.PaymentCompleted;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Email;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Customs;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Identity.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Events.Application.PaymentCompleted;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly CustomPaymentCompletedHandler handler;
	private readonly CustomPaymentCompletedApplicationEvent request = new(ValidId, ValidBuyerId);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEmailService> email = new();

	private const string ToEmail = "user123@gmail.com";
	private const string ClientUrl = "https://www.site123.com";
	private readonly Custom custom = CreateCustom(forDelivery: false);

	public Tests()
	{
		handler = new(reads.Object, uow.Object, sender.Object, email.Object);

		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
		custom.Complete(customizationId: null);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);

		sender.Setup(x => x.SendQueryAsync(It.Is<GetUserEmailByIdQuery>(x => x.Id == ValidBuyerId), ct))
			.ReturnsAsync(ToEmail);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetClientUrlQuery>(), ct))
			.ReturnsAsync(ClientUrl);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

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
		await handler.HandleAsync(request);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUserEmailByIdQuery>(x => x.Id == ValidBuyerId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.IsAny<GetClientUrlQuery>(),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendEmail()
	{
		// Arrange

		// Act
		await handler.HandleAsync(request);

		// Assert
		email.Verify(
			x => x.SendRewardGrantedEmailAsync(
				ToEmail,
				$"{ClientUrl}/customs/{ValidId}",
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldGrantShipment_WhenForDelivery()
	{
		// Arrange
		Custom custom = CreateCustom(forDelivery: true);
		custom.Accept(ValidDesignerId);
		custom.Begin();
		custom.Finish(ValidCadId, ValidPrice);
		custom.Complete(ValidCustomizationId);
		custom.SetShipment(ValidShipmentId);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);

		// Act
		await handler.HandleAsync(request);

		// Assert
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<ActivateShipmentCommand>(x => x.Id == ValidShipmentId),
				ct
			),
			Times.Once()
		);
		email.Verify(
			x => x.SendRewardGrantedEmailAsync(
				ToEmail,
				$"{ClientUrl}/shipments/{ValidShipmentId}",
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(() => handler.HandleAsync(request));
	}
}
