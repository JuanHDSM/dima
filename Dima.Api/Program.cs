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

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.UseDocumentation();
app.MapEndpoints();

app.Run();