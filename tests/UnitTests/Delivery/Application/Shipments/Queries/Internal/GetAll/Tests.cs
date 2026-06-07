using CustomCADs.Modules.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Modules.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetAll;


public class Tests : Data.Shipments.BaseUnitTests
{
	private readonly GetAllShipmentsHandler handler;
	private readonly GetAllShipmentsQuery request = new(new(), null, null);

	private readonly Mock<IShipmentReads> reads = new();

	private static readonly Shipment[] Shipments = [
		CreateShipment(),
		CreateShipment(),
		CreateShipment(),
		CreateShipment(),
	];
	private static readonly ShipmentQuery Query = new(new(), null, null);

	public Tests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.AllAsync(Query, false, ct))
			.ReturnsAsync(new Result<Shipment>(Shipments.Length, Shipments));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(Query, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		Result<GetAllShipmentsDto> result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(result.Items.Select(r => r.Address), Shipments.Select(r => r.Address));
	}
}
