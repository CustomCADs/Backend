using CustomCADs.Shared.Domain.Bases.Entities;
using System.Linq.Expressions;

namespace CustomCADs.Shared.Domain.Exceptions;

public static class ExceptionExtensions
{
	private static readonly ArgumentException propertyNotComputableException = new(
		message: "Expression Body must be MemberExpression"
	);

	public static TEntity ThrowIfPredicateIsTrue<TEntity, TProperty>(
		this TEntity entity,
		Expression<Func<TEntity, TProperty>> expression,
		Predicate<TProperty> predicate,
		string message,
		string? property = null
	) where TEntity : BaseEntity
	{
		TProperty? value = expression.Compile()(entity);
		property ??= expression.Body is MemberExpression memberExpression
			? memberExpression.Member.Name
			: throw propertyNotComputableException;

		if (predicate(value))
		{
			throw CustomValidationException<TEntity>.Custom(
				string.Format(message, typeof(TEntity).Name, property)
			);
		}
		return entity;
	}

	public static TEntity ThrowIfPredicateIsFalse<TEntity, TProperty>(
		this TEntity entity,
		Expression<Func<TEntity, TProperty>> expression,
		Predicate<TProperty> predicate,
		string message,
		string? property = null
	) where TEntity : BaseEntity
		=> entity.ThrowIfPredicateIsTrue(
			expression: expression,
			predicate: (entity) => !predicate(entity),
			message: message,
			property: property
		);

	public static TEntity ThrowIfNull<TEntity, TProperty>(
		this TEntity entity,
		Expression<Func<TEntity, TProperty>> expression,
		Predicate<TProperty> predicate,
		string? property = null
	) where TEntity : BaseEntity
		=> entity.ThrowIfPredicateIsTrue(
			expression: expression,
			predicate: predicate,
			message: "{0} requires property: {1} to not be null.",
			property: property
		);

	public static TEntity ThrowIfInvalidLength<TEntity>(
		this TEntity entity,
		Expression<Func<TEntity, string>> expression,
		(int Min, int Max) length,
		bool inclusive = false,
		string? property = null
	) where TEntity : BaseEntity
	{
		string value = expression.Compile()(entity);
		property ??= expression.Body is MemberExpression memberExpression
			? memberExpression.Member.Name
			: throw propertyNotComputableException;

		if (ComputeCondition(value.Length, length, inclusive))
		{
			throw CustomValidationException<TEntity>.Custom($"A/An {typeof(TEntity).Name}'s {property} length must be more than {length.Min} and less than {length.Max}.");
		}
		return entity;
	}

	public static TEntity ThrowIfInvalidRange<TEntity, TProperty>(
		this TEntity entity,
		Expression<Func<TEntity, TProperty>> expression,
		(TProperty Min, TProperty Max) range,
		bool inclusive = false,
		string? property = null
	) where TEntity : BaseEntity where TProperty : struct, IComparable<TProperty>
	{
		TProperty value = expression.Compile()(entity);
		property ??= expression.Body is MemberExpression memberExpression
			? memberExpression.Member.Name
			: throw propertyNotComputableException;

		if (ComputeCondition(value, range, inclusive))
		{
			throw CustomValidationException<TEntity>.Custom($"A/An {typeof(TEntity).Name}'s {property} must be more than {range.Min} and less than {range.Max}.");
		}
		return entity;
	}

	public static TEntity ThrowIfInvalidSize<TEntity, TProperty>(
		this TEntity entity,
		Expression<Func<TEntity, TProperty[]>> expression,
		(int Min, int Max) size,
		bool inclusive = false,
		string? property = null
	) where TEntity : BaseEntity
	{
		TProperty[] value = expression.Compile()(entity);
		property ??= expression.Body is MemberExpression memberExpression
			? memberExpression.Member.Name
			: throw propertyNotComputableException;

		if (ComputeCondition(value.Length, size, inclusive))
		{
			throw CustomValidationException<TEntity>.Custom($"A/An {typeof(TEntity).Name}'s {property} length must be more than {size.Min} and less than {size.Max}.");
		}
		return entity;
	}

	private static bool ComputeCondition<TProperty>(TProperty value, (TProperty Min, TProperty Max) range, bool inclusive) where TProperty : struct, IComparable<TProperty> => inclusive
		? value.CompareTo(range.Max) >= 0 || value.CompareTo(range.Min) <= 0
		: value.CompareTo(range.Max) > 0 || value.CompareTo(range.Min) < 0;
}
