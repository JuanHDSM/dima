using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;
using Dima.Core;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.Services.AddHttpClient(Configuration.StocksHttpClientName, options =>
    options.BaseAddress = new Uri("https://brapi.dev")
);
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

app.UseStaticFiles();
app.UseDocumentation();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.MapEndpoints();

app.MapFallbackToFile("index.html");

app.Run();