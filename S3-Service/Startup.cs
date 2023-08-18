using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Minio;

namespace S3_Service
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
            services.Configure<MinioOptions>(Configuration.GetSection(nameof(MinioOptions)));
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<MinioOptions>>().CurrentValue;
                
                return new MinioClient(options.Endpoint, options.AccessKey, options.SecretKey);
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "S3 Service", Version = "v1"});
            });
            services.AddCors(
                 //c => c.AddPolicy("cors", opt =>
                 //    {
                 //        opt.AllowAnyHeader();
                 //        opt.AllowCredentials();
                 //        opt.AllowAnyMethod();
                 //        opt.WithOrigins(Configuration.GetSection("Cors:Urls").Get<string[]>()!);
                 //    })
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "S3-service"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            //app.UseCors("cors");

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
