using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Files.Application.Images.Dtos;

public record ImageDto(
	ImageId Id,
	string Key,
	string ContentType,
	AccountId OwnerId,
	string OwnerName
);
