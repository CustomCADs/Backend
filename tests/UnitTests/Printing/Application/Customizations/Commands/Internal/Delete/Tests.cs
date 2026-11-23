using CustomCADs.Modules.Printing.Application.Customizations.Commands.Shared.Delete;
using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Customizations.Commands;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Commands.Internal.Delete;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly DeleteCustomizationHandler handler;
	private readonly DeleteCustomizationByIdCommand request = new(ValidId);

	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IWrites<Customization>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Customization customization = CreateCustomization();

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(customization);
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
		writes.Verify(
			x => x.Remove(customization),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldThrowException_WhenCustomizationNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Customization);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Customization>>(() => handler.Handle(request, ct));
	}
}
