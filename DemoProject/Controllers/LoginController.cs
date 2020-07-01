using DemoProject.CommonBiz.Enumeration;
using DemoProject.Model.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    /// <summary>
    ///     登录
    /// </summary>
    [Route("api/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>
        ///     登录
        /// </summary>
        /// <param name="loginname">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public MessageModel<object> Login(string loginname, string password)
        {
            var data = new MessageModel<object>();

            if (!string.IsNullOrEmpty(loginname) && !string.IsNullOrEmpty(password))
            {
                //TODO 校验用户名、密码

                //var id = user.Id;
                //var name = user.Name;
                //var userGroups = user.UserGroups;

                var token = "";
                data.Status = (int)HttpStatusEnum.Success;
                data.Msg = HttpStatusEnum.Success.GetDescription();
                data.Response = token;
                return data;
            }

            data.Status = (int)HttpStatusEnum.UserLoginError;
            data.Msg = HttpStatusEnum.UserLoginError.GetDescription();
            return data;
        }
    }
}