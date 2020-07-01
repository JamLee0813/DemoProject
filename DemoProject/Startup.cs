using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace DemoProject
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        private const string ApiName = "样例项目";

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

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                const string version = "V1";
                //根据版本名称倒序 遍历展示
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName}, {version}");
                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("SRM.Core.index.html");//这里是配合MiniProfiler进行性能监控的，《文章：完美基于AOP的接口性能分析》，如果你不需要，可以暂时先注释掉，不影响大局。
                c.RoutePrefix =
                    ""; //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉
            });

            #endregion Swagger
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
                    o.SerializerSettings.ContractResolver = new DefaultContractResolver(); // 取消默认驼峰
                    o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local; //匹配时区
                });

            #region Swagger Swagger UI Service

            var basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} 定义成全局变量，方便修改
                    Version = "V1",
                    Title = $"{ApiName} 接口文档",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = "Jam" }
                });
                // 按相对路径排序
                c.OrderActionsBy(o => o.RelativePath);

                #region 读取xml信息

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(basePath, xmlFile); //这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true); //默认的第二个参数是false，这个是controller的注释，记得修改

                #endregion 读取xml信息

                #region Token绑定到ConfigureServices

                //开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                //必须是oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion Token绑定到ConfigureServices
            });

            #endregion Swagger Swagger UI Service
        }
    }
}