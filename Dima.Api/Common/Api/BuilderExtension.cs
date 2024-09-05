using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Common.Api
{
    public static class BuilderExtension
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? string.Empty;
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

        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
            builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

        }
    }
    
}