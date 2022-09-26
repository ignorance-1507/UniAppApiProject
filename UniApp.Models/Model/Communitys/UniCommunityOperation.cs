using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Models.Model.Communitys
{
    /// <summary>
    /// 社区点赞关注表
    /// </summary>
    [SugarTable("UniCommunityOperation")]
    public class UniCommunityOperation
    {
        /// <summary>
        /// 表ID
        /// </summary>
        [SugarColumn(ColumnName = "Oid", IsPrimaryKey = true, IsIdentity = true)]
        public int Oid { get; set; }
        /// <summary>
        /// 点赞关注社区帖子ID
        /// </summary>
        public int CommunityId { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 点赞
        /// </summary>
        public bool FabulousState { get; set; } = false;
        /// <summary>
        /// 收藏
        /// </summary>
        public bool CollectionState { get; set; } = false;
        /// <summary>
        /// 关注
        /// </summary>
        public bool FollowState { get; set; } = false;
    }
}
