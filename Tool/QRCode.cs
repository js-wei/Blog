using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.IO;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;


namespace Tool
{
    public class QRCode
    {
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Create(string str, int _size, string destImg = "", bool _combin = false, QRCodeLevel _levet = QRCodeLevel.M, QRCodeVersion _ver = QRCodeVersion.A, QRCodeType _type = QRCodeType.Byte)
        {
            var _result = string.Empty;
            var fullpath = string.Empty;
            var filepath = System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") + "\\";
            if (string.IsNullOrEmpty(destImg))
            {
                destImg = filepath + "favicon.png";
            }
            if (string.IsNullOrEmpty(str))
            {
                _result = "[{\"status\":0,\"count\":1,\"content\":\"" + "亲内容去哪里了".ChinessConvertToUnicodeString() + "\"}]";
                return _result;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = _type.ToString();
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }

            try
            {
                int scale = _size;
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
                _result = "[{\"status\":0,\"count\":1,\"content\":\"" + "大小设置出错".ChinessConvertToUnicodeString() + "\"}]";
                return _result;
            }
            try
            {
                int version = Convert.ToInt16(_ver);
                qrCodeEncoder.QRCodeVersion = version + 1;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.ToString());
                _result = "[{\"status\":0,\"count\":1,\"content\":\"" + "版本设置出错".ChinessConvertToUnicodeString() + "\"}]";
                return _result;
            }
            string errorCorrect = _levet.ToString();
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;



            Image image;

            image = qrCodeEncoder.Encode(ConverToGB(str, 16));

            var filename = Guid.NewGuid().ToString() + ".png";

            fullpath = filepath + filename;

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            fullpath = filepath + filename;
            if (_combin)
            {
                CombinImage(image, destImg, true);
            }
            image.Dispose();
            _result = "[{\"status\":1,\"count\":1,\"content\":\"" + str + "\",\"list\":[{\"imgurl\":\"" + fullpath + "\"}]}]";

            return _result;
        }

        /// <summary>
        /// 识别二维码
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string Recognition(string _path)
        {
            var _result = string.Empty;
            var fullpath = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int j = 0; j < System.Web.HttpContext.Current.Request.Files.Count; j++)
                {
                    var filename = Guid.NewGuid().ToString() + "_tmp.png";
                    _path = (_path.IndexOf("~") > -1) ?
                        System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") + "\\" : _path;
                    var filepath = _path;
                    if (!Directory.Exists(filename))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    fullpath = filepath + filename.Replace("_tmp", "");
                    string qrdecode = string.Empty;
                    System.Web.HttpPostedFile uploadFile = System.Web.HttpContext.Current.Request.Files[j];
                    uploadFile.SaveAs(fullpath);

                    QRCodeDecoder decoder = new QRCodeDecoder();
                    Bitmap bm = new Bitmap(fullpath);
                    qrdecode = decoder.decode(new QRCodeBitmapImage(bm));
                    bm.Dispose();

                    _result = "[{\"count\":1,\"list\":[{\"imgurl\":\"" + fullpath + "\",\"qrtext\":\"" + qrdecode + "\"}]}]";
                }
            }
            else
            {
                _result = "[{\"count\":0,\"list\":[{\"error\":\"" + "没有选择二维码文件".ChinessConvertToUnicodeString() + "\"}]}]";
            }
            //删除缓存图片
            File.Delete(fullpath);
            return _result;
        }
        /// <summary>
        /// 识别二维码
        /// </summary>
        /// <returns></returns>
        public static string Recognition()
        {
            var _result = string.Empty;
            var fullpath = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.Count > 0)
            {
                for (int j = 0; j < System.Web.HttpContext.Current.Request.Files.Count; j++)
                {
                    var filename = Guid.NewGuid().ToString() + "_tmp.png";
                    var filepath = System.Web.HttpContext.Current.Server.MapPath(@"~\Utilty\QRCode\upload") + "\\";
                    if (!Directory.Exists(filename))
                    {
                        Directory.CreateDirectory(filepath);
                    }
                    fullpath = filepath + filename;
                    if (string.IsNullOrEmpty(fullpath))
                        return "请上传二维码文件".ChinessConvertToUnicodeString();
                    string qrdecode = string.Empty;
                    System.Web.HttpPostedFile uploadFile = System.Web.HttpContext.Current.Request.Files[j];
                    uploadFile.SaveAs(fullpath);

                    QRCodeDecoder decoder = new QRCodeDecoder();
                    Bitmap bm = new Bitmap(fullpath);
                    try
                    {
                        qrdecode = decoder.decode(new QRCodeBitmapImage(bm));
                    }
                    catch
                    {
                        _result = "[{\"status\":0,\"count\":1,\"content\":\"" + "大小设置出错".ChinessConvertToUnicodeString() + "\"}]";
                        return _result;
                    }
                    bm.Dispose();

                    _result = "[{\"count\":1,\"list\":[{\"imgurl\":\"" + fullpath + "\",\"qrtext\":\"" + qrdecode + "\"}]}]";
                }
            }
            else
            {
                _result = "[{\"count\":0,\"list\":[{\"error\":\"" + "没有上传文件".ChinessConvertToUnicodeString() + "\"}]}]";
            }
            //删除缓存图片
            File.Delete(fullpath);
            return _result;
        }

        /*
        /// <summary>  
        /// 生成二维码.  
        /// </summary>  
        /// <param name="data">需要添加进去的文本</param>  
        /// <returns></returns>  
        public System.Drawing.Image GCode(String data)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeVersion = 7;

            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            var pbImg = qrCodeEncoder.Encode(data, System.Text.Encoding.UTF8);
            var width = pbImg.Width / 10;
            var dwidth = width * 2;
            Bitmap bmp = new Bitmap(pbImg.Width + dwidth, pbImg.Height + dwidth);
            Graphics g = Graphics.FromImage(bmp);
            var c = System.Drawing.Color.White;
            g.FillRectangle(new SolidBrush(c), 0, 0, pbImg.Width + dwidth, pbImg.Height + dwidth);
            g.DrawImage(pbImg, width, width);
            g.Dispose();
            return bmp;
        }
        */

        /// <summary>  
        /// 调用此函数后使此两种图片合并，类似相册，有个  
        /// 背景图，中间贴自己的目标图片  
        /// </summary>  
        /// <param name="sourceImg">粘贴的源图片</param>  
        /// <param name="destImg">粘贴的目标图片</param> 
        /// <param name="isCover">是否覆盖</param>
        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg, out string Qrcpath, bool isCover = false)
        {
            Qrcpath = string.Empty;
            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片    
            if (img.Height != 50 || img.Width != 50)
            {
                img = KiResizeImage(img, 50, 50, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  
            var filepath = System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") + "\\";
            var fullpath = string.Empty;
            if (!isCover)
            {
                fullpath = Qrcpath = destImg.Replace(".png", "") + "_Combin.png";
            }
            else
            {
                fullpath = Qrcpath = destImg;
            }

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            imgBack.Save(fullpath, System.Drawing.Imaging.ImageFormat.Png);
            GC.Collect();
            return imgBack;
        }
        /// <summary>
        /// 组合图片
        /// </summary>
        /// <param name="imgBack"></param>
        /// <param name="destImg"></param>
        /// <param name="isCover"></param>
        /// <returns></returns>
        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, string destImg, bool isCover = false)
        {

            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片    
            if (img.Height != 50 || img.Width != 50)
            {
                img = KiResizeImage(img, 20, 20, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  
            var filepath = System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") + "\\";
            var fullpath = string.Empty;

            fullpath = filepath + Guid.NewGuid().ToString() + ".png";

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            imgBack.Save(fullpath, System.Drawing.Imaging.ImageFormat.Png);
            GC.Collect();
            return imgBack;
        }
        /// <summary>
        /// 组合图片
        /// </summary>
        /// <param name="_imgBack"></param>
        /// <param name="destImg"></param>
        /// <param name="isCover"></param>
        /// <returns></returns>
        public static System.Drawing.Image CombinImage(string _imgBack, string destImg, bool isCover = false)
        {
            System.Drawing.Image imgBack = System.Drawing.Image.FromFile(_imgBack);        //照片图片
            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片    

            if (img.Height != 50 || img.Width != 50)
            {
                img = KiResizeImage(img, 50, 50, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  
            var filepath = System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") + "\\";
            var fullpath = string.Empty;
            if (!isCover)
            {
                fullpath = destImg.Replace(".png", "") + "_Combin.png";
            }
            else
            {
                fullpath = filepath + Guid.NewGuid().ToString() + ".png";
            }

            g.DrawImage(img, imgBack.Width / 2 - img.Width / 2, imgBack.Width / 2 - img.Width / 2, img.Width, img.Height);
            imgBack.Save(fullpath, System.Drawing.Imaging.ImageFormat.Png);
            GC.Collect();
            return imgBack;
        }
        /// <summary>
        /// 组合图片
        /// </summary>
        /// <param name="ResPath"></param>
        /// <param name="destImg"></param>
        /// <param name="Qrcpath"></param>
        /// <param name="isCover"></param>
        /// <returns></returns>
        public static System.Drawing.Image CombinImage(string ResPath, string destImg, out string Qrcpath, bool isCover = false)
        {
            Qrcpath = string.Empty;
            System.Drawing.Image img1 = System.Drawing.Image.FromFile(ResPath);        //照片图片
            System.Drawing.Image img = System.Drawing.Image.FromFile(destImg);        //照片图片    
            if (img.Height != 50 || img.Width != 50)
            {
                img = KiResizeImage(img, 50, 50, 0);
            }
            Graphics g = Graphics.FromImage(img1);

            g.DrawImage(img1, 0, 0, img1.Width, img1.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  
            var filepath = System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") + "\\";
            var fullpath = string.Empty;
            if (!isCover)
            {
                fullpath = Qrcpath = destImg.Replace(".png", "") + "_Combin.png";
                //fullpath = filepath + filename;

            }
            else
            {
                fullpath = Qrcpath = destImg;
            }

            g.DrawImage(img, img1.Width / 2 - img.Width / 2, img1.Width / 2 - img.Width / 2, img.Width, img.Height);
            img1.Save(fullpath, System.Drawing.Imaging.ImageFormat.Png);
            GC.Collect();
            return img1;
        }

        #region 废弃
        /*
        /// <summary>  
        /// 调用此函数后使此两种图片合并，类似相册，有个  
        /// 背景图，中间贴自己的目标图片  
        /// </summary>  
        /// <param name="sourceImg">粘贴的源图片</param>  
        /// <param name="destImg">粘贴的目标图片</param>  
        public static System.Drawing.Image CombinImage(System.Drawing.Image imgBack, System.Drawing.Image destImg)
        {


            if (destImg.Height != 50 || destImg.Width != 50)
            {
                destImg = KiResizeImage(destImg, 50, 50, 0);
            }
            Graphics g = Graphics.FromImage(imgBack);

            g.DrawImage(imgBack, 0, 0, imgBack.Width, imgBack.Height);      //g.DrawImage(imgBack, 0, 0, 相框宽, 相框高);   

            //g.FillRectangle(System.Drawing.Brushes.White, imgBack.Width / 2 - img.Width / 2 - 1, imgBack.Width / 2 - img.Width / 2 - 1,1,1);//相片四周刷一层黑色边框  

            //g.DrawImage(img, 照片与相框的左边距, 照片与相框的上边距, 照片宽, 照片高);  

            g.DrawImage(destImg, imgBack.Width / 2 - destImg.Width / 2, imgBack.Width / 2 - destImg.Width / 2, destImg.Width, destImg.Height);
            GC.Collect();
            return imgBack;
        }
        */
        #endregion

        /// <summary>  
        /// Resize图片  
        /// </summary>  
        /// <param name="bmp">原始Bitmap</param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>  
        /// <param name="Mode">保留着，暂时未用</param>  
        /// <returns>处理以后的图片</returns>  
        public static System.Drawing.Image KiResizeImage(System.Drawing.Image bmp, int newW, int newH, int Mode)
        {
            try
            {
                System.Drawing.Image b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);

                // 插值算法的质量  
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>  
        /// 10进制或16进制转换为中文  
        /// </summary>  
        /// <param name="name">要转换的字符串</param>  
        /// <param name="fromBase">进制（10或16）</param>  
        /// <returns></returns>  
        private static string ConverToGB(string text, int fromBase)
        {
            string value = text;
            MatchCollection mc;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (fromBase)
            {
                case 10:

                    MatchCollection mc1 = Regex.Matches(text, @"&#([\d]{5})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    foreach (Match _v in mc1)
                    {
                        string w = _v.Value.Substring(2);
                        w = Convert.ToString(int.Parse(w), 16);
                        byte[] c = new byte[2];
                        string ss = w.Substring(0, 2);
                        int c1 = Convert.ToInt32(w.Substring(0, 2), 16);
                        int c2 = Convert.ToInt32(w.Substring(2), 16);
                        c[0] = (byte)c2;
                        c[1] = (byte)c1;
                        sb.Append(Encoding.Unicode.GetString(c));
                    }
                    value = sb.ToString();

                    break;
                case 16:
                    mc = Regex.Matches(text, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    if (mc != null && mc.Count > 0)
                    {

                        foreach (Match m2 in mc)
                        {
                            string v = m2.Value;
                            string w = v.Substring(2);
                            byte[] c = new byte[2];
                            int c1 = Convert.ToInt32(w.Substring(0, 2), 16);
                            int c2 = Convert.ToInt32(w.Substring(2), 16);
                            c[0] = (byte)c2;
                            c[1] = (byte)c1;
                            sb.Append(Encoding.Unicode.GetString(c));
                        }
                        value = sb.ToString();
                    }
                    break;
            }
            return value;
        }

    }
    /// <summary>
    /// 类型
    /// </summary>
    public enum QRCodeType
    {
        Byte,
        AlphaNumeric,
        Numeric
    }
    /// <summary>
    /// 级别
    /// </summary>
    public enum QRCodeLevel
    {
        M,
        L,
        Q,
        H
    }
    /// <summary>
    /// 版本
    /// </summary>
    public enum QRCodeVersion
    {
        A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
    }


}
