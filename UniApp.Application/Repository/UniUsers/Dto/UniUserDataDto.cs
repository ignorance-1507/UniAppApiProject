using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Application.Repository.UniUsers.Dto
{
    /// <summary>
    /// 用户信息Dto
    /// </summary>
    public class UniUserDataDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string? UserNickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? UserPortrait { get; set; }
        /// <summary>
        /// 个性签名
        /// </summary>
        public string? PersonalSignature { get; set; }
        /// <summary>
        /// 背景图片
        /// </summary>
        public string? BackgroundPicture { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string? UserAddress { get; set; }
    }
}
