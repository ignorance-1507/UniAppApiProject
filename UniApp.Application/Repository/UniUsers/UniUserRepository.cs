using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniApp.Application.Repository.UniUsers.Dto;
using UniApp.Domain;
using UniApp.Domain.Domain.Help;
using UniApp.Models;
using UniApp.Models.Model.Uni;
using UniAppProjectCore;

namespace UniApp.Application.Repository.Uni
{
    public class UniUserRepository : BaseService<UniUser>, IUniUserRepository
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<UniUserDataDto>> UniUserLoginAsync(UniUserLoginDto loginDto)
        {
            try
            {
                //根据用户账号查询用户
                var dataList = await Context.Queryable<UniUser>().Where(x => x.UserName == loginDto.UserName).ToListAsync();
                //判断用户账号是否存在，不存在直接返回
                if (dataList.Count == 0)
                    return new UniResponseResult<UniUserDataDto>() { StatusCode = 204, Message = "此账号不存在在,请重新输入" };
                //判断用户密码是否正确
                var conut = dataList.Where(c => c.UserPassword == loginDto.Password).Count();
                //密码不正确，直接返回
                if (conut == 0)
                    return new UniResponseResult<UniUserDataDto>() { StatusCode = 204, Message = "密码错误,请重新输入" };
                //密码正确返回用户信息
                var data = dataList.Where(c => c.UserPassword == loginDto.Password).Select(c => new UniUserDataDto
                {
                    Id = c.Id,
                    UserNickName = c.UserNickName,
                    PersonalSignature = c.PersonalSignature,
                    BackgroundPicture = c.BackgroundPicture,
                    UserPortrait = c.UserPortrait,
                    UserAddress=c.UserAddress
                }).First();
                return new UniResponseResult<UniUserDataDto>() { StatusCode = 200, Data = data };
            }
            catch (Exception ex)
            {
                //出错信息返回
                return new UniResponseResult<UniUserDataDto>() { StatusCode = 500, Message = ex.Message };
            }
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="uniUser"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<string>> InsertUniUserAsync(UniUser uniUser)
        {
            try
            {
                //注册是否成功参数判断定义
                var inst = 0;
                //通过用户账号查询，用户是否已经注册
                var dataList = await Context.Queryable<UniUser>().Where(x => x.UserName == uniUser.UserName).ToListAsync();
                //已经注册直接返回
                if (dataList.Count > 0)
                    return new UniResponseResult<string>() { StatusCode = 204, Data = "此账号已存在在,请重新输入", Message = "此账号已存在在,请重新输入" };
                //开启sql事务
                await Context.Ado.UseTranAsync(async () =>
                {
                    //用户昵称默认赋值
                    uniUser.UserNickName = "用户_" + RandomVerificationCode.GetRandomCode();
                    //用户个性签名默认赋值
                    uniUser.PersonalSignature = "这个人很懒什么也没有写";
                    //创建时间赋值
                    uniUser.CreationTime = DateTime.Now;
                    //进行用户注册，返回受影响行数
                    var data = await Context.Insertable(uniUser).ExecuteCommandAsync();
                    //受影响行数赋值
                    inst = data;
                }, e => throw e);
                //判断受影响行数是否为0
                if (inst == 0)
                    //用户注册失败返回
                    return new UniResponseResult<string>() { StatusCode = 204, Data = "注册失败", Message = "注册失败" };
                else
                    //用户注册成功返回
                    return new UniResponseResult<string>() { StatusCode = 200, Data = "注册成功,请登录", Message = "注册成功,请登录" };
            }
            catch (Exception ex)
            {
                //出错信息返回
                return new UniResponseResult<string>() { StatusCode = 500, Message = ex.Message };
            }
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="uniUser"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<string>> UpdateUserData(UniUserDataDto uniUser)
        {
            //用户昵称 个性签名 头像都是为单独修改所以出现if判断
            try
            {
                //修改是否成功返回参数定义
                var inst = 0;
                //开启sql事务
                await Context.Ado.UseTranAsync(async () =>
                {
                    //查询修改用户数据对象
                    UniUser user = await Context.Queryable<UniUser>().Where(c => c.Id == uniUser.Id).FirstAsync();
                    //修改用户信息id赋值
                    user.Id = uniUser.Id;
                    //修改用户信息时间赋值
                    user.UpdateTime = DateTime.Now;
                    //用户昵称赋值
                    if (!string.IsNullOrEmpty(uniUser.UserNickName))
                        user.UserNickName = uniUser.UserNickName;
                    //用户个性签名赋值
                    if (!string.IsNullOrEmpty(uniUser.PersonalSignature))
                        user.PersonalSignature = uniUser.PersonalSignature;
                    //用户头像赋值
                    if (!string.IsNullOrEmpty(uniUser.UserPortrait))
                    {
                        if (!string.IsNullOrEmpty(user.UserPortrait)&& user.UserPortrait != uniUser.UserPortrait)
                            UserFileDelete(user.UserPortrait.Split("=")[1]);
                        user.UserPortrait = uniUser.UserPortrait;
                    }
                    //背景图片赋值
                    if (!string.IsNullOrEmpty(uniUser.BackgroundPicture) && user.BackgroundPicture != uniUser.BackgroundPicture)
                    {
                        if (!string.IsNullOrEmpty(user.BackgroundPicture))
                            UserFileDelete(user.BackgroundPicture.Split("=")[1]);
                        user.BackgroundPicture = uniUser.BackgroundPicture;
                    }
                    //返回受影响行数
                    inst = await Context.Updateable(user).UpdateColumns(c => new { c.UserNickName, c.PersonalSignature, c.UserPortrait, c.BackgroundPicture, c.UpdateTime }).Where(c => c.Id == uniUser.Id).ExecuteCommandAsync();
                    #region 多判断处理
                    ////判断昵称是否为空，不为空进行昵称修改
                    //if (!string.IsNullOrEmpty(uniUser.UserNickName))
                    //{
                    //    //用户昵称赋值
                    //    user.UserNickName = uniUser.UserNickName;
                    //    //修改昵称，返回受影响行数
                    //    inst = await Context.Updateable(user).UpdateColumns(c => new { c.UserNickName, c.UpdateTime }).Where(c => c.Id == uniUser.Id).ExecuteCommandAsync();
                    //}
                    ////判断个性签名是否为空，不为空进行个性签名修改
                    //if (!string.IsNullOrEmpty(uniUser.PersonalSignature))
                    //{
                    //    //用户个性签名赋值
                    //    user.PersonalSignature = uniUser.PersonalSignature;
                    //    //进行个性签名修改，返回受影响行数
                    //    inst = await Context.Updateable(user).UpdateColumns(c => new { c.PersonalSignature, c.UpdateTime }).Where(c => c.Id == uniUser.Id).ExecuteCommandAsync();
                    //}
                    ////判断用户头像是否为空，不为空进行用户头像修改
                    //if (!string.IsNullOrEmpty(uniUser.UserPortrait))
                    //{
                    //    //用户头像赋值
                    //    user.UserPortrait = uniUser.UserPortrait;
                    //    //进行用户头像修改，返回受影响行数
                    //    inst = await Context.Updateable(user).UpdateColumns(c => new { c.UserPortrait, c.UpdateTime }).Where(c => c.Id == uniUser.Id).ExecuteCommandAsync();
                    //}
                    ////判断用户自定义背景图片是否为空，不为空进行背景图片赋值
                    //if (!string.IsNullOrEmpty(uniUser.BackgroundPicture))
                    //{
                    //    //背景图片赋值
                    //    user.BackgroundPicture = uniUser.BackgroundPicture;
                    //    //进行背景图片循环，返回受影响行数
                    //    inst = await Context.Updateable(user).UpdateColumns(c => new { c.UserNickName, c.UpdateTime }).Where(c => c.Id == uniUser.Id).ExecuteCommandAsync();
                    //}
                    #endregion

                }, e => throw e);
                //修改成功返回
                return new UniResponseResult<string>() { StatusCode = 200, Data = "修改成功", Message = "修改成功" };
            }
            catch (Exception ex)
            {
                //出错信息返回
                return new UniResponseResult<string>() { StatusCode = 500, Message = ex.Message };
            }
        }

        /// <summary>
        /// 通过账号获取头像
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<UniUserDataDto>> GetUniUserAsync(string UserName)
        {
            try
            {
                //根据用户账号查询用户
                var dataList = await Context.Queryable<UniUser>().Where(x => x.UserName == UserName).ToListAsync();
                //判断用户账号是否存在，不存在直接返回
                if (dataList.Count == 0)
                    return new UniResponseResult<UniUserDataDto>() { StatusCode = 200, Message = "" };
                //用户存在进行返回数据
                var data = dataList.Where(c => c.UserName == UserName).Select(c => new UniUserDataDto
                {
                    Id = c.Id,
                    UserNickName = c.UserNickName,
                    PersonalSignature = c.PersonalSignature,
                    BackgroundPicture = c.BackgroundPicture,
                    UserPortrait = c.UserPortrait,
                    UserAddress = c.UserAddress
                }).First();
                //返回数据对象
                return new UniResponseResult<UniUserDataDto>() { StatusCode = 200, Data = data };
            }
            catch (Exception ex)
            {
                //出错信息返回
                return new UniResponseResult<UniUserDataDto>() { StatusCode = 500, Message = ex.Message };
            }

        }

        /// <summary>
        /// 通过id获取用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public async Task<UniResponseResult<UniUserDataDto>> GetUniUserAsync(int id)
        {
            try
            {
                //根据用户账号查询用户
                var dataList = await Context.Queryable<UniUser>().Where(x => x.Id == id).ToListAsync();
                //判断用户账号是否存在，不存在直接返回
                if (dataList.Count == 0)
                    return new UniResponseResult<UniUserDataDto>() { StatusCode = 204, Message = "" };
                //用户存在进行返回数据
                var data = dataList.Where(c => c.Id == id).Select(c => new UniUserDataDto
                {
                    Id = c.Id,
                    UserNickName = c.UserNickName,
                    PersonalSignature = c.PersonalSignature,
                    BackgroundPicture = c.BackgroundPicture,
                    UserPortrait = c.UserPortrait,
                    UserAddress = c.UserAddress
                }).First();
                //返回数据对象
                return new UniResponseResult<UniUserDataDto>() { StatusCode = 200, Data = data };
            }
            catch (Exception ex)
            {
                //出错信息返回
                return new UniResponseResult<UniUserDataDto>() { StatusCode = 500, Message = ex.Message };
            }

        }

        private void UserFileDelete(string name)
        {
            try
            {
                string Path = AppHelper.GetAppSettings<string>("UploadPath");
                string filePath = Path + name;
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception)
            {
            }

        }
    }
}
