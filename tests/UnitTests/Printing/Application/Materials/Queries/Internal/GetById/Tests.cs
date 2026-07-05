using CustomCADs.Modules.Printing.Application.Materials.Dtos;
using CustomCADs.Modules.Printing.Application.Materials.Queries.Internal.GetById;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Queries.Internal.GetById;

using static Data.Materials.TestData;

public class Tests : Data.Materials.BaseUnitTests
{
	private readonly GetMaterialByIdHandler handler;
	private readonly GetMaterialByIdQuery request = new(ValidId);

	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private readonly Material material = CreateMaterial();

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()))
			.ReturnsAsync(material);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Material>>>()),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		MaterialDto response = await handler.Handle(request, ct);

		// Assert
		await Assert.That(response.Id).IsEqualTo(material.Id);
	}
}
