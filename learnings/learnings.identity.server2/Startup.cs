// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using learnings.identity.server2.Data;
using learnings.identity.server2.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IdentityServer4.EntityFramework;
using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace learnings.identity.server2
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var dbConnectionString = Configuration.GetConnectionString("IdentityServerConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddControllersWithViews();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(dbConnectionString));



            services.AddIdentityServer()
                 //.AddDeveloperSigningCredential()
                 .AddConfigurationStore(option =>
                        option.ConfigureDbContext = builder => builder.UseNpgsql(dbConnectionString, options =>
                         options.MigrationsAssembly(migrationsAssembly)))
                 .AddOperationalStore(option =>
                        option.ConfigureDbContext = builder => builder.UseNpgsql(dbConnectionString, options =>
                         options.MigrationsAssembly(migrationsAssembly)));

            //var builder = services.AddIdentityServer()
            //                      .AddInMemoryIdentityResources(Config.IdentityResources)
            //                      .AddInMemoryApiScopes(Config.ApiScopes)
            //                      .AddInMemoryClients(Config.Clients);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // this will do the initial DB population
           // InitializeDatabase(app);

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }


        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
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

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}