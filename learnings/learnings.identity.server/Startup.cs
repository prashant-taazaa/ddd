// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServerHost.Quickstart.UI;
using learnings.identity.server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace learnings.identity.server
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IWebHostEnvironment environment,IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200");
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                  });
            });

            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            var dbConnectionString = Configuration.GetConnectionString("IdentityServerConnection");
            services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(dbConnectionString));


            services.AddIdentityServer()
                   .AddDeveloperSigningCredential()
                   .AddConfigurationStore(option =>
                          option.ConfigureDbContext = builder => builder.UseNpgsql(dbConnectionString, options =>
                           options.MigrationsAssembly("learnings.identity.server")))
                   .AddOperationalStore(option =>
                          option.ConfigureDbContext = builder => builder.UseNpgsql(dbConnectionString, options =>
                           options.MigrationsAssembly("learnings.identity.server")));

        }

        public void Configure(IApplicationBuilder app, AuthDbContext context)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(MyAllowSpecificOrigins);
            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

           // DatabaseInitializer.Initialize(app, context);

            app.UseIdentityServer();

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
