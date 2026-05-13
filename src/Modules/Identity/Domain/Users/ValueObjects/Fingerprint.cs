namespace CustomCADs.Modules.Identity.Domain.Users.ValueObjects;

public record Fingerprint(string Device = "", string? Location = null);
