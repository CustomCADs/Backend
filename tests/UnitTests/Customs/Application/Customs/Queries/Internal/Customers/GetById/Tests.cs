using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Customers.GetById;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Customers.GetById;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly CustomerGetCustomByIdHandler handler;
	private readonly CustomerGetCustomByIdQuery request = new(ValidId, ValidBuyerId);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Custom custom = CreateCustom();

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		custom.Accept(ValidDesignerId);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
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
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests_WhenAccepted()
	{
		// Arrange
		custom.SetCategory(null);

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidDesignerId),
				ct
			),
			Times.Once()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCategoryNameByIdQuery>(x => x.Id == ValidCategoryId),
				ct
			),
			Times.Never()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests_WhenHasCategory()
	{
		// Arrange
		custom.Cancel();

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidDesignerId),
				ct
			),
			Times.Never()
		);
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCategoryNameByIdQuery>(x => x.Id == ValidCategoryId),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CustomerGetCustomByIdDto custom = await handler.Handle(request, ct);

		// Assert
		await Assert.That(custom.Id).IsEqualTo(this.custom.Id);
	}

	[Test]
	public async Task Handle_ShouldNotThrowException_WhenCompleted()
	{
		// Arrange
		this.custom.Begin();
		this.custom.Finish(ValidCadId, ValidPrice);
		this.custom.Complete(customizationId: null);

		// Act
		CustomerGetCustomByIdDto custom = await handler.Handle(request, ct);

		// Assert
		await Assert.That(custom.Id).IsEqualTo(this.custom.Id);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(() => handler.Handle(request, ct));
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(() => handler.Handle(request with { CallerId = new() }, ct));
	}
}
