using CustomCADs.Modules.Printing.Application.Materials.Commands.Internal.Create;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Queries;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Commands.Internal.Create;

using static MaterialsData;

public class CreateMaterialHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly CreateMaterialHandler handler;
	private readonly Mock<IWrites<Material>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly Material material = CreateMaterialWithId();

	public CreateMaterialHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object, sender.Object);

		writes.Setup(x => x.AddAsync(It.Is<Material>(x => x.Id == material.Id), ct))
			.ReturnsAsync(CreateMaterialWithId(id: ValidId));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<ImageExistsByIdQuery>(x => x.Id == ValidTextureId),
			ct
		)).ReturnsAsync(true);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureId: ValidTextureId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<ImageExistsByIdQuery>(x => x.Id == ValidTextureId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureId: ValidTextureId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(It.Is<Material>(x => x.Id == material.Id), ct), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureId: ValidTextureId
		);

		// Act
		MaterialId id = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, id);
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		CreateMaterialCommand command = new(
			Name: MaxValidName,
			Density: MaxValidDensity,
			Cost: MaxValidCost,
			TextureId: ValidTextureId
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.UpdateAsync(ValidId, It.Is<Material>(x => x.Id == material.Id)), Times.Once());
	}
}
