using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Real_State_Catalog_WCF.Data;
using Real_State_Catalog_WCF.Models;

namespace Real_State_Catalog_WCF
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ��� ����� ����������� ����������� ���������. ��������������� ��� �����, ��� ������ ������ �� ����������.
        public void ConfigureServices(IServiceCollection services)
        {
            // ���������� ���������� ��� ���� ����� �� ����� appsettings.json
            services.AddDbContext<AppContextDb>(options => options.UseSqlServer(Configuration.GetConnectionString("AppContextDB")));

            services.Configure<SecurityStampValidatorOptions>(o =>
                o.ValidationInterval = TimeSpan.FromSeconds(0));

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen();
        }

        // ��� ����� ����������� ����������� ���������. ��������������� ��� ����� ��� ������������ ������� ������ HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
