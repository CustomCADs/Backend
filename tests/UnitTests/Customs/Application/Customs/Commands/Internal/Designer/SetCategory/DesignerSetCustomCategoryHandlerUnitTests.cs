using CustomCADs.Customs.Application.Customs.Commands.Internal.Designer.SetCategory;
using CustomCADs.Customs.Domain.Customs.Enums;
using CustomCADs.Customs.Domain.Repositories;
using CustomCADs.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.SetCategory;

using static CustomsData;

public class DesignerSetCustomCategoryHandlerUnitTests : CustomsBaseUnitTests
{
	private readonly DesignerSetCustomCategoryHandler handler;
	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Custom custom = CreateCustom();

	public DesignerSetCustomCategoryHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);

		custom.Accept(ValidDesignerId);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(custom);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DesignerSetCustomCategoryCommand command = new(
			Id: ValidId,
			CategoryId: ValidCategoryId,
			CallerId: ValidDesignerId
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
		DesignerSetCustomCategoryCommand command = new(
			Id: ValidId,
			CategoryId: ValidCategoryId,
			CallerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange
		DesignerSetCustomCategoryCommand command = new(
			Id: ValidId,
			CategoryId: ValidCategoryId,
			CallerId: ValidDesignerId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(ValidCategoryId, custom.Category?.Id),
			() => Assert.Equal(CustomCategorySetter.Designer, custom.Category?.Setter)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
	{
		// Arrange
		DesignerSetCustomCategoryCommand command = new(
			Id: ValidId,
			CategoryId: ValidCategoryId,
			CallerId: AccountId.New()
		);

		// Assert
		await Assert.ThrowsAsync<CustomAuthorizationException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}

	[Fact]
	public async Task Handle_ShouldNotThrowException_WhenUnauthorizedAccessButPending()
	{
		// Arrange
		custom.Cancel();
		DesignerSetCustomCategoryCommand command = new(
			Id: ValidId,
			CategoryId: ValidCategoryId,
			CallerId: AccountId.New()
		);

		// Act
		// Assert
		await handler.Handle(command, ct);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		DesignerSetCustomCategoryCommand command = new(
			Id: ValidId,
			CategoryId: ValidCategoryId,
			CallerId: ValidDesignerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
