using DemoProject.CommonBiz.Enumeration;
using DemoProject.Model.Dto;
using Microsoft.AspNetCore.Mvc;
using static DemoProject.AuthHelper.JwtHelper;

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
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<object> Login(string loginName, string password)
        {
            if (!string.IsNullOrEmpty(loginName) && !string.IsNullOrEmpty(password))
            {
                //TODO 校验用户名、密码
                if (loginName.Equals("admin") && password.Equals("demo@admin"))
                {
                    var tokenModelJwt = new TokenModelJwt()
                    {
                        Uid = 1,
                        Role = "超级超级超级管理"
                    };
                    var jwtStr = IssueJwt(tokenModelJwt);

                    return MessageModel<object>.Success(jwtStr);
                }
            }

            return new MessageModel<object>(HttpStatusEnum.PermissionNoAccess);
        }
    }
}