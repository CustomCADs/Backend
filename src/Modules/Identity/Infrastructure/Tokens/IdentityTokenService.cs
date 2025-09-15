using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CustomCADs.Identity.Infrastructure.Tokens;

using static DomainConstants.Tokens;

public sealed class IdentityTokenService(IOptions<JwtSettings> jwtOptions) : ITokenService
{
	private const string Algorithm = SecurityAlgorithms.HmacSha256;
	private readonly JwtSettings jwtSettings = jwtOptions.Value;

	public RefreshToken IssueRefreshToken(Func<string, RefreshToken> createRefreshToken)
		=> createRefreshToken(
			Base64UrlEncoder.Encode(inArray: GenerateRandomValue())
		);

	public TokensDto IssueTokens(User user, RefreshToken refreshToken)
		=> new(
			Role: user.Role,
			AccessToken: GenerateAccessToken(
				accountId: user.AccountId,
				username: user.Username,
				role: user.Role
			),
			RefreshToken: new(
				Value: refreshToken.Value,
				ExpiresAt: refreshToken.ExpiresAt
			),
			CsrfToken: new(
				Value: Convert.ToBase64String(inArray: GenerateRandomValue()),
				ExpiresAt: DateTime.UtcNow.AddMinutes(JwtDurationInMinutes)
			)
		);

	private TokenDto GenerateAccessToken(AccountId accountId, string username, string role)
	{
		List<Claim> claims =
		[
			new(ClaimTypes.NameIdentifier, accountId.ToString()),
			new(ClaimTypes.Name, username),
			new(ClaimTypes.Role, role),
		];
		SymmetricSecurityKey security = new(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

		JwtSecurityToken token = new(
			issuer: jwtSettings.Issuer,
			audience: jwtSettings.Audience,
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(JwtDurationInMinutes),
			signingCredentials: new(security, Algorithm)
		);

		string jwt = new JwtSecurityTokenHandler().WriteToken(token);
		return new(jwt, token.ValidTo);
	}

	private static byte[] GenerateRandomValue()
	{
		byte[] randomNumber = new byte[32];
		RandomNumberGenerator.Fill(randomNumber);
		return randomNumber;
	}
}
