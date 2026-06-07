using CustomCADs.Modules.Printing.Application.Materials.Dtos;
using CustomCADs.Modules.Printing.Application.Materials.Queries.Internal.GetAll;
using CustomCADs.Modules.Printing.Domain.Materials;
using CustomCADs.Modules.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Queries.Internal.GetAll;

public class Tests : Data.Materials.BaseUnitTests
{
	private readonly GetAllMaterialsHandler handler;
	private readonly GetAllMaterialsQuery request = new();

	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();

	private static readonly ICollection<Material> Materials = [
		CreateMaterial(),
		CreateMaterial(),
		CreateMaterial(),
	];

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Material>>>>()))
			.ReturnsAsync(Materials);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(It.IsAny<Func<Task<ICollection<Material>>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		ICollection<MaterialDto> response = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(Materials.Count, response.Count);
	}
}
