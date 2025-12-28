namespace CustomCADs.Modules.Accounts.API.Roles;

internal static class Mapper
{
	extension(RoleDto role)
	{
		internal RoleResponse ToResponse()
			=> new(role.Name, role.Description);
	}

}
