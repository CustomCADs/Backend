namespace CustomCADs.Presentation;

public record ServerUrlSettings(string All, string Preferred)
{
	public ServerUrlSettings() : this("", "") { }
}
