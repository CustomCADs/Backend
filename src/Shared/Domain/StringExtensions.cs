namespace CustomCADs.Shared.Domain;

public static class StringExtensions
{
	extension(string value)
	{
		public string AsCapitalized(bool allUppercase = false, bool dismantlePascal = true)
		{
			if (value.Length == 0) return value;

			System.Text.StringBuilder result = new(value);
			result[0] = char.ToUpper(result[0]);

			if (dismantlePascal) for (int i = 1; i < result.Length; ++i)
			{
				if (char.IsLower(result[i - 1]) && char.IsUpper(result[i]))
				{
					result.Insert(i, ' ');
				}
			}

			if (allUppercase) for (int i = 1; i < result.Length; ++i)
			{
				if (char.IsWhiteSpace(result[i - 1]) && !char.IsWhiteSpace(result[i]))
				{
					result[i] = char.ToUpper(result[i]);
				}
			}

			return result.ToString();
		}
	}
}
