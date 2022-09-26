using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Models.Model.Communitys
{
    /// <summary>
    /// 社区用户关注表
    /// </summary>
    [SugarTable("UniCommunityFollow")]
    public class UniCommunityFollow
    {
        /// <summary>
        /// 表id
        /// </summary>
        [SugarColumn(ColumnName = "Fid", IsIdentity = true, IsPrimaryKey = true)]
        public int Fid { get; set; }
        /// <summary>
        /// 关注的用户ID
        /// </summary>
        public int FollwId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 关注状态
        /// </summary>
        public bool FollwState { get; set; }=false;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }=DateTime.Now;
    }
}
