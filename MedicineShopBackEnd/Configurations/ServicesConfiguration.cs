using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Data.Repository;
using MedicineShopBackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MedicineShopBackEnd.Configurations
{
  
        public static class ServicesConfiguration
        {
            public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
            {
            

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //services.AddScoped<IUrlHelper>(factory =>
            //{
            //    var actionContext = factory.GetService<IActionContextAccessor>()
            //                               .ActionContext;
            //    return new UrlHelper(actionContext);
            //});

            return services;
            }

            public static IServiceCollection ConfigureSupervisor(this IServiceCollection services)
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddScoped<IStudentService, StudentService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<ICategoryService, CategoryService>();
                services.AddScoped<IProductService, ProductService>();
                services.AddScoped<IOrderService, OrderService>();
                services.AddScoped<INotificationService, NotificationService>();
            //services.AddHttpContextAccessor();

            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddScoped<IUrlHelper>(factory =>
            //{
            //    var actionContext = factory.GetService<IActionContextAccessor>()
            //                               .ActionContext;
            //    return new UrlHelper(actionContext);
            //});


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
