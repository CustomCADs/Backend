using CustomCADs.Modules.Accounts.Domain.Repositories.Reads;

namespace CustomCADs.Modules.Accounts.Application.Roles.Queries.Internal.GetAll;

public sealed class GetAllRolesHandler(IRoleReads reads, BaseCachingService<RoleId, Role> cache)
	: IQueryHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
{
	public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery req, CancellationToken ct)
	{
		ICollection<Role> roles = await cache.GetOrCreateAsync(
			factory: async () => [.. await reads.AllAsync(track: false, ct: ct).ConfigureAwait(false)]
		).ConfigureAwait(false);

		return roles.Select(x => x.ToDto());
	}
}
