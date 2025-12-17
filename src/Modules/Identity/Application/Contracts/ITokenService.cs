using CustomCADs.Modules.Identity.Application.Users.Dtos;
using CustomCADs.Modules.Identity.Domain.Users.Entities;

namespace CustomCADs.Modules.Identity.Application.Contracts;

public interface ITokenService
{
	RefreshToken IssueRefreshToken(Func<string, RefreshToken> createRefreshToken);
	TokensDto IssueTokens(User user, RefreshToken refreshToken);
}
