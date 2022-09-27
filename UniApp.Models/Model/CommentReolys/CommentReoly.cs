using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Models.Model.CommentReolys
{
    /// <summary>
    /// 评论回复表
    /// 
    /// </summary>
    [SugarTable("CommentReoly")]
    public class CommentReoly
    {
        /// <summary>
        /// 表ID
        /// </summary>
        [SugarColumn(ColumnName = "", IsIdentity = true, IsPrimaryKey = true)]
        public int CommentId { get; set; }
        /// <summary>
        /// 帖子ID
        /// </summary>
        public int CommunityId { get; set; }
        /// <summary>
        /// 评论父级ID
        /// </summary>
        public int CommentParent { get; set; }
        /// <summary>
        /// 评论人昵称
        /// </summary>
        public string CommentNickName{get;set;}
        /// <summary>
        /// 评论人头像
        /// </summary>
        public string CommentPortrait{get;set;}
        /// <summary>
        /// 评论人地址
        /// </summary>
        public string CommentAddress{ get;set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent{ get; set; }
        /// <summary>
        /// 评论算了
        /// </summary>
        public int ReolyQuantity { get; set; }
        /// <summary>
        /// 被回复人昵称
        /// </summary>
        public string ReolyNickName { get; set; }
        /// <summary>
        /// 感兴趣数量
        /// </summary>
        public int BeInterestedNumber { get; set; }
        /// <summary>
        /// 感兴趣状态
        /// </summary>
        public bool BeInterestedState { get; set; }
        /// <summary>
        /// 不感兴趣数量
        /// </summary>
        public int UninterestedNumber { get; set; }
        /// <summary>
        /// 不感兴趣状态
        /// </summary>
        public bool UninterestedState { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
