using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectHotel.BLL.Interfaces;
using ProjectHotel.BLL.Services;
using ProjectHotel.DAL.Interfaces;
using ProjectHotel.DAL.Repositories;
using ProjectHotel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHotel
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder().SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<IUnitOfWork, EFUnitOfWork>(ServiceProvider =>{ 
                return new EFUnitOfWork(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IEmployeeService,EmployeeService>();
            services.AddScoped<IEmployeeRoleService,EmployeeRoleService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBookingInfoService, BookingInfoService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICustomerService,CustomerService>();
            services.AddScoped<IRoomService, RoomService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<AuthMiddelware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
