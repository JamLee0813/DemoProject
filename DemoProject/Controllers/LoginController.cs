using DemoProject.CommonBiz.Const;
using DemoProject.CommonBiz.Enumeration;
using DemoProject.Model.Dto;
using Microsoft.AspNetCore.Authorization;
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
        /// <param name="loginname">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<object> Login(string loginname, string password)
        {
            if (!string.IsNullOrEmpty(loginname) && !string.IsNullOrEmpty(password))
            {
                //TODO 校验用户名、密码
                if (loginname.Equals("admin") && password.Equals("admin"))
                {
                    var tokenModelJwt = new TokenModelJwt()
                    {
                        Uid = 1,
                        Role = "超级超级超级管理"
                    };
                    var jwtStr = IssueJwt(tokenModelJwt);

                    return MessageModelHelper.Ok((object)jwtStr);
                }
            }

            return new MessageModel<object>(HttpStatusEnum.PermissionNoAccess);
        }
    }
}