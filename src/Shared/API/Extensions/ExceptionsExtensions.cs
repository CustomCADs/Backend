namespace CustomCADs.Shared.API.Extensions;

public static class ExceptionsExtensions
{
	extension(Exception ex)
	{
		public bool IsType<TType>() => !ex.GetType().IsGenericType && ex.GetType() == typeof(TType);
		public bool IsGenericType<TType>() => ex.GetType().IsGenericType && ex.GetType().GetGenericTypeDefinition() == typeof(TType);
		public bool IsGenericType(Type type) => ex.GetType().IsGenericType && ex.GetType().GetGenericTypeDefinition() == type;
	}
}
