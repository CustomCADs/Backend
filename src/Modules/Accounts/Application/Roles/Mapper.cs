namespace CustomCADs.Modules.Accounts.Application.Roles;

internal static class Mapper
{
	extension(Role role)
	{
		internal RoleDto ToDto()
			=> new(role.Id, role.Name, role.Description);
	}

}
