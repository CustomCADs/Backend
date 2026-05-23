using CustomCADs.Modules.Customs.Application.Customs.Queries.Internal.Designer.GetById;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Categories.Queries;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Queries.Internal.Designers.GetById;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly DesignerGetCustomByIdHandler handler;
	private readonly DesignerGetCustomByIdQuery request = new(ValidId, ValidDesignerId);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Custom custom = CreateCustom();

	public Tests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
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

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		custom.SetCategory(null);

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
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

	[Fact]
	public async Task Handle_ShouldSendRequests_WhenHasCategory()
	{
		// Arrange

		// Act
		await handler.Handle(request with { CallerId = ValidBuyerId }, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetCategoryNameByIdQuery>(x => x.Id == ValidCategoryId),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		DesignerGetCustomByIdDto custom = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(ValidId, custom.Id);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		custom.Accept(ValidDesignerId);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			() => handler.Handle(request with { CallerId = new() }, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldNotThrowException_WhenUnauthorizedAccessButPending()
	{
		// Arrange

		// Act
		Exception? ex = await Record.ExceptionAsync(
			() => handler.Handle(request with { CallerId = new() }, ct)
		);

		// Assert
		Assert.Null(ex);
	}
}
