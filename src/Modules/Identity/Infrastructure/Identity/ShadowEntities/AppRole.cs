using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Modules.Identity.Infrastructure.Identity.ShadowEntities;

public class AppRole(string name) : IdentityRole<Guid>(name);
