using System.Net.Mime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Vk_clone.Errors.Request.Dal.TokenRepository;
using Vk_clone.Errors.Request.Dal.UserRepository;
using Vk_clone.Errors.Request.Services.AuthService;
using Vk_clone.Errors.Request.Services.MailService;
using Vk_clone.Errors.Request.Services.TokenService;
using Vk_clone.Errors.Request.Services.UserService;
using Vk_clone.Errors.Response;
using Vk_clone.Errors.Request.Middleware;

namespace Vk_clone.Errors.Request
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new ValidationFailedResponse(context.ModelState);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Vk_clone", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            });


            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                { };
            });

            services.AddSingleton(sp => new DatabaseConnectionOptions(sp
                .GetService<IConfiguration>()
                ?.GetSection("DatabaseConnectionsOptions")
                .GetValue<string>("ConnectionString"))
            );
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITokenRepository, TokenRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vk_clone v1"));
            }
            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseCors("All");
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseAuthentication();
        }
    }

    public class DatabaseConnectionOptions
    {
        public string ConnectionString { get; }
        
        public DatabaseConnectionOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}