using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SecurePrivacy.Data;
using SecurePrivacy.Data.IRepository;
using SecurePrivacy.Data.Repository;
using SecurePrivacy.Services;
using SecurePrivacyBackEndTask.Configurations;

namespace SecurePrivacyBackEndTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddScoped<IStudentRepository, StudentRepository>();
            //services.AddScoped<IStudentService, StudentService>();


            services.AddDbContext<SecurePrivacyDbContext>
         (options => options.UseSqlServer(Configuration.GetConnectionString("DatabseContext")));
            services.AddScoped<DbContext, SecurePrivacyDbContext>();

            services.AddSingleton(provider => Configuration);
            var appSettingsSection = Configuration.GetSection("ApplicationSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.ConfigureRepositories()
             .ConfigureSupervisor()
             .AddMiddleware()
             .AddCorsConfiguration()
             .AddConnectionProvider(Configuration)
             .AddAppSettings(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseCors("AllowAll");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseAuthentication();
            app.UseCors("CorsPolicy");




            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
