using CustomCADs.Shared.Domain.Bases.Entities;
using System.Linq.Expressions;

namespace CustomCADs.Shared.Domain.Exceptions;

public static class ExceptionExtensions
{
	private static readonly ArgumentException propertyNotComputableException = new(
		message: "Expression Body must be MemberExpression"
	);

	extension<TEntity>(TEntity entity) where TEntity : BaseEntity
	{
		public TEntity ThrowIfPredicateIsTrue<TProperty>(
			Expression<Func<TEntity, TProperty>> expression,
			Predicate<TProperty> predicate,
			string message,
			string? property = null
		)
		{
			TProperty? value = expression.Compile().Invoke(entity);
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

		public TEntity ThrowIfPredicateIsFalse<TProperty>(
			Expression<Func<TEntity, TProperty>> expression,
			Predicate<TProperty> predicate,
			string message,
			string? property = null
		) => entity.ThrowIfPredicateIsTrue(
				expression: expression,
				predicate: (entity) => !predicate(entity),
				message: message,
				property: property
			);

		public TEntity ThrowIfNull<TProperty>(
			Expression<Func<TEntity, TProperty>> expression,
			Predicate<TProperty> predicate,
			string? property = null
		) => entity.ThrowIfPredicateIsTrue(
				expression: expression,
				predicate: predicate,
				message: "{0} requires property: {1} to not be null.",
				property: property
			);

		public TEntity ThrowIfInvalidLength(
			Expression<Func<TEntity, string?>> expression,
			(int Min, int Max) length,
			bool inclusive = false,
			string? property = null
		)
		{
			string? value = expression.Compile().Invoke(entity);
			property ??= expression.Body is MemberExpression memberExpression
				? memberExpression.Member.Name
				: throw propertyNotComputableException;

			if (value?.Length.IsOutOfRange(length, inclusive) ?? false)
			{
				throw CustomValidationException<TEntity>.Custom($"A/An {typeof(TEntity).Name}'s {property} length must be more than {length.Min} and less than {length.Max}.");
			}
			return entity;
		}

		public TEntity ThrowIfInvalidRange<TProperty>(
			Expression<Func<TEntity, TProperty>> expression,
			(TProperty Min, TProperty Max) range,
			bool inclusive = false,
			string? property = null
		) where TProperty : struct, IComparable<TProperty>
		{
			TProperty value = expression.Compile().Invoke(entity);
			property ??= expression.Body is MemberExpression memberExpression
				? memberExpression.Member.Name
				: throw propertyNotComputableException;

			if (value.IsOutOfRange(range, inclusive))
			{
				throw CustomValidationException<TEntity>.Custom($"A/An {typeof(TEntity).Name}'s {property} must be more than {range.Min} and less than {range.Max}.");
			}
			return entity;
		}

		public TEntity ThrowIfInvalidSize<TProperty>(
			Expression<Func<TEntity, TProperty[]>> expression,
			(int Min, int Max) size,
			bool inclusive = false,
			string? property = null
		)
		{
			TProperty[] value = expression.Compile().Invoke(entity);
			property ??= expression.Body is MemberExpression memberExpression
				? memberExpression.Member.Name
				: throw propertyNotComputableException;

			if (value.Length.IsOutOfRange(size, inclusive))
			{
				throw CustomValidationException<TEntity>.Custom($"A/An {typeof(TEntity).Name}'s {property} length must be more than {size.Min} and less than {size.Max}.");
			}
			return entity;
		}
	}

	extension<TValue>(TValue value) where TValue : struct, IComparable<TValue>
	{
		private bool IsOutOfRange((TValue Min, TValue Max) range, bool inclusive)
			=> inclusive
				? value.CompareTo(range.Max) >= 0 || value.CompareTo(range.Min) <= 0
				: value.CompareTo(range.Max) > 0 || value.CompareTo(range.Min) < 0;
	}
}
