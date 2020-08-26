using DemoProject.CommonBiz.Enumeration;

namespace DemoProject.Model.Dto
{
    /// <summary>
    ///     通用返回信息类
    /// </summary>
    public class MessageModel<T>
    {
        /// <summary>
        /// </summary>
        public MessageModel()
        {
            Status = (int)HttpStatusEnum.ServerException;
            Msg = HttpStatusEnum.ServerException.GetDescription();
        }

        /// <summary>
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        public MessageModel(HttpStatusEnum status, string msg = null)
        {
            Status = (int)status;
            var msgPrefix = status.GetDescription(); //状态码信息
            Msg = string.IsNullOrWhiteSpace(msg) ? msgPrefix : $"{msgPrefix} {msg}";
        }

        /// <summary>
        /// </summary>
        /// <param name="status"></param>
        /// <param name="response"></param>
        /// <param name="msg"></param>
        public MessageModel(HttpStatusEnum status, T response, string msg = null)
        {
            Status = (int)status;
            var msgPrefix = status.GetDescription(); //状态码信息
            Msg = string.IsNullOrWhiteSpace(msg) ? msgPrefix : $"{msgPrefix} {msg}";
            Response = response;
        }

        /// <summary>
        ///     状态码
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     返回信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        ///     返回数据
        /// </summary>
        public T Response { get; set; }

        /// <summary>
        ///     服务异常的返回模型
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static MessageModel<T> Exception(string msg = null) => new MessageModel<T>(HttpStatusEnum.ServerException, msg);

        /// <summary>
        ///     成功的返回模型
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static MessageModel<T> Success(T response) => new MessageModel<T>(HttpStatusEnum.Success, response);
    }
}