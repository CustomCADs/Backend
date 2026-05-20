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
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<IWrites<Cad>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private readonly Cad[] cads = [
		CreateCad(),
	];
	private readonly CadId[] Ids = [ValidId];
	private readonly CadQuery query;
	private readonly Result<Cad> result;

	public Tests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object);

		query = new(new(1, Ids.Length), null, Ids);
		result = new Result<Cad>(cads.Length, cads);

		reads.Setup(x => x.AllAsync(query, false, ct))
			.ReturnsAsync(result);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		BatchDuplicateCadByIdCommand command = new(Ids, ValidOwnerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.AllAsync(query, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		BatchDuplicateCadByIdCommand command = new(Ids, ValidOwnerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddRangeAsync(It.Is<ICollection<Cad>>(x => x.Count == result.Count), ct), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldWriteToCache()
	{
		// Arrange
		BatchDuplicateCadByIdCommand command = new(Ids, ValidOwnerId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(
			x => x.UpdateAsync(
				CadId.New(Guid.Empty),
				It.Is<Cad>(x => cads.Any(c => x.Key == c.Key))
			),
			Times.Exactly(cads.Length)
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		BatchDuplicateCadByIdCommand command = new(Ids, ValidOwnerId);

		// Act
		var result = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(cads.Select(x => x.Id), result.Select(x => x.Key));
	}
}
