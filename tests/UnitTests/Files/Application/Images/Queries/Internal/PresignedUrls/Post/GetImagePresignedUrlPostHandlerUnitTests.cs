using CustomCADs.Modules.Files.Application.Images.Queries.Internal.PresignedUrls.Post;
using CustomCADs.Modules.Files.Application.Images.Storage;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;
using CustomCADs.UnitTests.Files.Application.Cads;

namespace CustomCADs.UnitTests.Files.Application.Images.Queries.Internal.PresignedUrls.Post;

using static ImagesData;

public class GetImagePresignedUrlPostHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetImagePresignedUrlPostHandler handler;
	private readonly Mock<IImageStorageService> storage = new();

	public const string Name = "CustomCAD";
	private const FileContextType Type = FileContextType.Product;
	public static readonly UploadFileRequest req = new("content-type", "file-name");
	public static readonly UploadFileResponse res = new("generated-key", "presigned-url");

	public GetImagePresignedUrlPostHandlerUnitTests()
	{
		handler = new(storage.Object, policies: [new ImageUploadPolicyMock()]);

		storage.Setup(x => x.GetPresignedPostUrlAsync(Name, req))
			.ReturnsAsync(res);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetImagePresignedUrlPostQuery query = new(Name, req, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPostUrlAsync(Name, req), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetImagePresignedUrlPostQuery query = new(Name, req, Type, ValidOwnerId);

		// Act
		UploadFileResponse result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(res, result);
	}
}
