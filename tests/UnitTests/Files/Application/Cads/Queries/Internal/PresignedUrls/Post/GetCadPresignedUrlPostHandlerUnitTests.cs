using CustomCADs.Files.Application.Cads.Queries.Internal.PresignedUrls.Post;
using CustomCADs.Files.Application.Cads.Storage;
using CustomCADs.Shared.Application.Dtos.Files;
using CustomCADs.Shared.Application.Policies;

namespace CustomCADs.UnitTests.Files.Application.Cads.Queries.Internal.PresignedUrls.Post;

using static CadsData;

public class GetCadPresignedUrlPostHandlerUnitTests : CadsBaseUnitTests
{
	private readonly GetCadPresignedUrlPostHandler handler;
	private readonly Mock<ICadStorageService> storage = new();

	public const string Name = "CustomCAD";
	private const FileContextType Type = FileContextType.Product;
	public static readonly UploadFileRequest req = new("content-type", "file-name");
	public static readonly UploadFileResponse res = new("generated-key", "presigned-url");

	public GetCadPresignedUrlPostHandlerUnitTests()
	{
		handler = new(storage.Object, policies: []);

		storage.Setup(x => x.GetPresignedPostUrlAsync(Name, req))
			.ReturnsAsync(res);
	}

	[Fact]
	public async Task Handle_ShouldCallStorage()
	{
		// Arrange
		GetCadPresignedUrlPostQuery query = new(Name, req, Type, ValidOwnerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		storage.Verify(x => x.GetPresignedPostUrlAsync(Name, req), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetCadPresignedUrlPostQuery query = new(Name, req, Type, ValidOwnerId);

		// Act
		UploadFileResponse result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(res, result);
	}
}
