using DataCenter.Common.Helper;
using DemoProject.AuthHelper;
using DemoProject.CommonBiz.Config;
using DemoProject.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace DemoProject
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMvc();
            app.UseSwaggerMiddleware(); //封装Swagger展示
            app.UseAuthentication();// 先开启认证
            app.UseAuthorization();// 然后是授权中间件

            //app.UseEndpoints(endpoints => endpoints.MapControllers());

            Log.WriteLine($"[{ConfigFile.AppName}] 启动");
        }

        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o => o.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddNewtonsoftJson(o =>
                {
                    //o.SerializerSettings.ContractResolver = new DefaultContractResolver(); //取消默认驼峰
                    o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local; //匹配时区
                });

            services.AddSwaggerSetup();
            services.AddAuthorizationSetup();
        }
    }
}