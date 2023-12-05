using AuthService.Data;
using AuthService.Dtos;
using AuthService.Services.Group;
using AuthService.Services.Hold;
using AuthService.Services.Role;
using AuthService.Services.Status;
using AuthService.Services.Type;
using AuthService.Services.User;
//using AuthService.Services.Right;
//using AuthService.Services.Tool;
//using AuthService.Services.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace AuthService
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
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth-Service", Version = "v1" });
            });
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<AuthContext>(options =>
                options.UseNpgsql(
                    connectionString
                ),
                ServiceLifetime.Transient
            );
            services.AddCors(
            //     c => c.AddPolicy("cors", opt =>
            // {
            //     opt.AllowAnyHeader();
            //     opt.AllowCredentials();
            //     opt.AllowAnyMethod();
            //     opt.WithOrigins(Configuration.GetSection("Cors:Urls").Get<string[]>()!);
            // })
            );
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            services.AddMvc();

            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHoldService, HoldService>();
            services.AddScoped<IRoleService, RoleService>();



            //services.AddScoped<ILoginService, LoginService>();
            //services.AddScoped<IHoldService, HoldService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<ITestDataService, TestDataService>();

            //IConfiguration configuration = new ConfigurationBuilder()
            //    .AddEnvironmentVariables()
            //    .Build();

            //services.AddSingleton(configuration);
            services.AddSingleton(Configuration);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcTrack Auth Service"));
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AuthContext>();
            ApplyMigrations(context);
        }

        public void ApplyMigrations(AuthContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}