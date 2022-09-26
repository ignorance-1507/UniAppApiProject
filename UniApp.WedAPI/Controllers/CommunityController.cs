using Microsoft.AspNetCore.Mvc;
using UniApp.Application.Repository.Communitys;
using UniApp.Application.Repository.Communitys.Dto;
using UniApp.Models;
using UniApp.Models.Model.Communitys;

namespace UniApp.WedAPI.Controllers
{
    /// <summary>
    /// 社区控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityRepository _communityRepository;
        public CommunityController(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }
        [HttpPost("InsertCommunity")]
        public async Task<UniResponseResult<string>> InsertCommunity(UniCommunity community)
        {
            return await _communityRepository.InsertCommunity(community);
        }
        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        [HttpPost("InsertOperationsAsync")]
        public async Task<UniResponseResult<string>> InsertOperationsAsync(UniCommunityOperation operation)
        {
            return await _communityRepository.InsertOperationsAsync(operation);
        }
        /// <summary>
        /// 查询社区
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("GetCommunityData")]
        public async Task<UniResponseResult<CommunityResultDto>> GetCommunityData(CommunityDataRequest request)
        {
            return await _communityRepository.GetCommunityData(request);
        }
        /// <summary>
        /// 更新关注点赞收藏状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("OperationsAsync")]
        public async Task<UniResponseResult<string>> OperationsAsync(OperationsRequest request)
        {
            return await _communityRepository.OperationsAsync(request);
        }
        /// <summary>
        /// 关注操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("FollowOperationAsync")]
        public async Task<UniResponseResult<string>> FollowOperationAsync(OperationsRequest request) 
        {
            return await _communityRepository.FollowOperationAsync(request);
        }
    }
}
