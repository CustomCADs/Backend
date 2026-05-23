using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.Register;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Commands;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.Register;

using static Data.Users.TestData;

public class Tests : Data.Users.BaseUnitTests
{
	private readonly RegisterUserHandler handler;
	private readonly RegisterUserCommand request = new(
		Role: ValidRole,
		Username: MaxValidUsername,
		Email: ValidEmail,
		Password: MinValidPassword,
		FirstName: null,
		LastName: null
	);

	private readonly Mock<IUserService> service = new();
	private readonly Mock<IRequestSender> sender = new();

	public Tests()
	{
		handler = new(service.Object, sender.Object);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateAccountCommand>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(ValidAccountId);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		service.Verify(
			x => x.CreateAsync(
				It.Is<User>(x =>
					x.Username == request.Username
					&& x.AccountId == ValidAccountId
				),
				request.Password
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange

		// Act
		await handler.Handle(request, ct);

		// Assert
		sender.Verify(
			x => x.SendCommandAsync(
				It.Is<CreateAccountCommand>(x =>
					x.Role == request.Role
					&& x.Username == request.Username
					&& x.Email == request.Email
					&& x.FirstName == request.FirstName
					&& x.LastName == request.LastName
				),
				ct
			),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenRegisterUnsuccessfuly()
	{
		// Arrange
		service.Setup(x => x.CreateAsync(
			It.Is<User>(x => x.Username == MaxValidUsername),
			MinValidPassword
		)).ThrowsAsync(new CustomException("CreationErrorMessage"));

		// Assert
		await Assert.ThrowsAsync<CustomException>(
			// Act
			() => handler.Handle(request, ct)
		);
	}
}
