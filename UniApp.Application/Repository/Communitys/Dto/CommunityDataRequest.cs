using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Application.Repository.Communitys.Dto
{
    public class CommunityDataRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 社区搜索内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 多少条
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
