using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace Dima.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? string.Empty;
            Configuration.BackendUrl = Environment.GetEnvironmentVariable("BACKEND_URL")  ?? builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL")  ?? builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
            Configuration.StockApiUrl = builder.Configuration.GetValue<string>("StockApiUrl") ?? Environment.GetEnvironmentVariable("StockApiUrl") ?? string.Empty;
            ApiConfiguration.StripeApiKey = builder.Configuration.GetValue<string>("StripeApiKey") ?? Environment.GetEnvironmentVariable("StripeApiKey") ?? string.Empty;
            
            StripeConfiguration.ApiKey = ApiConfiguration.StripeApiKey;

        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });
        }

        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddIdentityCookies();
            builder.Services.AddAuthorization();
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseMySql(Configuration.ConnectionString, ServerVersion.AutoDetect(Configuration.ConnectionString));
                }
            );
            builder.Services
                .AddIdentityCore<User>()
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
                options => 
                    options
                        .AddPolicy(
                            ApiConfiguration.CorsPolicyName, 
                            policy => 
                                policy
                                    .WithOrigins([
                                        Configuration.FrontendUrl,
                                        Configuration.BackendUrl,
                                        Configuration.StockApiUrl,
                                        "https://dima-api-production.up.railway.app",
                                        "http://localhost:5000"
                                    ])
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials()
                                    )
            );
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
            builder.Services.AddTransient<IStockHandler, StockHandler>();
            builder.Services.AddTransient<IOrderHandler, OrderHandler>();
            builder.Services.AddTransient<IProductHandler, ProductHandler>();
            builder.Services.AddTransient<IVoucherHandler, VoucherHandler>();
            builder.Services.AddTransient<IStripeHandler, StripeHandler>();
            builder.Services.AddTransient<IReportHandler, ReportHandler>();
        }
    }
    
}