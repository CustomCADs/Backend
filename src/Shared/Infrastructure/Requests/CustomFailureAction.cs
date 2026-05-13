using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using FluentValidation.Results;
using Wolverine.FluentValidation;

namespace CustomCADs.Shared.Infrastructure.Requests;

public class CustomFailureAction<T> : IFailureAction<T>
{
	public void Throw(T message, IReadOnlyList<ValidationFailure> failures)
		=> throw failures.ValidationException;
}
