using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;
using FluentValidation.TestHelper;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ResetPasswordEmail;

using static Data.Users.TestData;

public class Validator : Data.Users.BaseUnitTests
{
	private readonly ResetPasswordEmailValidator validator;
	private readonly ResetPasswordEmailCommand request = new(ValidEmail);

	private readonly Mock<IUserService> service = new();

	public Validator()
	{
		validator = new(service.Object);

		service.Setup(x => x.GetIsSSOByEmailAsync(ValidEmail))
			.ReturnsAsync(false);
	}

	[Test]
	public async Task Validate_ShouldBeValid_WhenAccountIsNotSSO()
	{
		// Arrange

		// Act
		var result = await validator.TestValidateAsync(request);

		// Assert
		await Assert.That(result.IsValid).IsTrue();
	}

	[Test]
	public async Task Validate_ShouldBeInvalid_WhenAccountIsSSO()
	{
		// Arrange
		service.Setup(x => x.GetIsSSOByEmailAsync(ValidEmail))
			.ReturnsAsync(true);

		// Act
		var result = await validator.TestValidateAsync(request);

		// Assert
		await Assert.That(result.IsValid).IsFalse();
	}
}
