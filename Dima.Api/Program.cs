using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

app.UseDocumentation();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.UseStaticFiles();
app.MapEndpoints();

app.Run();