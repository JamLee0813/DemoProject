using SqlSugar;
using System;

namespace DemoProject.Model.Models.Base
{
    /// <summary>
    ///     表模型基类
    /// </summary>
    public class RootEntity
    {
        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [SugarColumn(IsOnlyIgnoreInsert = true, IsOnlyIgnoreUpdate = true)]
        public DateTime Createtime { get; set; }

        /// <summary>
        ///     更新时间
        /// </summary>
        [SugarColumn(IsOnlyIgnoreInsert = true)]
        public DateTime Updatetime { get; set; }
    }
}