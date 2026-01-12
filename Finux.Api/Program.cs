using Finux.Api;
using Finux.Api.Common.Api;
using Finux.Api.Endpoints;
using Finux.Core;

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

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.UseDocumentation();
app.MapEndpoints();
app.UseDefaultFiles()
    .UseStaticFiles(new StaticFileOptions
    {
        ServeUnknownFileTypes = true
    });
app.MapFallbackToFile("index.html");

app.Run();