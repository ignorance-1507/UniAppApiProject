using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniApp.Application.Repository.UniUsers.Dto;
using UniApp.Models;
using UniApp.Models.Model.Uni;

namespace UniApp.Application.Repository.Uni
{
    public interface IUniUserRepository
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        Task<UniResponseResult<UniUserDataDto>> UniUserLoginAsync(UniUserLoginDto loginDto);
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="uniUser"></param>
        /// <returns></returns>
        Task<UniResponseResult<string>> InsertUniUserAsync(UniUser uniUser);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="uniUser"></param>
        /// <returns></returns>
        Task<UniResponseResult<string>> UpdateUserData(UniUserDataDto uniUser);
        /// <summary>
        /// 通过账号获取头像
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        Task<UniResponseResult<UniUserDataDto>> GetUniUserAsync(string UserName);
        /// <summary>
        /// 通过ID获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<UniResponseResult<UniUserDataDto>> GetUniUserAsync(int id);
    }
}
