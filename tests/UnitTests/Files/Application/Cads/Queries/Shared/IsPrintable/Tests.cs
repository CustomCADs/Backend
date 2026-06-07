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

	private readonly Cad Cad = CreateCad(contentType: "not-a-printable-content-type");

	public Tests()
	{
		handler = new(reads.Object, cache.Object);
		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		)).ReturnsAsync(Cad);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		cache.Verify(
			x => x.GetOrCreateAsync(
				ValidId,
				It.IsAny<Func<Task<Cad>>>()
			),
			Times.Once()
		);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public async Task Handle_ShouldReturnResult(bool exists)
	{
		// Arrange
		if (exists)
		{
			Cad.SetContentType(ApplicationConstants.Cads.PrintableContentTypes.First());
		}

		// Act
		bool result = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
