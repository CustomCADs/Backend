using CustomCADs.Modules.Files.Application.Cads.Commands.Internal.SetCoords;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Exceptions;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Internal.SetCoords;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly SetCadCoordsHandler handler;
	private readonly SetCadCoordsCommand request = new(
		Id: ValidId,
		CamCoordinates: CamCoords,
		PanCoordinates: PanCoords,
		CallerId: ValidOwnerId
	);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private static readonly CoordinatesDto CamCoords = new(MinValidCoord, MinValidCoord, MinValidCoord);
	private static readonly CoordinatesDto PanCoords = new(MaxValidCoord, MaxValidCoord, MaxValidCoord);
	private readonly Cad cad = CreateCad();

	public Tests()
	{
		handler = new(reads.Object, uow.Object, cache.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(cad);
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
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(ValidId, cad),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldModifyCad()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(CamCoords.X, cad.CamCoordinates.X),
			() => Assert.Equal(CamCoords.Y, cad.CamCoordinates.Y),
			() => Assert.Equal(CamCoords.Z, cad.CamCoordinates.Z),
			() => Assert.Equal(PanCoords.X, cad.PanCoordinates.X),
			() => Assert.Equal(PanCoords.Y, cad.PanCoordinates.Y),
			() => Assert.Equal(PanCoords.Z, cad.PanCoordinates.Z)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCadNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct))
			.ReturnsAsync(null as Cad);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Cad>>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
