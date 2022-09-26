using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Application.Repository.Communitys.Dto
{
    /// <summary>
    /// 返回社区列表数据模型
    /// </summary>
    public class CommunityResultDto
    {
        /// <summary>
        /// 社区ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 发布人头像Url
        /// </summary>
        public string? HeadPortrait { get; set; }
        /// <summary>
        /// 发布人ID
        /// </summary>
        public int ReleaseId { get; set; }
        /// <summary>
        /// 发布人昵称
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// 发布人地址
        /// </summary>
        public string? AddressStr { get; set; }
        /// <summary>
        /// 发布人地址
        /// </summary>
        public string? Address { get { 
            if (AddressStr == null||AddressStr=="") return "未知";
            return AddressStr;
            } set { Address = value; } }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 发布内容配图
        /// </summary>
        public List<string>? Picture
        {
            get {
                if (!string.IsNullOrEmpty(PictureStr))
                {
                    var list = PictureStr.Split(',');
                  return  list.ToList();
                }else
                    return null;
            }
            set { Picture = value; }
        }
        public string? PictureStr { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int FabulousNumber { get; set; }
        /// <summary>
        /// 当前登录人是否点赞状态
        /// </summary>
        public bool FabulousState { get; set; }
        /// <summary>
        /// 收藏数量
        /// </summary>
        public int CollectionNumber { get; set; }
        /// <summary>
        /// 当前登录人是否收藏状态
        /// </summary>
        public bool CollectionState { get; set; }
        /// <summary>
        /// 当前登录人是否关注状态
        /// </summary>
        public bool FollowState { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentaryNumber { get; set; }
    }
}
