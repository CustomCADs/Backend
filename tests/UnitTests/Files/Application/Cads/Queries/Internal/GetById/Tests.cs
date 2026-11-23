using CustomCADs.Modules.Files.Application.Cads.Dtos;
using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.GetById;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.GetById;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly GetCadByIdHandler handler;
	private readonly GetCadByIdQuery request = new(ValidId);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly Cad Cad = CreateCad();

	public Tests()
	{
		handler = new(reads.Object, cache.Object, sender.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Cad>>>()))
			.Returns(async (CadId id, Func<Task<Cad>> factory) => await factory());

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(Cad);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == Cad.OwnerId)
		)).ReturnsAsync("John Kiriakou");
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request).ConfigureAwait(false);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Cad>>>()),
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
			x => x.SingleByIdAsync(ValidId, false, ct),
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
			x => x.SendQueryAsync(It.Is<GetUsernameByIdQuery>(x => x.Id == Cad.OwnerId)),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		CadDto result = await handler.Handle(request).ConfigureAwait(false);

		// Assert
		using (Assert.Multiple())
		{
			await Assert.That(result.Id).IsEqualTo(Cad.Id);
			await Assert.That(result.Key).IsEqualTo(Cad.Key);
			await Assert.That(result.ContentType).IsEqualTo(Cad.ContentType);
			await Assert.That(result.Volume).IsEqualTo(Cad.Volume);
			await Assert.That(result.CamCoordinates.X).IsEqualTo(Cad.CamCoordinates.X);
			await Assert.That(result.CamCoordinates.Y).IsEqualTo(Cad.CamCoordinates.Y);
			await Assert.That(result.CamCoordinates.Z).IsEqualTo(Cad.CamCoordinates.Z);
			await Assert.That(result.PanCoordinates.X).IsEqualTo(Cad.PanCoordinates.X);
			await Assert.That(result.PanCoordinates.Y).IsEqualTo(Cad.PanCoordinates.Y);
			await Assert.That(result.PanCoordinates.Z).IsEqualTo(Cad.PanCoordinates.Z);
			await Assert.That(result.OwnerId).IsEqualTo(Cad.OwnerId);
		}
	}
}
