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
            app.UseSwaggerMiddleware(); //��װSwaggerչʾ
            app.UseAuthentication();// �ȿ�����֤
            app.UseAuthorization();// Ȼ������Ȩ�м��

            //app.UseEndpoints(endpoints => endpoints.MapControllers());

            Log.WriteLine($"[{ConfigFile.AppName}] ����");
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
                    //o.SerializerSettings.ContractResolver = new DefaultContractResolver(); //ȡ��Ĭ���շ�
                    o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local; //ƥ��ʱ��
                });

            services.AddSwaggerSetup();
            services.AddAuthorizationSetup();
        }
    }
}