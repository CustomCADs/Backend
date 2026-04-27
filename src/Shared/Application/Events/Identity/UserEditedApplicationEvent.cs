namespace CustomCADs.Shared.Application.Events.Identity;

public record UserEditedApplicationEvent(
	AccountId Id,
	NamesDto? Names = null,
	string? Username = null,
	bool? TrackViewedProducts = null
) : BaseApplicationEvent;

public record NamesDto(string? FirstName = null, string? LastName = null);
