using CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetAll;
using CustomCADs.Delivery.Domain.Repositories.Reads;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.UnitTests.Delivery.Application.Shipments.Queries.Internal.GetAll;


public class GetAllShipmentsHandlerUnitTests : ShipmentsBaseUnitTests
{
	private readonly GetAllShipmentsHandler handler;
	private readonly Mock<IShipmentReads> reads = new();

	private static readonly Shipment[] shipments = [
		CreateShipment(),
		CreateShipment(),
		CreateShipment(),
		CreateShipment(),
	];
	private readonly ShipmentQuery shipmentQuery = new(new(), null, null);

	public GetAllShipmentsHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.AllAsync(shipmentQuery, false, ct))
			.ReturnsAsync(new Result<Shipment>(shipments.Length, shipments));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAllShipmentsQuery query = new(
			Pagination: new(),
			CallerId: null,
			Sorting: null
		);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(
			x => x.AllAsync(shipmentQuery, false, ct),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllShipmentsQuery query = new(
			Pagination: new(),
			CallerId: null,
			Sorting: null
		);

		// Act
		Result<GetAllShipmentsDto> result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(result.Items.Select(r => r.Address), shipments.Select(r => r.Address));
	}
}
