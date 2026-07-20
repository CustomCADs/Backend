using CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Edit;
using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Commands.Internal.Edit;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly EditCustomizationHandler handler;
	private readonly EditCustomizationCommand request = new(
		Id: ValidId,
		Scale: MaxValidScale,
		Infill: MaxValidInfill,
		Volume: MaxValidVolume,
		Color: ValidColor,
		MaterialId: ValidMaterialId
	);

	private readonly Mock<ICustomizationReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private readonly Customization customization = CreateCustomization();

	public Tests()
	{
		handler = new(reads.Object, uow.Object);

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
