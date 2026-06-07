using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Application.Users.Queries.Shared.ClientUrl;
using CustomCADs.Shared.Application.UseCases.Identity.Queries;
using Microsoft.Extensions.Options;

namespace CustomCADs.UnitTests.Identity.Application.Users.Queries.Shared.ClientUrl;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly GetClientUrlHandler handler;
	private readonly GetClientUrlQuery request = new();

	private readonly Mock<IOptions<ClientUrlSettings>> settings = new();

	private const string All = "preferred-url.com,www.preferred-url.com,app.preferred-url.com";
	private const string Preferred = "www.preferred-url.com";

	public Tests()
	{
		handler = new(settings.Object);

		settings.Setup(x => x.Value).Returns(new ClientUrlSettings(All, Preferred));
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange

		// Act
		string actual = await handler.Handle(request, ct);

		// Assert
		Assert.Equal(Preferred, actual);
	}
}
