using CustomCADs.Modules.Files.Application.Cads.Dtos;
using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.GetAll;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.Querying;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.GetAll;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly GetAllCadsHandler handler;
	private readonly GetAllCadsQuery request = new(ValidOwnerId, Query.Pagination);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly CadQuery Query = new(
		Pagination: new(1, 10)
	);
	private static readonly ICollection<Cad> Cads = [
		CreateCad(id: CadId.New(), ownerId: AccountId.New()),
		CreateCad(id: CadId.New(), ownerId: AccountId.New()),
		CreateCad(id: CadId.New(), ownerId: AccountId.New()),
		CreateCad(id: CadId.New(), ownerId: AccountId.New()),
	];

	public Tests()
	{
		handler = new(reads.Object, cache.Object, sender.Object);

		cache.Setup(x => x.GetOrCreateAsync(It.IsAny<Func<Task<Result<Cad>>>>()))
			.Returns(async (Func<Task<Result<Cad>>> factory) => await factory());

		reads.Setup(x => x.AllAsync(It.Is<CadQuery>(x => x.Pagination == Query.Pagination), false, ct))
			.ReturnsAsync(new Result<Cad>(Cads.Count, Cads));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<BatchGetUsernamesByIdQuery>(x => x.Ids.Length == Cads.Count())
		)).ReturnsAsync(Cads.ToDictionary(x => x.OwnerId, x => x.Key));
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request).ConfigureAwait(false);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(It.IsAny<Func<Task<Result<Cad>>>>()),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request).ConfigureAwait(false);

		// Assert
		reads.Verify(
			x => x.AllAsync(It.Is<CadQuery>(x => x.Pagination == Query.Pagination), false, ct),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request).ConfigureAwait(false);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(It.Is<BatchGetUsernamesByIdQuery>(x => x.Ids.Length == Cads.Count())),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		Result<CadDto> result = await handler.Handle(request).ConfigureAwait(false);

		// Assert
		await Assert.That(result.Items.Select(x => x.Id)).IsEquivalentTo(Cads.Select(x => x.Id));
	}
}
