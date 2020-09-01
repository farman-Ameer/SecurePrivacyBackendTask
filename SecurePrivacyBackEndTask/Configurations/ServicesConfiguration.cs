using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SecurePrivacy.Data.IRepository;
using SecurePrivacy.Data.Repository;
using SecurePrivacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurePrivacyBackEndTask.Configurations
{
  
        public static class ServicesConfiguration
        {
            public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
            {
                services.AddScoped<IStudentRepository, StudentRepository>();
              
                services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                services.AddScoped<IUrlHelper>(factory =>
                {
                    var actionContext = factory.GetService<IActionContextAccessor>()
                                               .ActionContext;
                    return new UrlHelper(actionContext);
                });

                return services;
            }

            public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddScoped<IStudentService, StudentService>();
                return services;
            }
            public static IServiceCollection AddMiddleware(this IServiceCollection services)
            {
                services.AddMvc().AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
                return services;
            }

            public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
            {
                var corsBuilder = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder();
                corsBuilder.AllowAnyHeader();
                corsBuilder.AllowAnyMethod();
                corsBuilder.AllowAnyOrigin();
                corsBuilder.AllowCredentials();
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll", corsBuilder.Build());
                });

                return services;
            }
        }
    
}
