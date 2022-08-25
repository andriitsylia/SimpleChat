using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DAL.Configurations
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var chatContext = scope.ServiceProvider.GetRequiredService<ChatContext>())
                {
                    try
                    {
                        chatContext.Database.Migrate();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return host;
        }
    }
}
