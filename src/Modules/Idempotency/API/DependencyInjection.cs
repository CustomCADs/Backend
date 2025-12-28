using CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Commands.Internal.Complete;
using CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Commands.Internal.Create;
using CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Commands.Internal.Delete;
using CustomCADs.Modules.Idempotency.Application.IdempotencyKeys.Queries.Internal.Get;
using CustomCADs.Modules.Idempotency.Domain.IdempotencyKeys;
using CustomCADs.Shared.API.Attributes;
using CustomCADs.Shared.API.Extensions;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Domain.TypedIds.Idempotency;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

#pragma warning disable IDE0130
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
	extension(HttpRequest request)
	{
		internal bool IsIdempotent => request.IsIdempotentBySpec || HttpMethods.IsPatch(request.Method);

		internal async Task<string> GenerateRequestHashAsync(Endpoint? endpoint)
		{
			request.EnableBuffering();

			using StreamReader reader = new(
				stream: request.Body,
				encoding: Encoding.UTF8,
				detectEncodingFromByteOrderMarks: false,
				bufferSize: 1024,
				leaveOpen: true
			);
			string body = await reader.ReadToEndAsync().ConfigureAwait(false);

			request.Body.Position = 0;
			string endpointId = endpoint?.DisplayName ?? "unknown-endpoint";
			return ComputeHash($"{endpointId}|{body}");
		}
	}

	extension(IApplicationBuilder app)
	{
		public void UseIdempotencyKeys()
			=> app.Use(async (context, next) =>
			{
				if (!context.HasAttribute<EnforceIdempotencyAttribute>())
				{
					if (context.HasAttribute<SkipIdempotencyAttribute>() || context.Request.IsIdempotent || context.Request.IsSignalR)
					{
						await next().ConfigureAwait(false);
						return;
					}
				}

				if (!context.Request.TryGetIdempotencyKey(out Guid idempotencyKey))
				{
					throw new CustomException($"A valid Idempotency Key header required!");
				}

				CancellationToken ct = context.RequestAborted;
				string requestHash = await context.Request.GenerateRequestHashAsync(context.GetEndpoint()).ConfigureAwait(false);

				IRequestSender sender = context.RequestServices.GetRequiredService<IRequestSender>();
				IProblemDetailsService problemDetails = context.RequestServices.GetRequiredService<IProblemDetailsService>();

				try
				{
					GetIdempotencyKeyDto? dto = await sender.SendQueryAsync(
						new GetIdempotencyKeyQuery(idempotencyKey, requestHash),
						ct: ct
					).ConfigureAwait(false);

					if (dto is null)
					{
						await problemDetails.ConflictResponseAsync(
							context: context,
							ex: new CustomException("Too many identical requests at once!")
						).ConfigureAwait(false);
						return;
					}

					context.Response.StatusCode = dto.StatusCode;
					if (!string.IsNullOrWhiteSpace(dto.ResponseBody))
					{
						await context.Response.WriteAsync(dto.ResponseBody).ConfigureAwait(false);
					}
				}
				catch (CustomNotFoundException<IdempotencyKey>)
				{
					IdempotencyKeyId id = await sender.SendCommandAsync(
						new CreateIdempotencyKeyCommand(
							IdempotencyKey: idempotencyKey,
							RequestHash: requestHash
						),
						ct: ct
					).ConfigureAwait(false);

					Stream originalBody = context.Response.Body;
					using MemoryStream stream = new();
					context.Response.Body = stream;

					try
					{
						await next().ConfigureAwait(false);
					}
					catch
					{
						context.Response.Body = originalBody;

						await sender.SendCommandAsync(
							new DeleteIdempotencyKeyCommand(
								IdempotencyKey: idempotencyKey,
								RequestHash: requestHash
							),
							ct: ct
						).ConfigureAwait(false);

						throw;
					}

					string responseBody = await context.Response.ExtractResponseBodyAsync(
						stream: stream,
						original: originalBody
					).ConfigureAwait(false);

					await sender.SendCommandAsync(
						new CompleteIdempotencyKeyCommand(
							Id: id,
							RequestHash: requestHash,
							ResponseBody: responseBody,
							StatusCode: context.Response.StatusCode
						),
						ct: ct
					).ConfigureAwait(false);
				}
			});
	}

	extension(HttpResponse response)
	{
		private async Task<string> ExtractResponseBodyAsync(MemoryStream stream, Stream original)
		{
			stream.Position = 0;
			using StreamReader reader = new(stream);
			string body = await reader.ReadToEndAsync().ConfigureAwait(false);

			stream.Position = 0;
			response.Body = original;
			await stream.CopyToAsync(original).ConfigureAwait(false);

			return body;
		}
	}

	private static string ComputeHash(string input)
		=> Convert.ToBase64String(
			inArray: SHA256.HashData(Encoding.UTF8.GetBytes(input))
		);
}
