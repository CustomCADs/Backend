namespace CustomCADs.Accounts.Endpoints.Roles;

internal static class Mapper
{
	internal static RoleResponse ToResponse(this RoleDto role)
		=> new(role.Name, role.Description);
}
