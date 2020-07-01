using DataCenter.Common.Helper;
using DemoProject.CommonBiz.Enumeration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    /// <summary>
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        /// <summary>
        ///     Hello
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        [HttpGet]
        public string Hello(string name)
        {
            var msg = $"Hello {name}!";
            Log.WriteLine(msg);
            return msg;
        }

        /// <summary>
        ///     Hello(Lock)
        /// </summary>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = RoleType.Admin)]
        public string HelloLock(string name)
        {
            var msg = $"Hello {name}!(Lock)";
            Log.WriteLine(msg);
            return msg;
        }
    }
}