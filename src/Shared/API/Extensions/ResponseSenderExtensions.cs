using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.Querying;
using FastEndpoints;

namespace CustomCADs.Shared.API.Extensions;

public static class ResponseSenderExtensions
{
	public static Task MappedAsync<TDto, TResponse>(
		this IResponseSender sender,
		TDto dto,
		Func<TDto, TResponse> converter
	) => sender.HttpContext.Response.SendOkAsync(converter(dto));

	public static Task MappedAsync<TDto, TResponse>(
		this IResponseSender sender,
		ICollection<TDto> collection,
		Func<TDto, TResponse> converter
	) => sender.HttpContext.Response.SendOkAsync(collection.Select(converter));

	public static Task MappedAsync<TKey, TDto, TResponse>(
		this IResponseSender sender,
		Dictionary<TKey, TDto> dictionary,
		Func<TDto, TResponse> converter
	) where TKey : notnull
		=> sender.HttpContext.Response.SendOkAsync(
			dictionary.ToDictionary(x => x.Key, x => converter(x.Value))
		);

	public static Task MappedAsync<TDto, TResponse>(
		this IResponseSender sender,
		Result<TDto> result,
		Func<TDto, TResponse> converter
	) => sender.HttpContext.Response.SendOkAsync(result.ToNewResult(converter));
}
