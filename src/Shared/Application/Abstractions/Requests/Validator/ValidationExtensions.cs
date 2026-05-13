namespace CustomCADs.Shared.Application.Abstractions.Requests.Validator;

using Shared.Domain;

public static class ValidationExtensions
{
	extension(IEnumerable<FluentValidation.Results.ValidationFailure> failures)
	{
		public FluentValidation.ValidationException ValidationException => new(
			message: string.Join(
				separator: ".\n",
				values: failures
					.Select(f =>
					{
						f.PropertyName = f.PropertyName.AsCapitalized();
						f.ErrorMessage = f.ErrorMessage.AsCapitalized();
						return f;
					})
					.GroupBy(f => f.PropertyName)
					.ToDictionary(
						f => f.Key,
						f => string.Join(
							separator: "; ",
							values: f.Select((x) => x.ErrorMessage).Distinct()
						).Trim()
					)
					.Select(e => string.IsNullOrWhiteSpace(e.Key) ? e.Value : $"{e.Key}: {e.Value}")
			) + '.'
		);
	}
}
