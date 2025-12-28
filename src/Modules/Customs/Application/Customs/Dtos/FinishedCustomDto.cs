using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.Modules.Customs.Application.Customs.Dtos;

public record FinishedCustomDto(
	decimal Price,
	DateTimeOffset FinishedAt,
	CadId CadId
);
