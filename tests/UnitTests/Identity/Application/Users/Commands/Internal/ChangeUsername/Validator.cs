using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ChangeUsername;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ChangeUsername;

using static Data.Users.TestData;

public class Validator : Data.Users.BaseUnitTests
{
	private readonly ChangeUsernameValidator validator;
	private readonly ChangeUsernameCommand request = new(ValidAccountId, MaxValidUsername, null, null);

	private readonly Mock<IRequestSender> sender = new();

	private static readonly AccountInfoDto info = new(
		Id: AccountId.New(),
		CreatedAt: DateTimeOffset.MinValue,
		TrackViewedProducts: false,
		FirstName: null,
		LastName: null
	);

	public Validator()
	{
		validator = new(sender.Object);

		sender.Setup(x => x.SendQueryAsync(new GetAccountExistsByUsernameQuery(request.Username)))
			.ReturnsAsync(false);

		sender.Setup(x => x.SendQueryAsync(new GetAccountInfoByUsernameQuery(request.Username)))
			.ReturnsAsync(info);
	}

	[Test]
	public async Task Validate_ShouldBeValid_WhenUsernameIsNew()
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(request);

		// Assert
		await Assert.That(result.IsValid).IsTrue();
	}

	[Test]
	public async Task Validate_ShouldBeInvalid_WhenUsernameAlreadyExists()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(new GetAccountExistsByUsernameQuery(request.Username)))
			.ReturnsAsync(true);

		// Act
		var result = await validator.TestValidateAsync(request);

		// Assert
		await Assert.That(result.IsValid).IsFalse();
	}

	[Test]
	public async Task Validate_ShouldBeValid_WhenUsernameEqualsTheCurrentOne()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(new GetAccountExistsByUsernameQuery(request.Username)))
			.ReturnsAsync(true);
		sender.Setup(x => x.SendQueryAsync(new GetAccountInfoByUsernameQuery(request.Username)))
			.ReturnsAsync(info with { Id = request.Id });

		// Act
		var result = await validator.TestValidateAsync(request);

		// Assert
		await Assert.That(result.IsValid).IsTrue();
	}

	[Test]
	public async Task Validate_ShouldNotValidate_WhenUsernameDoesNotExist()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(new GetAccountInfoByUsernameQuery(request.Username)))
			.ThrowsAsync(CustomNotFoundException<object>.Custom("exception"));

		// Act
		await validator.TestValidateAsync(request);

		// Assert
		sender.Verify(
			x => x.SendQueryAsync(It.IsAny<GetAccountExistsByUsernameQuery>(), ct),
			Times.Never()
		);
	}
}
