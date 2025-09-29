using CustomCADs.Shared.Domain.TypedIds.Accounts;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Identity.Infrastructure.Identity.ShadowEntities;

public class AppUser : IdentityUser<Guid>
{
	private List<AppRefreshToken> refreshTokens = [];
	private string? provider;

	public AppUser() : base() { }
	public AppUser(string username, string email, AccountId accountId)
		: base(username)
	{
		Email = email;
		AccountId = accountId;
	}

	public AccountId AccountId { get; set; }
	public bool IsSSO { get; set; }
	public string? Provider
	{
		get => ComputeProvider(provider);
		set
		{
			provider = ComputeProvider(value);
			UserName = FormatUsername(ResolveUsername(UserName ?? string.Empty));
		}
	}
	public string Username
	{
		get => ResolveUsername(UserName ?? string.Empty);
		set => UserName = FormatUsername(value);
	}
	public IReadOnlyCollection<AppRefreshToken> RefreshTokens => refreshTokens;

	internal AppUser FillRefreshTokens(ICollection<AppRefreshToken> refreshTokens)
	{
		this.refreshTokens = [.. refreshTokens];
		return this;
	}

	private string FormatUsername(string username)
		=> IsSSO ? $"{Provider}/{username}" : username;

	private string ResolveUsername(string username)
		=> IsSSO ? username.Split("/").Last() : username;

	private string? ComputeProvider(string? provider)
		=> IsSSO ? provider : null;
}
