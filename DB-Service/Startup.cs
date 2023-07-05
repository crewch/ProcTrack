using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace DB_Service
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
            services.AddHttpClient<IFileDataClient, HttpFileDataClient>();
            services.AddHttpClient<IAuthDataClient, HttpAuthDataClient>();
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Db-Service", Version = "v1" });
            });
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(
                    connectionString
                )
            );
            services.AddCors(c => c.AddPolicy("cors", opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowCredentials();
                opt.AllowAnyMethod();
                opt.WithOrigins(Configuration.GetSection("Cors:Urls").Get<string[]>()!);
            }));
            
            services.AddScoped<IProcessService, ProcessService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IStageService, StageService>();
            services.AddScoped<ITaskService, TaskService>();

            services.AddDatabaseDeveloperPageExceptionFilter();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProcTrack DB Service"));
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            ApplyMigrations(context);
        }

        public void ApplyMigrations(DataContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
