using Autofac;
using Autofac.Extensions.DependencyInjection;
using BlogModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using TestBlog.Controllers;
using TestBlog.Service;

namespace TestBlog
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
            services.AddControllers();
            services.AddDbContext<BlogContext>();
            //services.AddDbContext<BlogContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("BlogContext")));

            services.AddControllersWithViews().AddControllersAsServices();
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            services.Configure<CookiePolicyOptions>(option => {
                option.CheckConsentNeeded = context => false;
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var service = Assembly.Load("TestBlog.Service");
            var repositoty = Assembly.Load("TestBlog.Repositoty");
            var iservice = Assembly.Load("TestBlog.IService");
            var irepositoty = Assembly.Load("TestBlog.IRepositoty");
            var aaa = "IBaseService".EndsWith("Service");
            builder.RegisterAssemblyTypes(service, iservice).Where(r => r.Name.EndsWith("Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(repositoty, irepositoty).Where(r => r.Name.EndsWith("Repositoty")).AsImplementedInterfaces();

            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("any");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "api/[controller]/");
                endpoints.MapControllerRoute("apiDefault", "api/[controller]/");
                endpoints.MapControllers();
            });
        }
    }
}
