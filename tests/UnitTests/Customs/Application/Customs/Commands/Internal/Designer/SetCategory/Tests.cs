using CustomCADs.Modules.Customs.Application.Customs.Commands.Internal.Designer.SetCategory;
using CustomCADs.Modules.Customs.Domain.Customs.Enums;
using CustomCADs.Modules.Customs.Domain.Repositories;
using CustomCADs.Modules.Customs.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Customs.Application.Customs.Commands.Internal.Designer.SetCategory;

using static Data.Customs.TestData;

public class Tests : Data.Customs.BaseUnitTests
{
	private readonly DesignerSetCustomCategoryHandler handler;
	private readonly DesignerSetCustomCategoryCommand request = new(ValidId, ValidCategoryId, ValidDesignerId);

	private readonly Mock<ICustomReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Custom custom = CreateCustom();

	public Tests()
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

		// Act
		await handler.Handle(request, ct);

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

		// Act
		await handler.Handle(request, ct);

		// Assert
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperties()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

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
		custom.Cancel();

		// Act
		Exception? ex = await Record.ExceptionAsync(
			() => handler.Handle(request with { CallerId = new() }, ct)
		);

		// Assert
		Assert.Null(ex);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCustomNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Custom);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Custom>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
