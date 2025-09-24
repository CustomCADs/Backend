using CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Customers.GetById;

using static CustomsData;

public class CustomerGetCustomByIdHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly CustomerGetCustomByIdHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Custom custom = CreateCustomWithId();

	public CustomerGetCustomByIdHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		custom.Accept(ValidDesignerId);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests_WhenAccepted()
	{
		// Arrange
		custom.SetCategory(null);
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidDesignerId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryNameByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		), Times.Never());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests_WhenHasCategory()
	{
		// Arrange
		custom.Cancel();
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidDesignerId),
			ct
		), Times.Never());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCategoryNameByIdQuery>(x => x.Id == ValidCategoryId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Act
		CustomerGetCustomByIdDto custom = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(this.custom.Id, custom.Id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Custom);
		CustomerGetCustomByIdQuery query = new(ValidId, ValidBuyerId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorized()
	{
		// Arrange
		CustomerGetCustomByIdQuery query = new(ValidId, AccountId.New());

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
