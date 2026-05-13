namespace CustomCADs.Shared.Domain.Bases.Entities;

public interface ISoftDeletable<TEntity> where TEntity : BaseEntity
{
	bool IsDeleted { get; }
	DateTimeOffset? DeletedAt { get; }
	TEntity Delete();
}
