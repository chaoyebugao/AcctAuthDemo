using AcctAuthDemo.Data.Repositories;
using AcctAuthDemo.Model.Database;
using AcctAuthDemo.Model.Errors;
using AcctAuthDemo.Model.ExecuteResults;
using AcctAuthDemo.Utility.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace AcctAuthDemo.Service.Account
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserService
    {
        private readonly UserRepository userRepository;
        private readonly LoginTokenRepository loginTokenRepository;
        private readonly DeviceRepository deviceRepository;

        public UserService(UserRepository userRepository,
            LoginTokenRepository loginTokenRepository,
            DeviceRepository deviceRepository)
        {
            this.userRepository = userRepository;
            this.loginTokenRepository = loginTokenRepository;
            this.deviceRepository = deviceRepository;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name">登录名</param>
        /// <param name="inputPassword">密码</param>
        /// <param name="deviceId">设备Id</param>
        /// <param name="deviceName">设备名</param>
        public (string endpointToken, DateTime expireAt) Register(string name, string inputPassword, string deviceId, string deviceName,
            string ip = null)
        {
            if (userRepository.NameExists(name))
            {
                throw new ErrorException(ErrorCodes.UserOptFail, "已存在的登录名");
            }

            var userId = userRepository.Create(new User()
            {
                CreateAt = DateTime.Now,
                Name = name,
                Password = HashPassword(name, inputPassword),
                UpdateAt = DateTime.Now,
            });

            return Authorize(name, inputPassword, deviceId, deviceName, ip);
        }

        private string HashPassword(string name, string inputPassword)
        {
            return HashUtil.CreateHmacSha256Hash(name, inputPassword);
        }

        public (string endpointToken, DateTime expireAt) Authorize(
            string name, string inputPassword, string deviceId, string deviceName, string ip = null)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ErrorException(ErrorCodes.ParamsError, "请求数据有误");
            }
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ErrorException(ErrorCodes.ParamsError, "请求数据有误");
            }

            var hashPw = HashPassword(name, inputPassword);

            var user = userRepository.Get(name, hashPw);
            if (user == null)
            {
                throw new ErrorException(ErrorCodes.UserOptFail, "用户名或密码错误");
            }

            var endpointToken = BuildEndpointToken(user.Id, name);

            var dbToken = BuildDatabaseToken(endpointToken, deviceId);

            var loginToken = new LoginToken()
            {
                CreateAt = DateTime.Now,
                DeviceId = deviceId,
                ExpireAt = DateTime.Now.AddDays(30),    //过期时间看需求
                Token = dbToken,
                UpdateAt = DateTime.Now,
                UserId = user.Id,
                IP = ip,
            };

            loginToken.Id = loginTokenRepository.Create(loginToken);

            deviceRepository.CreateByDeviceId(deviceId, deviceName);

            return (endpointToken, loginToken.ExpireAt);
        }

        /// <summary>
        /// 构建终端票据
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        private string BuildEndpointToken(long userId, string name)
        {
            //这里别加进敏感信息（如密码），哈希虽然不可逆而且很难暴力破解，但还是有丁点可能的
            var input = $"{userId}{Guid.NewGuid().ToString("N")}{name}";
            var hashPw = HashUtil.CreateSha512Hash(input);
            return hashPw;
        }

        /// <summary>
        /// 构建数据库票据
        /// </summary>
        /// <param name="endpointToken">终端票据</param>
        /// <param name="deviceId">设备Id</param>
        /// <returns></returns>
        private string BuildDatabaseToken(string endpointToken, string deviceId)
        {
            var input = $"{endpointToken}{deviceId}";
            return HashUtil.CreateSha256Hash(input);
        }

        /// <summary>
        /// 验证票据
        /// </summary>
        /// <param name="endpointToken">终端票据</param>
        /// <param name="deviceId">设备Id</param>
        /// <returns></returns>
        public (ErrorCodes errCode, string errMsg) Valid(string endpointToken, string deviceId)
        {
            if (string.IsNullOrEmpty(endpointToken))
            {
                return (ErrorCodes.InvalidLoginToken, "请重新进行登录");
            }
            if (string.IsNullOrEmpty(deviceId))
            {
                return (ErrorCodes.InvalidLoginToken, "请重新进行登录");
            }

            var dbToken = BuildDatabaseToken(endpointToken, deviceId);
            var userId = loginTokenRepository.FindUser(dbToken, deviceId);
            if (userId == null)
            {
                return (ErrorCodes.InvalidLoginToken, "请重新进行登录");
            }

            //验证通过
            return (ErrorCodes.Success, null);
        }
        
        /// <summary>
        /// 撤销授权
        /// </summary>
        /// <param name="endpointToken">终端票据</param>
        /// <param name="deviceId">设备Id</param>
        public void Unauthorize(string endpointToken, string deviceId)
        {
            var dbToken = BuildDatabaseToken(endpointToken, deviceId);
            loginTokenRepository.Delete(dbToken);
        }
    }
}
