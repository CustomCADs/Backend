namespace CustomCADs.Modules.Identity.Application.Users.Dtos;

public record TokenDto(string Value, DateTimeOffset ExpiresAt);
