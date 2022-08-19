using Microsoft.Extensions.Hosting;
using DAL.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
