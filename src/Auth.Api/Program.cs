using Auth.Api.Extensions;
using Auth.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProgramDependencies(builder.Configuration);

var app = builder.Build();

await app.UseConfiguring();

app.Run();