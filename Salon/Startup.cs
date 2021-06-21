using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.Configuration;
using Salon.Common.JWT;
using Microsoft.Extensions.DependencyInjection;
using Salon.Common.Exceptions;
using Salon.Common.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Salon.DAL.UnitOfWork;
using System.Reflection;
using MediatR;
using AutoMapper;
using Salon.Profiles.User;

namespace Salon
{
    public class Startup
    {
        private string AllowSpecificOrigins = "_AllowSpecificOrigins";
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<JWTSetting>(_configuration.GetSection("JWTSettings"));
            DBConfig.Connection = _configuration.GetConnectionString("DefaultConnection");
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = _configuration["JWTSettings:Issuer"],
                            ValidAudience = _configuration["JWTSettings:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]))
                        };
                    });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Salon API", Version = "V1", Description = "Salon Async API" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert the JWT Token into value (Authorization) field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                new string[] { }
                }
              });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            RegisterExceptionHandler.ExceptionHandler(app);
            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseMiddleware<JWTHandler>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Salon API Endpoint");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "Salon";
                c.DocExpansion(DocExpansion.None);
                c.DefaultModelsExpandDepth(-1);
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}