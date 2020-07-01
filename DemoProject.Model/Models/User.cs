using SqlSugar;

namespace DemoProject.Model.Models
{
    /// <summary>
    ///     用户表
    /// </summary>
    public class User
    {
        private int _Id;

        private string _Loginname;

        private string _Password;

        private string _Name;

        private System.DateTime? _Createtime;

        private System.DateTime? _Updatetime;

        /// <summary>
        ///     自增主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get => _Id; set => _Id = value; }

        /// <summary>
        ///     登录名
        /// </summary>
        public string Loginname { get => _Loginname; set => _Loginname = value?.Trim(); }

        /// <summary>
        ///     密码
        /// </summary>
        public string Password { get => _Password; set => _Password = value?.Trim(); }

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get => _Name; set => _Name = value?.Trim(); }

        /// <summary>
        ///     创建时间
        /// </summary>
        [SugarColumn(IsOnlyIgnoreInsert = true, IsOnlyIgnoreUpdate = true)]
        public System.DateTime? Createtime { get => _Createtime; set => _Createtime = value; }

        /// <summary>
        ///     更新时间
        /// </summary>
        [SugarColumn(IsOnlyIgnoreInsert = true)]
        public System.DateTime? Updatetime { get => _Updatetime; set => _Updatetime = value; }
    }
}