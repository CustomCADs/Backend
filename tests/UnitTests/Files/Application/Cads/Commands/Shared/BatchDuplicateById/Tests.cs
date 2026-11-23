using CustomCADs.Modules.Files.Application.Cads.Commands.Shared.BatchDuplicateById;
using CustomCADs.Modules.Files.Domain.Repositories;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Domain.Querying;

namespace CustomCADs.UnitTests.Files.Application.Cads.Commands.Shared.BatchDuplicateById;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly BatchDuplicateCadByIdHandler handler;
	private readonly BatchDuplicateCadByIdCommand request = new(Ids, ValidOwnerId);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private static readonly Cad[] Cads = [
		CreateCad(),
	];
	private static readonly CadId[] Ids = [ValidId];
	private static readonly CadQuery Query = new(new(1, Ids.Length), null, Ids);
	private static readonly Result<Cad> Result = new(Cads.Length, Cads);

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		reads.Setup(x => x.AllAsync(Query, false, ct))
			.ReturnsAsync(Result);
	}

	[Test]
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

	[Test]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		writes.Verify(
			x => x.AddRangeAsync(It.Is<ICollection<Cad>>(x => x.Count == Result.Count), ct),
			Times.Once()
		);
		uow.Verify(
			x => x.SaveChangesAsync(ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(
				CadId.New(Guid.Empty),
				It.Is<Cad>(x => Cads.Any(c => x.Key == c.Key))
			),
			Times.Exactly(Cads.Length)
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		var result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result.Select(x => x.Key)).IsEquivalentTo(Cads.Select(x => x.Id));
	}
}
