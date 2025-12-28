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

using static CustomsData;

public class CustomPaymentCompletedApplicationEventHandlerUnitTests : CustomsBaseUnitTests
{
	private CustomPaymentCompletedApplicationEventHandler handler;

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEmailService> email = new();

	private const string ToEmail = "user123@gmail.com";
	private const string ClientUrl = "https://www.site123.com";
	private readonly Custom custom = CreateCustomWithId(forDelivery: false);

	public CustomPaymentCompletedApplicationEventHandlerUnitTests()
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

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUserEmailByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetClientUrlQuery>(),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendEmail()
	{
		// Arrange
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		email.Verify(x => x.SendRewardGrantedEmailAsync(
			ToEmail,
			$"{ClientUrl}/customs/{ValidId}",
			ct
		), Times.Once());
	}

	[Fact]
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

		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Act
		await handler.HandleAsync(ae);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<ActivateShipmentCommand>(x => x.Id == ValidShipmentId),
			ct
		), Times.Once());
		email.Verify(x => x.SendRewardGrantedEmailAsync(
			ToEmail,
			$"{ClientUrl}/shipments/{ValidShipmentId}",
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Custom);
		CustomPaymentCompletedApplicationEvent ae = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.HandleAsync(ae)
		);
	}
}
