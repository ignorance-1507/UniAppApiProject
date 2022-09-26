using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Models.Model.Communitys
{
    /// <summary>
    /// 社区评论表
    /// </summary>
    [SugarTable("UniCommunityComment")]
    public class UniCommunityComment
    {
        /// <summary>
        /// 评论表ID
        /// </summary>
        [SugarColumn(ColumnName = "Pid", IsPrimaryKey = true, IsIdentity = true)]
        public int Pid { get; set; }
        /// <summary>
        /// 评论帖子ID
        /// </summary>
        public int CommunityId { get; set; }
        /// <summary>
        /// 评论人ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 评论人昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 评论获得点赞数量
        /// </summary>
        public int CommentLikeNumber { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
