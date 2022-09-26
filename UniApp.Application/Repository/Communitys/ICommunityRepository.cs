using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniApp.Application.Repository.Communitys.Dto;
using UniApp.Models;
using UniApp.Models.Model.Communitys;

namespace UniApp.Application.Repository.Communitys
{
    public interface ICommunityRepository
    {
        /// <summary>
        /// 添加社区发布
        /// </summary>
        /// <param name="community"></param>
        /// <returns></returns>
        Task<UniResponseResult<string>> InsertCommunity(UniCommunity community);
        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        Task<UniResponseResult<string>> InsertOperationsAsync(UniCommunityOperation operation);
        /// <summary>
        /// 分页获取社区信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UniResponseResult<CommunityResultDto>> GetCommunityData(CommunityDataRequest request);
        /// <summary>
        /// 更新关注点赞收藏状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UniResponseResult<string>> OperationsAsync(OperationsRequest request);
        /// <summary>
        /// 关注操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UniResponseResult<string>> FollowOperationAsync(OperationsRequest request);
    }
}
