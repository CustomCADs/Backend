namespace CustomCADs.Shared.Domain.Bases.Id;

public interface IEntityId<TId> where TId : struct
{
	TId Value { get; init; }
}
