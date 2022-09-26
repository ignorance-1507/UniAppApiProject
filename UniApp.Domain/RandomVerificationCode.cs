using SkiaSharp;
using System.Drawing;

namespace UniApp.Domain
{
    public static class RandomVerificationCode
    {
        /// <summary>
        /// 4位随机数验证码纯数字的
        /// </summary>
        /// <returns></returns>
        public static string GetRandomCode()
        {
            Random rad = new Random();
            int mobile_code = rad.Next(1000, 10000);   //生成随机数
            return mobile_code.ToString();
        }
        /// <summary>
        /// 随机生成字母数字验证码
        /// </summary>
        /// <param name="lengths">要生成的个数</param>
        /// <returns></returns>
        public static string GetRandomVerificationCode(int lengths)
        {
            string[] chars = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string code = "";
            Random random = new Random();
            for (int i = 0; i < lengths; i++)
            {
                code += chars[random.Next(chars.Length)];
            }
            return code;
        }

        public static string CreatePng(string code)
        {
            Random random = new();
            //验证码颜色集合  
            var colors = new[] { SKColors.Black, SKColors.Red, SKColors.DarkBlue, SKColors.Green, SKColors.Orange, SKColors.Brown, SKColors.DarkCyan, SKColors.Purple };
            //验证码字体集合
            var fonts = new[] { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            //相当于js的 canvas.getContext('2d')
            using var image2d = new SKBitmap(100, 30, SKColorType.Bgra8888, SKAlphaType.Premul);
            //相当于前端的canvas
            using var canvas = new SKCanvas(image2d);
            //填充白色背景
            canvas.DrawColor(SKColors.AntiqueWhite);
            //样式 跟xaml差不多
            using var drawStyle = new SKPaint();
            //填充验证码到图片
            for (int i = 0; i < code.Length; i++)
            {
                drawStyle.IsAntialias = true;
                drawStyle.TextSize = 30;
                var font = SKTypeface.FromFamilyName(fonts[random.Next(0, fonts.Length - 1)], SKFontStyleWeight.SemiBold, SKFontStyleWidth.ExtraCondensed, SKFontStyleSlant.Upright);
                drawStyle.Typeface = font;
                drawStyle.Color = colors[random.Next(0, colors.Length - 1)];
                //写字
                canvas.DrawText(code[i].ToString(), (i + 1) * 16, 28, drawStyle);
            }
            //生成三条干扰线
            for (int i = 0; i < 3; i++)
            {
                drawStyle.Color = colors[random.Next(colors.Length)];
                drawStyle.StrokeWidth = 1;
                canvas.DrawLine(random.Next(0, code.Length * 15), random.Next(0, 60), random.Next(0, code.Length * 16), random.Next(0, 30), drawStyle);
            }
            //巴拉巴拉的就行了
            using var img = SKImage.FromBitmap(image2d);
            using var p = img.Encode(SKEncodedImageFormat.Png, 100);
            using var ms = new MemoryStream();
            //保存到流
            p.SaveTo(ms);
            return Convert.ToBase64String(ms.ToArray());
            //return ms;
        }

    }
}