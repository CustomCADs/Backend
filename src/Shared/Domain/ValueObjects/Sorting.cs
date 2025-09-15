using CustomCADs.Shared.Domain.Enums;

namespace CustomCADs.Shared.Domain.ValueObjects;

public record Sorting<TSorting>(
	TSorting Type,
	SortingDirection Direction
) where TSorting : Enum;
