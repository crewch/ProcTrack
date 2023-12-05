using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Hubs;
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
            services.AddHttpClient<IMailDataClient, HttpMailDataClient>();

            services.AddSignalR();

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

            services.AddScoped<Services.Comment.CRUD.ICommentService, Services.Comment.CRUD.CommentService>();
            services.AddScoped<Services.Dependence.CRUD.IDependenceService, Services.Dependence.CRUD.DependenceService>();
            services.AddScoped<Services.Edge.CRUD.IEdgeService, Services.Edge.CRUD.EdgeService>();
            services.AddScoped<Services.Log.CRUD.ILogService, Services.Log.CRUD.LogService>();
            services.AddScoped<Services.Passport.CRUD.IPassportService, Services.Passport.CRUD.PassportService>();
            services.AddScoped<Services.Priority.CRUD.IPriorityService, Services.Priority.CRUD.PriorityService>();
            services.AddScoped<Services.Program.CRUD.IProgramService, Services.Program.CRUD.ProgramService>();
            services.AddScoped<Services.Status.CRUD.IStatusService, Services.Status.CRUD.StatusService>();
            services.AddScoped<Services.Type.CRUD.ITypeService, Services.Type.CRUD.TypeService>();

            services.AddScoped<Services.Notification.CRUD.INotificationService, Services.Notification.CRUD.NotificationService>();
            
            services.AddScoped<Services.Process.CRUD.IProcessService, Services.Process.CRUD.ProcessService>();
            
            services.AddScoped<Services.Stage.CRUD.IStageService, Services.Stage.CRUD.StageService>();

            services.AddScoped<Services.Task.CRUD.ITaskService, Services.Task.CRUD.TaskService>();

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
            
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notifications");
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
