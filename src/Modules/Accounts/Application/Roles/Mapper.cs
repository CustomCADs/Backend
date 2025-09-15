namespace CustomCADs.Accounts.Application.Roles;

internal static class Mapper
{
	internal static RoleDto ToDto(this Role role)
		=> new(role.Id, role.Name, role.Description);
}
