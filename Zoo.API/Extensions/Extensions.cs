using Zoo.Data;

namespace Zoo.API;
public static class Extensions
{
    public static void CreateDbIfNotExists(this WebApplication webApplication)
        //(this IHost host)
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ZooContext>();
            context.Database.EnsureCreated();
            
            DbInitializer.Initialize(context);
        }
    }
}