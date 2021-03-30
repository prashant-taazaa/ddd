using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnings.identity.server;
using IdentityServer4.EntityFramework.Mappers;

namespace learnings.identity.server.Data
{
    public class DatabaseInitializer
    {
        public static void Initialize(IApplicationBuilder app, AuthDbContext context)
        {
            context.Database.EnsureCreated();

            InitializeTokenServerConfigurationDatabase(app);

            context.SaveChanges();
        }

        private static void InitializeTokenServerConfigurationDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()
                   .CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
                    .Database.Migrate();

                var context = scope.ServiceProvider
                    .GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.ApiResources)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
