using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Models.Model.Uni
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [SugarTable("UniUser")]
    public class UniUser
    {
        [SugarColumn(ColumnName = "Id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string? UserPortrait { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string? UserNickName { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int? UserSex { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string? UserAddress { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string? PersonalSignature { get; set; }
        /// <summary>
        /// 背景图片
        /// </summary>
        public string? BackgroundPicture { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreationTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
