using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniApp.Application.Repository.Communitys.Dto;
using UniApp.Models;
using UniApp.Models.Model.Communitys;
using UniApp.Models.Model.Uni;
using UniAppProjectCore;

namespace UniApp.Application.Repository.Communitys
{
    public class CommunityRepository : BaseService<UniCommunity>, ICommunityRepository
    {
        /// <summary>
        /// 添加社区信息
        /// </summary>
        /// <param name="community"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<string>> InsertCommunity(UniCommunity community)
        {
            try
            {
                var ints = 0;
                await Context.Ado.UseTranAsync(async () =>
                {
                    ints = await Context.Insertable(community).ExecuteCommandAsync();
                }, e => throw e);
                if (ints == 0)
                    return new UniResponseResult<string>() { StatusCode = 204, Data = "发布失败", Message = "发布失败" };
                else
                    return new UniResponseResult<string>() { Data = "请求成功", Message = "请求成功", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new UniResponseResult<string>() { Data = ex.Message, Message = ex.Message, StatusCode = 500 };
            }
        }
        /// <summary>
        ///分页查询社区信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<CommunityResultDto>> GetCommunityData(CommunityDataRequest request)
        {
            try
            {
                RefAsync<int> totalNumber = 0;//总数据
                List<CommunityResultDto> communityData = new List<CommunityResultDto>();
                var query = Context.Queryable<UniCommunity, UniUser, UniCommunityOperation,UniCommunityFollow>((com, us, ope,fol) => new JoinQueryInfos(JoinType.Left, com.PublisherID == us.Id, JoinType.Left, com.Cid == ope.CommunityId && ope.UserId == request.UserId,JoinType.Left,com.PublisherID==fol.FollwId&&fol.UserId==request.UserId));
                if (!string.IsNullOrEmpty(request.Content))
                    query = query.Where((com, us, ope,fol) => com.CommunityContent.Contains(request.Content));
                communityData = await query.Select((com, us, ope,fol) => new CommunityResultDto
                {
                    Id = com.Cid,
                    ReleaseId=com.PublisherID,
                    HeadPortrait = us.UserPortrait,
                    Nickname = us.UserNickName,
                    AddressStr = us.UserAddress,
                    Title = com.CommunityContent,
                    PictureStr = com.ContentFigure,
                    FabulousNumber = com.FabulousNumber,
                    FabulousState = ope.FabulousState,
                    CollectionNumber = com.CollectionNumber,
                    CollectionState = ope.CollectionState,
                    FollowState = fol.FollwState,
                    CommentaryNumber = com.CommentaryNumber
                }).ToPageListAsync(request.PageIndex, request.PageSize, totalNumber);
                return new UniResponseResult<CommunityResultDto>() { StatusCode = 200, Data = null, DataList = communityData, Message = "请求成功" };
            }
            catch (Exception ex)
            {
                return new UniResponseResult<CommunityResultDto>() { StatusCode = 500, Message = ex.Message };
            }
        }
        /// <summary>
        /// 添加操作状态
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<string>> InsertOperationsAsync(UniCommunityOperation operation)
        {
            try
            {
                int instr = 0;
                await Context.Ado.UseTranAsync(async () =>
                {
                    instr = await Context.Insertable(operation).ExecuteCommandAsync();
                }, e => throw e);
                if (instr == 0)
                    return new UniResponseResult<string>() { StatusCode = 204, Data = "添加失败", Message = "添加失败" };
                else
                    return new UniResponseResult<string>() { Data = "请求成功", Message = "请求成功", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new UniResponseResult<string>() { Data = ex.Message, Message = ex.Message, StatusCode = 500 };
            }
        }
        /// <summary>
        /// 更新关注点赞收藏状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<string>> OperationsAsync(OperationsRequest request)
        {
            try
            {
                var data = await Context.Queryable<UniCommunityOperation>().Where(c => c.UserId == request.UserId && c.CommunityId == request.CommunityId).FirstAsync();
                var CommunityData = await Context.Queryable<UniCommunity>().Where(c => c.Cid == request.CommunityId).FirstAsync();
                if (data == null)
                {
                    UniCommunityOperation operation = new UniCommunityOperation();
                    operation.UserId = request.UserId;
                    operation.CommunityId = request.CommunityId;
                    switch (request.Type)
                    {
                        case 1:
                            operation.FabulousState = true;
                            operation.CollectionState = false;
                            operation.FollowState = false;
                            CommunityData.FabulousNumber++;
                            break;
                        case 2:
                            operation.FabulousState = false;
                            operation.CollectionState = true;
                            operation.FollowState = false;
                            CommunityData.CollectionNumber++;
                            break;
                    }
                    await Context.Ado.UseTranAsync(async () =>
                    {
                        await Context.Insertable(operation).ExecuteCommandAsync();
                        await Context.Updateable(CommunityData).UpdateColumns(c => new { c.FabulousNumber, c.CollectionNumber }).ExecuteCommandAsync();
                    }, e => throw e);
                }
                else
                {
                    switch (request.Type)
                    {
                        case 1:
                            if (request.State)
                            {
                                data.FabulousState = true;
                                CommunityData.FabulousNumber++;
                            }
                            else
                            {
                                data.FabulousState = false;
                                CommunityData.FabulousNumber--;
                            }
                            break;
                        case 2:
                            if (request.State)
                            {
                                data.CollectionState = true;
                                CommunityData.CollectionNumber++;
                            }
                            else
                            {
                                data.CollectionState = false;
                                CommunityData.CollectionNumber--;
                            }
                            break;
                    }
                    await Context.Ado.UseTranAsync(async () =>
                    {
                        await Context.Updateable(data).UpdateColumns(c => new { c.FabulousState, c.CollectionState, c.FollowState }).ExecuteCommandAsync();
                        await Context.Updateable(CommunityData).UpdateColumns(c => new { c.FabulousNumber, c.CollectionNumber }).ExecuteCommandAsync();
                    }, e => throw e);
                }
                return new UniResponseResult<string>() { Data = "请求成功", Message = "请求成功", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new UniResponseResult<string>() { Data = ex.Message, Message = ex.Message, StatusCode = 500 };
            }
        }
        /// <summary>
        /// 关注操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<string>> FollowOperationAsync(OperationsRequest request)
        {
            try
            {
                var data = await Context.Queryable<UniCommunityFollow>().Where(c => c.UserId == request.UserId && c.FollwId == request.FollowId).FirstAsync();
                if (data==null)
                {
                    UniCommunityFollow follow = new UniCommunityFollow() {UserId=request.UserId,FollwId=request.FollowId,CreationTime=DateTime.Now,FollwState=request.State };
                    await Context.Ado.UseTranAsync(async () =>
                    {
                        await Context.Insertable(follow).ExecuteCommandAsync();
                    }, e => throw e);
                }
                else
                {
                    data.FollwState = request.State;
                    await Context.Ado.UseTranAsync(async () =>
                    {
                        await Context.Updateable(data).ExecuteCommandAsync();
                    }, e => throw e);

                }
                return new UniResponseResult<string>() { Data = "请求成功", Message = "请求成功", StatusCode = 200 };
            }
            catch (Exception ex)
            {
                return new UniResponseResult<string>() { Data = ex.Message, Message = ex.Message, StatusCode = 500 };
            }
            
        }
    }
}
