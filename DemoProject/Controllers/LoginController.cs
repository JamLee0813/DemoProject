using DemoProject.Common.Helper;
using DemoProject.CommonBiz.Enumeration;
using DemoProject.Model.Dto;
using DemoProject.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly UserServices _services;

        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        public LoginController(UserServices services)
        {
            _services = services;
        }

        /// <summary>
        ///     登录
        /// </summary>
        /// <param name="loginname">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<object>> Login(string loginname, string password)
        {
            if (!string.IsNullOrEmpty(loginname) && !string.IsNullOrEmpty(password))
            {
                //校验用户名、密码
                var pwd = password.ToMd5_32();
                var users = await _services.QueryAsync(it => it.Loginname == loginname && it.Password == pwd && it.Enable);
                if (users.Any())
                {
                    var user = users[0];
                    var tokenModelJwt = new TokenModelJwt()
                    {
                        Uid = user.Id,
                        Role = user.Name
                    };
                    var jwtStr = IssueJwt(tokenModelJwt);

                    return MessageModel<object>.Success(jwtStr);
                }
            }

            return new MessageModel<object>(HttpStatusEnum.PermissionNoAccess);
        }

        /// <summary>
        ///     注册
        /// </summary>
        /// <param name="loginname">登录名</param>
        /// <param name="password">密码</param>
        /// <param name="password2">确认密码</param>
        /// <returns></returns>
        [HttpPost]
        public Task<MessageModel<bool>> Register([FromForm] string loginname, [FromForm] string password, [FromForm] string password2)
        {
            return null;
        }
    }
}