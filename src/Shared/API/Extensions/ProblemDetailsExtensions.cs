using Microsoft.AspNetCore.Http;

namespace CustomCADs.Shared.API.Extensions;

using static StatusCodes;

public static class ProblemDetailsExtensions
{
	extension(IProblemDetailsService service)
	{
		public async Task<bool> BadRequestResponseAsync(HttpContext context, Exception ex, string message = "Invalid Request Parameters")
		{
			context.Response.StatusCode = Status400BadRequest;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Status = Status400BadRequest,
				},
			}).ConfigureAwait(false);
		}

		public async Task<bool> PaymentFailedResponseAsync(HttpContext context, Exception ex, string? clientSecret, string message = "Payment Failure")
		{
			context.Response.StatusCode = Status400BadRequest;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Extensions = new Dictionary<string, object?>()
					{
						["clientSecret"] = clientSecret,
					},
					Status = Status400BadRequest,
				},
			}).ConfigureAwait(false);
		}

		public async Task<bool> UnauthorizedResponseAsync(HttpContext context, Exception ex, string message = "Inappropriately Unauthenticated")
		{
			context.Response.StatusCode = Status401Unauthorized;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Status = Status401Unauthorized,
				},
			}).ConfigureAwait(false);
		}

		public async Task<bool> ForbiddenResponseAsync(HttpContext context, Exception ex, string message = "Authorization Issue")
		{
			context.Response.StatusCode = Status403Forbidden;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Status = Status403Forbidden,
				},
			}).ConfigureAwait(false);
		}

		public async Task<bool> NotFoundResponseAsync(HttpContext context, Exception ex, string message = "Resource Not Found")
		{
			context.Response.StatusCode = Status404NotFound;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Status = Status404NotFound,
				},
			}).ConfigureAwait(false);
		}

		public async Task<bool> InternalServerErrorResponseAsync(HttpContext context, Exception ex, string message = "Internal Error")
		{
			context.Response.StatusCode = Status500InternalServerError;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Status = Status500InternalServerError,
				},
			}).ConfigureAwait(false);
		}

		public async Task<bool> ConflictResponseAsync(HttpContext context, Exception ex, int status = Status409Conflict, string message = "Conflict occured! Try again")
		{
			context.Response.StatusCode = status;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Status = status,
				},
			}).ConfigureAwait(false);
		}

		public async Task<bool> CustomResponseAsync(HttpContext context, Exception ex, int status = Status400BadRequest, string message = "Error processing request")
		{
			context.Response.StatusCode = status;

			return await service.TryWriteAsync(new()
			{
				HttpContext = context,
				Exception = ex,
				ProblemDetails = new()
				{
					Type = ex.GetType().Name,
					Title = message,
					Detail = ex.Message,
					Status = status,
				},
			}).ConfigureAwait(false);
		}
	}
}
