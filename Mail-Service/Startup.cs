using Mail_Service.Configuration;
using MailKit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Mail_Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Mail Service", Version = "v1"});
            });
            services.AddCors(c => c.AddPolicy("cors", opt =>
            {
                opt.AllowAnyHeader();
                opt.AllowCredentials();
                opt.AllowAnyMethod();
                opt.WithOrigins(Configuration.GetSection("Cors:Urls").Get<string[]>()!);
            }));
            services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));
            services.AddTransient<IMailService, MailService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mail-Service"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
