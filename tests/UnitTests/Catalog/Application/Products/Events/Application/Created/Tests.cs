using CustomCADs.Modules.Catalog.Application.Products.Events.Application.ProductCreated;
using CustomCADs.Modules.Catalog.Domain.Repositories;
using CustomCADs.Modules.Catalog.Domain.Repositories.Writes;

namespace CustomCADs.UnitTests.Catalog.Application.Products.Events.Application.Created;

using static Data.Products.TestData;

public class Tests : Data.Products.BaseUnitTests
{
	private readonly ProductCreatedHandler handler;
	private readonly Mock<IProductWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly TagId[] TagIds = [];

	public Tests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		ProductCreatedApplicationEvent @event = new(ValidId, TagIds);

		// Act
		await handler.HandleAsync(@event);

		// Assert
		writes.Verify(
			x => x.AddTagAsync(ValidId, It.Is<TagId>(x => TagIds.Contains(x)), ct),
			Times.Exactly(TagIds.Length)
		);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}
}
