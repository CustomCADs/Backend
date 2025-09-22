namespace CustomCADs.Accounts.API.Roles;

internal static class Mapper
{
	internal static RoleResponse ToResponse(this RoleDto role)
		=> new(role.Name, role.Description);
}
