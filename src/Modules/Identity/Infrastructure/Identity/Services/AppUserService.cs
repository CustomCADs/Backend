using CustomCADs.Modules.Identity.Application.Contracts;
using CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.Services;

public partial class AppUserService(
	UserManager<AppUser> manager,
	Context.IdentityContext context
) : IUserService;
