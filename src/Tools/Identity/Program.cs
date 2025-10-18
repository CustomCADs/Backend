var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity(builder.Configuration);

if (args.Contains("--migrate"))
{
	await builder.Services.ExecuteDbMigrationUpdaterAsync().ConfigureAwait(false);
}
else if (args.Contains("--migrate-only"))
{
	await builder.Services.ExecuteDbMigrationUpdaterAsync().ConfigureAwait(false);
	return;
}

var app = builder.Build();

await app.RunAsync().ConfigureAwait(false);
