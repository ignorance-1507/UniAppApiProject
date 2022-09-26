using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniApp.Models
{
    /// <summary>
    /// 返回数据模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UniResponseResult<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; } = "请求成功";
        /// <summary>
        /// 数据单体
        /// </summary>
        public T? Data { get; set; }
        /// <summary>
        /// 数据多个
        /// </summary>
        public List<T>? DataList { get; set; }
    }
}
