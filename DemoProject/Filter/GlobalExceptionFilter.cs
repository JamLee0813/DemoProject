using DataCenter.Common.Helper;
using DemoProject.Model.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace DemoProject.Filter
{
    /// <summary>
    ///     全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter : IAsyncExceptionFilter
    {
        private readonly IHostEnvironment _env;

        /// <summary>
        /// </summary>
        /// <param name="env"></param>
        public GlobalExceptionsFilter(IHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        ///     全局异常捕获
        /// </summary>
        /// <param name="context"></param>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var ex = context.Exception;
            var msg = ex.Message; //错误信息
            var stackTrace = ex.StackTrace; //堆栈信息

            var json = new MessageModel<string> { Msg = msg };
            if (_env.IsDevelopment()) json.Response = stackTrace;
            context.Result = new JsonResult(json);

            var logMsg = WriteLog(context.Exception);
            Log.WriteErrorLine(msg + logMsg); //采用log4net 进行错误日志记录

            return Task.CompletedTask;
        }

        /// <summary>
        ///     自定义返回格式
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(Exception ex) => $"【异常类型】：{ex.GetType().Name} \r\n【异常信息】：{ex.Message} \r\n【堆栈调用】：{ex.StackTrace}";
    }
}