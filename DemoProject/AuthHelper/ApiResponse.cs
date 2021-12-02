using DemoProject.CommonBiz.Enumeration;
using DemoProject.Model.Dto;

namespace DemoProject.AuthHelper
{
    /// <summary>
    /// </summary>
    public enum StatusCode
    {
        /// <summary>
        /// </summary>
        CODE401,

        /// <summary>
        /// </summary>
        CODE403,

        /// <summary>
        /// </summary>
        CODE404,

        /// <summary>
        /// </summary>
        CODE500
    }

    /// <summary>
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// </summary>
        public MessageModel<string> MessageModel = new MessageModel<string>();

        /// <summary>
        /// </summary>
        /// <param name="apiCode"></param>
        /// <param name="msg"></param>
        public ApiResponse(StatusCode apiCode, string msg = null)
        {
            switch (apiCode)
            {
                case StatusCode.CODE401:
                    {
                        Status = 401;
                        Value = "很抱歉，您无权访问该接口，请确保已经登录!";
                    }
                    break;

                case StatusCode.CODE403:
                    {
                        Status = 403;
                        Value = "很抱歉，您的访问权限等级不够，联系管理员!";
                    }
                    break;

                case StatusCode.CODE500:
                    {
                        Status = 500;
                        Value = msg;
                    }
                    break;
            }

            MessageModel = new MessageModel<string>
            {
                Status = HttpStatusEnum.PermissionNoAccess,
                Msg = Value
            };
        }

        /// <summary>
        /// </summary>
        public int Status { get; set; } = 404;

        /// <summary>
        /// </summary>
        public string Value { get; set; } = "No Found";
    }
}