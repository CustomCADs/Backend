using CustomCADs.Modules.Printing.Application.Customizations.Commands.Internal.Create;
using CustomCADs.Modules.Printing.Domain.Customizations;
using CustomCADs.Modules.Printing.Domain.Repositories;

namespace CustomCADs.UnitTests.Printing.Application.Customizations.Commands.Internal.Create;

using static Data.Customizations.TestData;

public class Tests : Data.Customizations.BaseUnitTests
{
	private readonly CreateCustomizationHandler handler;
	private readonly CreateCustomizationCommand request = new(
		Scale: MaxValidScale,
		Infill: MaxValidInfill,
		Volume: MaxValidVolume,
		Color: ValidColor,
		MaterialId: ValidMaterialId
	);

	private readonly Mock<IWrites<Customization>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	public Tests()
	{
		handler = new(writes.Object, uow.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<Customization>(x =>
				x.Scale == MaxValidScale
				&& x.Infill == MaxValidInfill
				&& x.Volume == MaxValidVolume
				&& x.Color == ValidColor
				&& x.MaterialId == ValidMaterialId
			),
			ct
		)).ReturnsAsync(CreateCustomization(id: ValidId));
	}

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddAsync(
				It.Is<Customization>(x =>
					x.Scale == MaxValidScale
					&& x.Infill == MaxValidInfill
					&& x.Volume == MaxValidVolume
					&& x.Color == ValidColor
					&& x.MaterialId == ValidMaterialId
				),
				ct
			),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CustomizationId id = await handler.Handle(request, ct);

		// Assert
		await Assert.That(id).IsEqualTo(ValidId);
	}
}
