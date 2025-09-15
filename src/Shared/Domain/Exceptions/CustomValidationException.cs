using CustomCADs.Shared.Domain.Bases.Exceptions;

namespace CustomCADs.Shared.Domain.Exceptions;

public class CustomValidationException<TEntity> : BaseException
{
	private CustomValidationException(string message, Exception? inner) : base(message, inner) { }

	public static CustomValidationException<TEntity> Status<TStatus>(
		TStatus status,
		Exception? inner = default
	) where TStatus : Enum
		=> new($"Cannot edit this data on an {typeof(TEntity).Name} with status: {status}.", inner);

	public static CustomValidationException<TEntity> Status<TStatus>(
		TStatus newStatus,
		TStatus oldStatus,
		Exception? inner = default
	) where TStatus : Enum
		=> new($"Cannot set status: {newStatus} to {typeof(TEntity).Name} of status: {oldStatus}.", inner);

	public static CustomValidationException<TEntity> Custom(string message, Exception? inner = default)
		=> new(message, inner);
}
