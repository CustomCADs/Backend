using CustomCADs.Modules.Files.Application.Cads.Queries.Shared;
using CustomCADs.Modules.Files.Domain.Repositories.Reads;
using CustomCADs.Shared.Application;
using CustomCADs.Shared.Application.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Shared.IsPrintable;

using static CadsData;

public class IsCadPrintableByIdHandlerUnitTests : CadsBaseUnitTests
{
	private readonly IsCadPrintableByIdHandler handler;
	private readonly Mock<ICadReads> reads = new();
	private readonly Mock<BaseCachingService<CadId, Cad>> cache = new();

	private readonly Cad Cad = CreateCad(contentType: "not-a-printable-content-type");

	public IsCadPrintableByIdHandlerUnitTests()
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
		IsCadPrintableByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Cad>>>()
		), Times.Once());
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
		IsCadPrintableByIdQuery query = new(ValidId);

		// Act
		bool result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(exists, result);
	}
}
