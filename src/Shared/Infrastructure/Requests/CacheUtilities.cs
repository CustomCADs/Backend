using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Requests.Attributes;
using CustomCADs.Shared.Application.Abstractions.Requests.Commands;
using CustomCADs.Shared.Application.Abstractions.Requests.Queries;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace CustomCADs.Shared.Infrastructure.Requests;

internal static class CacheUtilities
{
	extension(IEnumerable<AddRequestCachingAttribute> attributes)
	{
		internal Expiration CacheExpiration
		{
			get
			{
				Expiration expiration = new(Absolute: null, Sliding: null);

				foreach (AddRequestCachingAttribute attribute in attributes)
				{
					expiration = attribute.Expiration switch
					{
						{ Absolute: not null } => expiration with { Absolute = attribute.Expiration.Absolute },
						{ Sliding: not null } => expiration with { Sliding = attribute.Expiration.Sliding },
						_ => expiration,
					};
				}

				return expiration;
			}
		}
	}

	extension<TResponse>(IQuery<TResponse> request) { internal string Hash(string group) => HashRequest(request, group); }
	extension<TResponse>(ICommand<TResponse> request) { internal string Hash(string group) => HashRequest(request, group); }
	extension(ICommand request) { internal string Hash(string group) => HashRequest(request, group); }

	private static string HashRequest(object request, string group)
		=> $"{group}/{request.GetType().FullName}:{Convert.ToHexString(
			SHA256.HashData(
				source: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request))
			)
		)}";
}
