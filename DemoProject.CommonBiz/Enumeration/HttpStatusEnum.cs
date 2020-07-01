using System;
using System.ComponentModel;

namespace DemoProject.CommonBiz.Enumeration
{
    /// <summary>
    ///     HTTP状态码
    /// </summary>
    public enum HttpStatusEnum
    {
        /// <summary>
        ///     成功
        /// </summary>
        [Description("成功")] Success = 200,

        //* 用户错误：20001-29999*/

        /// <summary>
        ///     用户未登录
        /// </summary>
        [Description("用户未登录")] UserNotLogin = 2001001,

        /// <summary>
        ///     账号不存在或密码错误
        /// </summary>
        [Description("账号不存在或密码错误")] UserLoginError = 2001002,

        /// <summary>
        ///     账号已被禁用
        /// </summary>
        [Description("账号已被禁用")] UserAccountForbidden = 2001003,

        /// <summary>
        ///     用户不存在
        /// </summary>
        [Description("用户不存在")] UserNotExist = 2001004,

        /// <summary>
        ///     用户已存在
        /// </summary>
        [Description("用户已存在")] UserHasExisted = 2001005,

        //* 业务错误：30001-39999 */

        /// <summary>
        ///     某业务出现问题
        /// </summary>
        [Description("某业务出现问题")] SpecifiedQuestionedUserNotExist = 3001001,

        /// <summary>
        ///     参数校验不通过
        /// </summary>
        [Description("参数校验不通过")] ParamIsInvalid = 4001001,

        /// <summary>
        ///     参数为空
        /// </summary>
        [Description("参数为空")] ParamIsBlank = 4001002,

        /// <summary>
        ///     参数类型错误
        /// </summary>
        [Description("参数类型错误")] ParamTypeBindError = 4001003,

        /// <summary>
        ///     参数缺失
        /// </summary>
        [Description("参数缺失")] ParamNotComplete = 4001004,

        //* 数据错误：50001-599999 */

        /// <summary>
        ///     系统繁忙，请稍后重试
        /// </summary>
        [Description("系统繁忙，请稍后重试")] SystemInnerError = 5001001,

        /// <summary>
        ///     数据未找到
        /// </summary>
        [Description("数据未找到")] ResultDataNone = 5001002,

        /// <summary>
        ///     数据有误
        /// </summary>
        [Description("数据有误")] DataIsWrong = 5001003,

        /// <summary>
        ///     数据已存在
        /// </summary>
        [Description("数据已存在")] DataAlreadyExisted = 5001004,

        /// <summary>
        ///     上传文件类型不支持或文件为空
        /// </summary>
        [Description("上传文件类型不支持或文件为空")] UploadFileError = 5001005,

        //* 接口错误：60001-69999 */

        /// <summary>
        ///     内部系统接口调用异常
        /// </summary>
        [Description("内部系统接口调用异常")] InterfaceInnerInvokeError = 6001001,

        /// <summary>
        ///     外部系统接口调用异常
        /// </summary>
        [Description("外部系统接口调用异常")] InterfaceOuterInvokeError = 6001002,

        /// <summary>
        ///     该接口禁止访问
        /// </summary>
        [Description("该接口禁止访问")] InterfaceForbidVisit = 6001003,

        /// <summary>
        ///     接口地址无效
        /// </summary>
        [Description("接口地址无效")] InterfaceAddressInvalid = 6001004,

        /// <summary>
        ///     接口请求超时
        /// </summary>
        [Description("接口请求超时")] InterfaceRequestTimeout = 6001005,

        /// <summary>
        ///     接口负载过高
        /// </summary>
        [Description("接口负载过高")] InterfaceExceedLoad = 6001006,

        /// <summary>
        ///     接口熔断
        /// </summary>
        [Description("接口熔断")] InterfaceHystrix = 6001007,

        /// <summary>
        ///     服务器异常
        /// </summary>
        [Description("服务器异常")] ServerException = 6001008,

        //* 权限错误：70001-79999 */

        /// <summary>
        ///     无访问权限
        /// </summary>
        [Description("无访问权限")] PermissionNoAccess = 7001001
    }

    /// <summary>
    ///     Http返回状态码帮助类
    /// </summary>
    public static class HttpStatusHelper
    {
        /// <summary>
        ///     返回Http状态码的描述文本
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDescription(this HttpStatusEnum obj)
        {
            var type = obj.GetType();
            var field = type.GetField(Enum.GetName(type, obj));

            return !(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute
                descAttr)
                ? string.Empty
                : descAttr.Description;
        }
    }
}