using DemoProject.CommonBiz.Enumeration;

namespace DemoProject.Model.Dto
{
    /// <summary>
    ///     通用返回信息类
    /// </summary>
    public class MessageModel<T>
    {
        /// <summary>
        ///     状态码
        /// </summary>
        public int Status { get; set; } = (int)HttpStatusEnum.ServerException;

        /// <summary>
        ///     返回信息
        /// </summary>
        public string Msg { get; set; } = HttpStatusEnum.ServerException.GetDescription();

        /// <summary>
        ///     返回数据集合
        /// </summary>
        public T Response { get; set; }
    }
}