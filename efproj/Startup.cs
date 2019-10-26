using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace efproj
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //服务注入配置
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        //程序配置
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //判断当前环境是 开发环境 还是 生产环境
            if (env.IsDevelopment())
            {
                //开发环境。启用错误显示
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境。错误时转到对应错误页
                app.UseExceptionHandler("/Error");
                //允许浏览器信任SSL证书 并 缓存，默认时间为30天
                app.UseHsts();
            }

            //设置SSL端口的前提下，强制使用HTTPS
            app.UseHttpsRedirection();

            //设置默认页
            app.UseDefaultFiles(SetDefaultPage("index.html"));

            //启用默认文件夹(wwwroot)内的静态文件访问权限
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private DefaultFilesOptions SetDefaultPage(string PageName)
        {
            DefaultFilesOptions defaultFilesOptions=new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add(PageName);
            return defaultFilesOptions;
        }
    }
}
