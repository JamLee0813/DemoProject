using DataCenter.Common.Helper;
using DemoProject.AuthHelper;
using DemoProject.Common.Config;
using DemoProject.Filter;
using DemoProject.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            app.UseSwaggerMiddleware();//封装Swagger展示
            app.UseRouting();
            app.UseAuthentication();//先开启认证
            app.UseAuthorization();//然后是授权中间件

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            Log.WriteLine($"[{ConfigFile.AppName}] 启动");
        }

        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerSetup();
            services.AddAuthorizationSetup();

            services.AddControllers(o =>
                {
                    o.Filters.Add(typeof(GlobalExceptionsFilter)); // 全局异常过滤
                })
                //全局配置Json序列化处理
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略循环引用
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); //取消默认驼峰
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local; //匹配时区
                });
        }
    }
}