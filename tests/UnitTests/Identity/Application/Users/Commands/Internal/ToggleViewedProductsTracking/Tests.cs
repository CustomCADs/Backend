using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Identity;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly ToggleViewedProductsTrackingHandler handler;
	private readonly ToggleViewedProductsTrackingCommand request = new(MaxValidUsername, ValidAccountId);

	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const bool InitialTrackViewedProducts = false;

	public Tests()
	{
		handler = new(sender.Object, raiser.Object);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(new AccountInfoDto(ValidAccountId, default, InitialTrackViewedProducts, null, null));
	}

	[Test]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(
				It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
				ct
			),
			Times.Once()
		);
	}

	[Test]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		raiser.Verify(
			x => x.RaiseApplicationEventAsync(
				It.Is<UserEditedApplicationEvent>(x =>
					x.TrackViewedProducts == !InitialTrackViewedProducts
					&& x.Id == ValidAccountId
				)
			),
			Times.Once()
		);
	}
}
