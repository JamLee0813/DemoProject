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

            app.UseSwaggerMiddleware();//��װSwaggerչʾ
            app.UseRouting();
            app.UseAuthentication();//�ȿ�����֤
            app.UseAuthorization();//Ȼ������Ȩ�м��

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            Log.WriteLine($"[{ConfigFile.AppName}] ����");
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
                    o.Filters.Add(typeof(GlobalExceptionsFilter)); // ȫ���쳣����
                })
                //ȫ������Json���л�����
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //����ѭ������
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); //ȡ��Ĭ���շ�
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local; //ƥ��ʱ��
                });
        }
    }
}