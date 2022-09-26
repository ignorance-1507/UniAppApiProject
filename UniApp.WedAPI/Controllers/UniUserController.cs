using DMS.Common.Model.Result;
using Microsoft.AspNetCore.Mvc;
using UniApp.Application.Repository.Uni;
using UniApp.Application.Repository.UniUsers.Dto;
using UniApp.Domain.Domain.Utils;
using UniApp.Models;
using UniApp.Models.Model;
using UniApp.Models.Model.Uni;

namespace UniApp.WedAPI.Controllers
{
    /// <summary>
    /// �û����ݲ���������
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UniUserController : ControllerBase
    {
        private readonly IUniUserRepository _uniUserRepository; 
        public UniUserController(IUniUserRepository uniUserRepository)
        {
           _uniUserRepository = uniUserRepository;
        }
        /// <summary>
        /// �û���¼
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("UniUserLoginAsync")]
        public async Task<UniResponseResult<UniUserDataDto>> UniUserLoginAsync(UniUserLoginDto loginDto) 
        {
            return await _uniUserRepository.UniUserLoginAsync(loginDto);
        }
        /// <summary>
        /// �û�ע��
        /// </summary>
        /// <param name="uniUser"></param>
        /// <returns></returns>
        [HttpPost("InsertUniUserAsync")]
        public async Task<UniResponseResult<string>> InsertUniUserAsync(UniUser uniUser)
        {
            return await _uniUserRepository.InsertUniUserAsync(uniUser);
        }
        /// <summary>
        /// �޸��û���Ϣ
        /// </summary>
        /// <param name="uniUser"></param>
        /// <returns></returns>
        [HttpPost("UpdateUserData")]
        public async Task<UniResponseResult<string>> UpdateUserData(UniUserDataDto uniUser) 
        {
            return await _uniUserRepository.UpdateUserData(uniUser);
        }
        /// <summary>
        /// ��ȡ�û�ͷ��
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet("GetUniUserAsync")]
        public async Task<UniResponseResult<UniUserDataDto>> GetUniUserAsync(string UserName) 
        {
            return await _uniUserRepository.GetUniUserAsync(UserName);
        }

        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet("GetUniUserDataAsync")]
        public async Task<UniResponseResult<UniUserDataDto>> GetUniUserAsync(int Id)
        {
            return await _uniUserRepository.GetUniUserAsync(Id);
        }
    }
}