namespace Dima.Api.Common.Api
{
    public static class AppExtension
    {
        public static void UseSecurity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static void UseDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapSwagger().RequireAuthorization();
        }
    }
}