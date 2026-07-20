using CustomCADs.Modules.Files.Application.Cads.Queries.Internal.PresignedUrls.Post;
using CustomCADs.Modules.Files.Application.Cads.Storage;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Post;

using static Data.Cads.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly GetCadPresignedUrlPostHandler handler;
	private readonly GetCadPresignedUrlPostQuery request = new(Name, Upload.Request, Type, ValidOwnerId);

	private readonly Mock<ICadStorageService> storage = new();

	public const string Name = "CustomCAD";
	private const FileContextType Type = FileContextType.Product;
	public static readonly (UploadFileRequest Request, UploadFileResponse Response) Upload = (
		new("content-type", "file-name"),
		new("generated-key", "presigned-url")
	);

	public Tests()
	{
		handler = new(storage.Object, policies: [new PolicyMock()]);

		storage.Setup(x => x.GetPresignedPostUrlAsync(Name, Upload.Request))
			.ReturnsAsync(Upload.Response);
	}

	[Test]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		storage.Verify(
			x => x.GetPresignedPostUrlAsync(Name, Upload.Request),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		UploadFileResponse result = await handler.Handle(request, ct);

		// Assert
		await Assert.That(result).IsEqualTo(Upload.Response);
	}
}