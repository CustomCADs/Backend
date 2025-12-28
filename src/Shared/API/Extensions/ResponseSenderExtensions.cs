using CustomCADs.Shared.Domain.Extensions;
using CustomCADs.Shared.Domain.Querying;
using FastEndpoints;

namespace CustomCADs.Shared.API.Extensions;

public static class ResponseSenderExtensions
{
	extension(IResponseSender sender)
	{
		public Task MappedAsync<TDto, TResponse>(TDto dto, Func<TDto, TResponse> converter)
			=> sender.HttpContext.Response.SendOkAsync(converter(dto));

		public Task MappedAsync<TDto, TResponse>(ICollection<TDto> collection, Func<TDto, TResponse> converter)
			=> sender.HttpContext.Response.SendOkAsync(collection.Select(converter));

		public Task MappedAsync<TKey, TDto, TResponse>(Dictionary<TKey, TDto> dictionary, Func<TDto, TResponse> converter) where TKey : notnull
			=> sender.HttpContext.Response.SendOkAsync(
				dictionary.ToDictionary(x => x.Key, x => converter(x.Value))
			);

		public Task MappedAsync<TDto, TResponse>(Result<TDto> result, Func<TDto, TResponse> converter)
			=> sender.HttpContext.Response.SendOkAsync(result.ToNewResult(converter));
	}
}
