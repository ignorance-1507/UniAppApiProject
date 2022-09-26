using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Extensions.DBExtensions;

namespace UniApp.Domain.Domain.Help
{
    /// <summary>
    /// 读取appsettings.json
    /// https://blog.csdn.net/qq_43893277/article/details/125958098
    /// </summary>
    public class AppHelper
    {
        private static IConfiguration _config;

        public AppHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        /// 读取指定节点的字符串
        /// </summary>
        /// <param name="sessions"></param>
        /// <returns></returns>
        public static string ReadAppSettings(params string[] sessions)
        {
            try
            {
                if (sessions.Any())
                {
                    return _config[string.Join(":", sessions)];
                }
            }
            catch
            {
                return "";
            }
            return "";
        }

        public static T GetAppSettings<T>(string name) 
        {
           return  DMS.Common.AppConfig.GetValue<T>(name);
        }

    }

}
