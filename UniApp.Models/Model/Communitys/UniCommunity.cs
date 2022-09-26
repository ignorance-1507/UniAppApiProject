using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Models.Model.Communitys
{
    /// <summary>
    /// 社区信息表
    /// </summary>
    [SugarTable("UniCommunity")]
    public class UniCommunity
    {
        /// <summary>
        /// id
        /// </summary>
        [SugarColumn(ColumnName = "Cid", IsPrimaryKey = true, IsIdentity = true)]
        public int Cid { get; set; }
        /// <summary>
        /// 发布人ID
        /// </summary>
        public int PublisherID { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string CommunityContent { get; set; }
        /// <summary>
        /// 发布内容配图
        /// </summary>
        public string ContentFigure { get; set; }
        /// <summary>
        /// 发布类型 0 纯文字 1 文字加图片 
        /// </summary>
        public int ContentType { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int FabulousNumber { get; set; } = 0;
        /// <summary>
        /// 收藏数量
        /// </summary>
        public int CollectionNumber { get; set; } = 0;
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentaryNumber { get; set; } = 0;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }= DateTime.Now;
    }
}
