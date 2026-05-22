using Finux.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Finux.Api.Common.Api
{
    public static class AppExtension
    {
        public static void UseSecurity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        public static void UseDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger();
        }
    }
}