namespace CustomCADs.Modules.Accounts.Application.Roles.Queries.Internal.GetById;

public sealed record GetRoleByIdQuery(
	RoleId Id
) : IQuery<RoleDto>;
