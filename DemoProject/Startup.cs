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
        private const string ApiName = "������Ŀ";

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
                //���ݰ汾���Ƶ��� ����չʾ
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName}, {version}");
                // ��swagger��ҳ�����ó������Զ����ҳ�棬�ǵ�����ַ�����д�������������.index.html
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("SRM.Core.index.html");//���������MiniProfiler�������ܼ�صģ������£���������AOP�Ľӿ����ܷ�����������㲻��Ҫ��������ʱ��ע�͵�����Ӱ���֡�
                c.RoutePrefix =
                    ""; //·�����ã�����Ϊ�գ���ʾֱ���ڸ�������localhost:8001�����ʸ��ļ�,ע��localhost:8001/swagger�Ƿ��ʲ����ģ�ȥlaunchSettings.json��launchUrlȥ��
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
                    o.SerializerSettings.ContractResolver = new DefaultContractResolver(); // ȡ��Ĭ���շ�
                    o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local; //ƥ��ʱ��
                });

            #region Swagger Swagger UI Service

            var basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                //������ȫ���İ汾�����ĵ���Ϣչʾ
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    // {ApiName} �����ȫ�ֱ����������޸�
                    Version = "V1",
                    Title = $"{ApiName} �ӿ��ĵ�",
                    Description = $"{ApiName} HTTP API V1",
                    Contact = new OpenApiContact { Name = "Jam" }
                });
                // �����·������
                c.OrderActionsBy(o => o.RelativePath);

                #region ��ȡxml��Ϣ

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(basePath, xmlFile); //������Ǹո����õ�xml�ļ���
                c.IncludeXmlComments(xmlPath, true); //Ĭ�ϵĵڶ���������false�������controller��ע�ͣ��ǵ��޸�

                #endregion ��ȡxml��Ϣ

                #region Token�󶨵�ConfigureServices

                //������ȨС��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                //������oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���) ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�\"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion Token�󶨵�ConfigureServices
            });

            #endregion Swagger Swagger UI Service
        }
    }
}