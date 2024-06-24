using ERP6.Models;
using ERP6.Repositories;
using ERP6.Repositories.Customer;
using ERP6.Repositories.Stock10Repository;
using ERP6.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ERP6
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
            services.AddTransient<Controllers.AjaxsController>();

            services.AddControllersWithViews();

            // 注入 DbContext
            string ConnectString = Configuration.GetConnectionString("ERPContext");
            services.AddDbContext<EEPEF01Context>(options =>
                options.UseSqlServer(ConnectString));

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //20221202WEI_A
            services.AddMvc();
            services.AddSession(options =>
            {
                options.Cookie.Name = "mywebsite";
                options.IdleTimeout = TimeSpan.FromMinutes(60);

            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //20221202WEI_B


            services.AddSingleton(
                HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
                UnicodeRanges.CjkUnifiedIdeographs }));

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 5000; // 5000 items max
                options.ValueLengthLimit = 1024 * 1024 * 100; // 100MB max len form data
            });

            // 註冊泛型 Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // 註冊具體的 Repository
            services.AddScoped<IOut30Rep, Out30Rep>();
            services.AddScoped<IOut40Rep, Out40Rep>();
            services.AddScoped<IStock10Rep, Stock10Rep>();
            services.AddScoped<ICustomerRep, CustomerRep>();

            // 註冊 Service
            services.AddScoped<IOut3040Service, Out3040Service>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            //20221202WEI_A
            app.UseSession();
            //20221202WEI_B

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });

        }
    }
}
