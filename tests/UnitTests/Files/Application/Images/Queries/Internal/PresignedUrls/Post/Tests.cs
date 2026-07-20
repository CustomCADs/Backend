using CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Post;
using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Post;

using static Data.Images.TestData;

public class Tests : Data.Cads.BaseUnitTests
{
	private readonly GetImagePresignedUrlPostHandler handler;
	private readonly GetImagePresignedUrlPostQuery request = new(Name, Upload.Request, Type, ValidOwnerId);

	private readonly Mock<IImageStorageService> storage = new();

	public const string Name = "CustomCAD";
	private const FileContextType Type = FileContextType.Product;
	public static readonly (UploadFileRequest Request, UploadFileResponse Response) Upload = (
		Request: new("content-type", "file-name"),
		Response: new("generated-key", "presigned-url")
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