using CustomCADs.Modules.Files.Application.Cads.Queries.Shared;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.IsPrintable;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly IsCadPrintableByIdHandler handler;
	private readonly IsCadPrintableByIdQuery request = new(ValidId);

	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private readonly Cad cad = CreateCad(contentType: "not-a-printable-content-type");

	public Tests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(ValidId, It.IsAny<Func<Task<Cad>>>()))
			.Returns(async (CadId id, Func<Task<Cad>> factory) => await factory());

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(cad);
	}

	[Test]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

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
		await handler.Handle(request, ct);

		// Assert
		reads.Verify(
			x => x.SingleByIdAsync(ValidId, false, ct),
			Times.Once()
		);
	}

	[Test]
	[Arguments(false)]
	[Arguments(true)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		if (exists)
		{
			cad.SetContentType(ApplicationConstants.Cads.PrintableContentTypes.First());
		}

		// Act
		bool result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result).IsEqualTo(exists);
	}
}
