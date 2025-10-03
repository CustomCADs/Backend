using CustomCADs.Printing.Application.Materials.Commands.Internal.SetTexture;
using CustomCADs.Printing.Domain.Materials;
using CustomCADs.Printing.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.UseCases.Images.Commands;

namespace CustomCADs.UnitTests.Printing.Application.Materials.Commands.Internal.SetTexture;

using static MaterialsData;

public class SetMaterialTextureHandlerUnitTests : MaterialsBaseUnitTests
{
	private readonly SetMaterialTextureHandler handler;
	private readonly Mock<IMaterialReads> reads = new();
	private readonly Mock<BaseCachingService<MaterialId, Material>> cache = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string ContentType = "content-type";

	public SetMaterialTextureHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object, sender.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Material>>>()
		)).ReturnsAsync(CreateMaterial());
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		SetMaterialTextureCommand command = new(ValidId, ContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Material>>>()
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		SetMaterialTextureCommand command = new(ValidId, ContentType);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendCommandAsync(
			It.Is<EditImageCommand>(x => x.Id == ValidTextureId),
			ct
		), Times.Once());
	}
}
