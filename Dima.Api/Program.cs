using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();
=======
var cnnStr = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddDbContext<AppDbContext>(
    options =>
        {
            options.UseMySql(cnnStr, ServerVersion.AutoDetect(cnnStr));
        }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => 
{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
>>>>>>> 23a7befb12364a254c686cb31ffc27fadb1c48ce

var app = builder.Build();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.UseSecurity();
app.UseDocumentation();
app.MapEndpoints();

app.Run();