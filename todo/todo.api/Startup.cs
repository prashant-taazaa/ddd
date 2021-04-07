using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using todo.api.Auth;
using todo.api.Middlewares;
using todo.domain.Models;
using todo.infrastructure.persistence;
using todo.infrastructure.shared.Data;
using todo.infrastructure.shared.Interfaces;

namespace todo.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200");
                                      builder.AllowAnyHeader();
                                      builder.AllowAnyMethod();
                                      builder.AllowCredentials();
                                  });
            });

            services.AddEntityFrameworkNpgsql()
                  .AddDbContext<ApplicationDbContext>(opt =>
                  {
                      opt.UseNpgsql(Configuration.GetConnectionString("DatabaseConnectionString"));
                  });

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<IdentityDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("IdentityConnectionString")));

         
            services.AddScoped(typeof(IDbContext<ApplicationDbContext>), typeof(ApplicationDbContext));
            services.AddScoped(typeof(IDbContext<IdentityDbContext>), typeof(IdentityDbContext));

            services.AddTransient(typeof(ITaskRepository), typeof(TaskRepository));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            //add authentication
            //services.AddAuthentication(options=>
            //          {
            //            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //          })
            //        .AddJwtBearer("Bearer", options =>
            //         {
            //              options.Authority = "https://localhost:5001";
            //              options.TokenValidationParameters = new TokenValidationParameters
            //                                                       {
            //                                                         ValidateAudience = false
            //                                                       };

            //         });

            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();


            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>policy.Requirements.Add(new RoleRequirement("Admin")));
                options.AddPolicy("AppUser", policy => policy.Requirements.Add(new RoleRequirement("AppUser")));

            });


            services.AddHttpContextAccessor();

            services.AddScoped<IAuthorizationHandler, RoleHandler>();

            // Enable Swagger   
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // global error handler
            app.UseMiddleware(typeof(ErrorHandlerMiddleware));

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();//.RequireAuthorization("ApiScope"); ;
            });
        }
    }
}
