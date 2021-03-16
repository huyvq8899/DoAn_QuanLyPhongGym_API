using API.Extentions;
using API.Hubs;
using API.Providers;
using AutoMapper;
using DLL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Repositories.Implimentations;
using Services.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using wework.Auguard;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Datacontext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                 o => o.MigrationsAssembly("DLL").UseRowNumberForPaging()));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Thuế",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "BKSoft",
                        Email = string.Empty,
                    },
                });

                c.AddSecurityDefinition("Bearer", //Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", //The name of the previously defined security scheme.
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
                });
            });
            services.AddAutoMapper();
            //services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
            services.AddTransient<IAuthorizationHandler, BaseAuthorizationHandler>();

            services.AddScoped<IRoleRespositories, RoleRespositories>();
            services.AddScoped<IUserRespositories, UserRespositories>();
            services.AddScoped<IFunctionRespositories, FunctionRespositories>();
            services.AddScoped<ICustomerRespositories, CustomerRespositories>();
            services.AddScoped<ICardTypeRespositories, CardTypeRespositories>();
            services.AddScoped<IEquipmentRespositories, EquipmentRespositories>();
            services.AddScoped<IFacilityRespositories, FacilityRespositories>();
            // bỏ dấu #
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
            //
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          var accessToken = context.Request.Query["access_token"];

                          // If the request is for our hub...
                          var path = context.HttpContext.Request.Path;
                          if (!string.IsNullOrEmpty(accessToken) &&
                              (path.StartsWithSegments("/signalr")))
                          {
                              // Read the token out of the query string
                              context.Token = accessToken;
                          }
                          return Task.CompletedTask;
                      }
                  };

                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                          .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });
            // chat with angular
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("https://localhost", "http://localhost", "https://*.pmbk.vn", "http://localhost:4200", "http://localhost:8100")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            // chat with angular
            services.AddSignalR();
        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });

                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseDefaultFiles(); // them khi co controller fallback
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseAuthentication();// con gấu
            app.UseCookiePolicy();

            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalRHub>("/signalr");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                //// To learn more about options for serving an Angular SPA from ASP.NET Core,
                //// see https://go.microsoft.com/fwlink/?linkid=864501

                //spa.Options.SourcePath = "ClientApp";

                //if (env.IsDevelopment())
                //{
                //    spa.UseAngularCliServer(npmScript: "start");
                //}
            });
        }
    }
}
