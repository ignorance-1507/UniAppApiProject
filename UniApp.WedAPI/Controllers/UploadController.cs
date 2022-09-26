using Microsoft.AspNetCore.Mvc;
using UniApp.Domain.Domain.Help;

namespace UniApp.WedAPI.Controllers
{
    /// <summary>
    /// 上传控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UploadController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 文件图片上传方法
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostFile")]
        public async Task<string> PostFileAsync([FromForm] IFormFile file)
        {
            //获取当前运行项目的根目录
            string webRootPath = _hostingEnvironment.ContentRootPath;
            //定义返回参数
            String result = "Fail";
            //文件流初始化
            StreamReader reader = new StreamReader(file.OpenReadStream());
            //读取文件流
            String content = reader.ReadToEnd();
            //获取上传文件的后缀名
            var fname = file.FileName.Substring(file.FileName.LastIndexOf("."), file.FileName.Length-file.FileName.LastIndexOf("."));
            //重新命名文件名
            String name = Guid.NewGuid()+ fname;
            //定义文件夹路径
            string Path = AppHelper.GetAppSettings<string>("UploadPath");
            //判断文件夹路径是否存在，不存在就创建
            if (!System.IO.Directory.Exists(Path)) 
            {
                //创建文件夹路径
                System.IO.Directory.CreateDirectory(Path);
            }
            //拼接文件路径
            String filename = Path + name;
            //判断文件路径是否存在，存在进行删除
            if (System.IO.File.Exists(filename))
            {
                //删除文件
                System.IO.File.Delete(filename);

            }
            //根据文件路径创建文件
            using (FileStream fs = System.IO.File.Create(filename))

            {

                // 复制文件

                file.CopyTo(fs);

                // 清空缓冲区数据

                fs.Flush();

            }
            //文件名称赋值
            result = name;

            //返回文件名称
            return result;
        }
        /// <summary>
        /// 图片文件展示接口
        /// </summary>
        /// <param name="urlName"></param>
        /// <returns></returns>
        [HttpGet("GetImage")]

        [Produces("image/jpeg")]
        public Stream GetImage(string urlName)
        {

            // 因为应用程序目录和内容目录相同

            // 所以直接获取Current即可

            //string dirpath = Directory.GetCurrentDirectory();

            // 直接返回文件流
            //获取程序根目录
            string webRootPath = _hostingEnvironment.ContentRootPath;
            //设置文件存在路径
            string FliePath = webRootPath.Replace("\\", "/") + "/UserImg/";
            //转换文件路径符号
            string file = FliePath.Replace("/",@"\");
            //根据文件路径返回文件图片进行展示
            return System.IO.File.OpenRead(Path.Combine(@"E:\UniAppUserImg\UserImg", urlName));

        }
    }
}
