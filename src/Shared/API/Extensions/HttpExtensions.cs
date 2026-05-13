using Microsoft.AspNetCore.Http;

namespace CustomCADs.Shared.API.Extensions;

public static class HttpExtensions
{
	extension(HttpRequest request)
	{
		public bool IsSignalR => request.Path.StartsWithSegments($"/{APIConstants.RequestPrefixForSignalR}");

		public bool IsGetWithEmptyBody =>
			HttpMethods.IsGet(request.Method)
			&& request is { ContentLength: null or 0 };

		public bool IsIdempotentBySpec =>
			HttpMethods.IsGet(request.Method)
			|| HttpMethods.IsPut(request.Method)
			|| HttpMethods.IsDelete(request.Method);

		public bool IsMutationBySpec =>
			HttpMethods.IsPost(request.Method)
			|| HttpMethods.IsPut(request.Method)
			|| HttpMethods.IsPatch(request.Method)
			|| HttpMethods.IsDelete(request.Method);

		public Dictionary<string, string?> HeadersDictionary =>
			request.Headers.ToDictionary(
				x => x.Key,
				x => x.Value.FirstOrDefault()
			);

		public bool TryGetIdempotencyKey(out Guid idempotencyKey, string idempotencyHeader = "Idempotency-Key")
		{
			string? header = request.Headers[idempotencyHeader];
			if (string.IsNullOrWhiteSpace(header))
			{
				string? param = request.Query.FirstOrDefault(
					x => x.Key.Equals("idempotencyKey", StringComparison.OrdinalIgnoreCase)
				).Value;

				if (string.IsNullOrWhiteSpace(param))
				{
					idempotencyKey = Guid.Empty;
					return false;
				}
				header = param;
			}

			if (!Guid.TryParse(header, out idempotencyKey))
			{
				idempotencyKey = Guid.Empty;
				return false;
			}
			return true;
		}
	}

	extension(HttpContext context)
	{
		public TAttribute? GetAttribute<TAttribute>() where TAttribute : Attribute
			=> context.GetEndpoint()?.Metadata.GetMetadata<TAttribute>();

		public bool HasAttribute<TAttribute>() where TAttribute : Attribute
			=> context.GetEndpoint()?.Metadata.GetMetadata<TAttribute>() is not null;
	}
}
