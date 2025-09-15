using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Domain.Users.Entities;

namespace CustomCADs.Identity.Application.Contracts;

public interface ITokenService
{
	RefreshToken IssueRefreshToken(Func<string, RefreshToken> createRefreshToken);
	TokensDto IssueTokens(User user, RefreshToken refreshToken);
}
