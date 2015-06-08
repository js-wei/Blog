using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Tool
{
    public static class Fantion
    {
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertToUnix(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public static DateTime ConvertToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 去除HTML标签
        /// </summary>
        /// <param name="Htmlstring">文本</param>
        /// <returns></returns>
        public static string GetNoHtmlString(string Htmlstring)  //替换HTML标记   
        { //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        /// <summary>
        /// 获取整型数字
        /// </summary>
        /// <param name="query">参数</param>
        /// <returns></returns>
        public static int GetQueryInt(string query)
        {
            var q = HttpContext.Current.Request[query];
            return Convert.ToInt32(GetNoHtmlString(q.ToString().Trim()));
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="query">参数</param>
        /// <returns></returns>
        public static string GetQueryString(string query)
        {
            var q = (object)query;
            return Convert.ToString(GetNoHtmlString(q.ToString().Trim()));
        }
        /// <summary>
        /// 是否邮箱地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static bool IsEmail(string _input)
        {
            return Regex.IsMatch(_input, @"^\w+@\w+\.\w+$");
        }
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static bool IsNumber(string _input)
        {
            return Regex.IsMatch(_input, @"^\d+$");
        }
        /// <summary>
        /// 是否链接地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static bool IsHtmlUrl(string _input)
        {
            return Regex.IsMatch(_input, @"^[A-Za-z]+://[A-Za-z0-9-_]+\\.[A-Za-z0-9-_%&\?\/.=]+$");
        }
        /// <summary>
        /// 是否电话号码
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static bool IsPhone(string _input)
        {
            return Regex.IsMatch(_input, @"^(\d{3,4}-)?\d{7,8})$|(13[0-9]{9}$");
        }
        /// <summary>
        /// 是否是邮编
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static bool IsPostCode(string _input)
        {
            return Regex.IsMatch(_input, @"^(\d{3,4}-)?\d{7,8})$|(13[0-9]{9}$");
        }
        /// <summary>
        /// 获取邮件地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetEmailAddress(string _input)
        {
            return Regex.Match(_input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").ToString();
        }
        /// <summary>
        /// 得到身份证
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetIdCard(string _input)
        {
            return Regex.Match(_input, @"\d{18}|\d{15}").ToString();
        }
        /// <summary>
        /// 得到汉字
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetHanString(string _input)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, @"[\u4e00-\u9fa5]*", RegexOptions.IgnoreCase);
            var result = string.Empty;
            // 取得匹配项列表
            foreach (Match match in matches)
            {
                var temp = match.Groups[0].Value;
                if (!string.IsNullOrEmpty(temp))
                {
                    result += temp;
                }
            }
            return result;
        }
        /// <summary>
        /// 得到中文(含标点符号)
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetChaniessString(string _input)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, @"[^\x00-\xff]*", RegexOptions.IgnoreCase);
            var result = string.Empty;
            // 取得匹配项列表
            foreach (Match match in matches)
            {
                var temp = match.Groups[0].Value;
                if (!string.IsNullOrEmpty(temp))
                {
                    result += temp;
                }
            }
            return result;
        }
        /// <summary>
        /// 得到小数
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetFloat(string _input)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, @"(?<number>(\+|-)?(0|[1-9]\d*)(\.\d*[0-9])?)", RegexOptions.IgnoreCase);
            var result = string.Empty;
            // 取得匹配项列表
            foreach (Match match in matches)
            {
                var temp = match.Groups["number"].Value;
                if (!string.IsNullOrEmpty(temp) && temp.IndexOf('.') > -1)
                {
                    result += temp + " ";
                }
            }
            return result;
        }
        /// <summary>
        /// 的到数字
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetNumber(string _input)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, @"(?<number>(\+|-)?(0|[1-9]\d*)(\.\d*[0-9])?)", RegexOptions.IgnoreCase);
            var result = string.Empty;
            // 取得匹配项列表
            foreach (Match match in matches)
            {
                var temp = match.Groups["number"].Value;
                if (!string.IsNullOrEmpty(temp))
                {
                    result += temp + " ";
                }
            }
            return result;
        }
        /// <summary>
        /// 获取定界符之间的值
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="start">开始符号</param>
        /// <param name="end">结束符号</param>
        /// <returns></returns>
        public static string[] GetValueWithSplit(string _input, string start, string end)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, @"(?<=(" + start + "))[.\\s\\S]*?(?=(" + end + "))", RegexOptions.IgnoreCase);
            var result = new string[matches.Count];
            var i = 0;
            // 取得匹配项列表
            foreach (Match match in matches)
            {
                var temp = match.Groups[0].Value;
                if (!string.IsNullOrEmpty(temp))
                {
                    result[i++] = temp;
                }
            }

            return result;
        }
        /// <summary>
        /// 获取邮编
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetPostCode(string _input)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, @"([1-9]{1}(\d+){5})", RegexOptions.IgnoreCase);
            var result = string.Empty;
            // 取得匹配项列表
            foreach (Match match in matches)
            {
                var temp = match.Groups[0].Value;
                if (!string.IsNullOrEmpty(temp))
                {
                    result += temp + " ";
                }
            }
            return result;
        }
        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string GetPhoneNumber(string _input)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, @"(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}", RegexOptions.IgnoreCase);
            var result = string.Empty;
            // 取得匹配项列表
            foreach (Match match in matches)
            {
                var temp = match.Groups[0].Value;
                if (!string.IsNullOrEmpty(temp))
                {
                    result += temp + " ";
                }
            }
            return result;
        }
        /// <summary>
        /// 获取图片地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string[] GetImageSrc(string _input)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(_input);

            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;

            return sUrlList;
        }
        /// <summary>
        /// 获取链接地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string[] GetHtmlUrl(string _input)
        {
            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(_input, "(?<=href=\")[^\"]*", RegexOptions.IgnoreCase);
            var result = string.Empty;
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups[0].Value;

            return sUrlList;
        }
        /// <summary>
        /// 高亮显示
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="keywords">关键词</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="color">颜色</param>
        /// <param name="isItalic">是否倾斜</param>
        /// <param name="isBold">是否加粗</param>
        /// <returns></returns>
        public static string HighLight(string content, string keywords, string fontFamily, int fontSize, string color, bool isItalic, bool isBold)
        {

            string result = "";
            string rep_content = "<span style='font-family:\"" + fontFamily + "\";color:" + color + ";font-size:\"" + fontSize + "\"";
            if (isItalic)
            {
                rep_content += ";font-style:italic";
            }
            if (isBold)
            {
                rep_content += ";font-weight:bold;";
            }
            rep_content += "'>" + keywords + "</span>";
            result = content.Replace(keywords, rep_content);
            return result;
        }
        /// <summary>
        /// 高亮显示
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="keywords">关键词</param>
        /// <param name="fontFamily">字体</param>
        /// <param name="color">颜色</param>
        /// <param name="isItalic">是否倾斜</param>
        /// <param name="isBold">是否加粗</param>
        /// <returns></returns>
        public static string HighLight(string content, string keywords, string fontFamily, string color, bool isItalic, bool isBold)
        {

            string result = "";
            string rep_content = "<span style='font-family:\"" + fontFamily + "\";color:" + color;
            if (isItalic)
            {
                rep_content += ";font-style:italic";
            }
            if (isBold)
            {
                rep_content += ";font-weight:bold;";
            }
            rep_content += "'>" + keywords + "</span>";
            result = content.Replace(keywords, rep_content);
            return result;
        }
        /// <summary>
        /// 高亮显示
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="keywords">关键词</param>
        /// <param name="color">颜色</param>
        /// <param name="isItalic">是否倾斜</param>
        /// <param name="isBold">是否加粗</param>
        /// <returns></returns>
        public static string HighLight(string content, string keywords, string color, bool isItalic, bool isBold)
        {

            string result = "";
            string rep_content = "<span style='color:" + color;
            if (isItalic)
            {
                rep_content += ";font-style:italic";
            }
            if (isBold)
            {
                rep_content += ";font-weight:bold;";
            }
            rep_content += "'>" + keywords + "</span>";
            result = content.Replace(keywords, rep_content);
            return result;
        }
        /// <summary>
        /// 将首字母大写
        /// </summary>
        /// <param name="_input"></param>
        /// <returns>首字母大写</returns>
        public static string ToFirstWordsUpper(string _input)
        {
            var result = string.Empty;
            var temp = _input.Split(' ');
            foreach (var item in temp)
            {
                var f = item[0].ToString().ToUpper();
                var t = item.Substring(1);
                var o = f + t + " ";
                result += o;
            }
            return result;
        }
        /// <summary>
        /// 字母大小写反转
        /// </summary>
        /// <param name="_input"></param>
        /// <returns>字母大小写反转</returns>
        public static string ToWordsReverse(string _input)
        {
            var result = string.Empty;
            var temp = _input.Split(' ');
            foreach (var t in temp)
            {
                foreach (var item in t)
                {
                    var i = (int)Convert.ToChar(item.ToString());
                    if (((i >= 97) && (i <= 122)) || ((i >= 65) && (i <= 90)))
                    {
                        if ((i >= 65) && (i <= 90))
                        {

                        }
                        else
                        {
                            result += item.ToString().ToUpper();
                        }
                    }
                    else
                    {
                        result += item.ToString();
                    }
                }
                result += " ";
            }
            return result;
        }
        /// <summary>
        /// 获取字符ASSIC码
        /// </summary>
        /// <param name="_input"></param>
        /// <returns>获取字符ASSIC码</returns>
        public static string ToCharCode(string _input)
        {
            var result = string.Empty;
            var temp = _input.Split(' ');
            var m = 0;
            foreach (var t in temp)
            {
                m = 0;
                foreach (var item in t)
                {
                    var j = t.ToString().Length;
                    var i = (int)Convert.ToChar(item.ToString());
                    if (++m < j)
                    {
                        result += i.ToString() + ",";
                    }
                    else
                    {
                        result += i.ToString();
                    }
                }
                result += " ";
            }
            return result;
        }
        /// <summary>
        /// 获取ASSIC码对应字符
        /// </summary>
        /// <param name="_input"></param>
        /// <returns>获取ASSIC码对应字符</returns>
        public static string CharCodeToString(string _input)
        {
            var result = string.Empty;
            result = Encoding.ASCII.GetString(new byte[] { Convert.ToByte(_input) });
            return result;
        }
        /// <summary>
        /// 获取Utf8编码字符
        /// </summary>
        /// <param name="_input"></param>
        /// <returns>获取Utf8编码字符</returns>
        public static string ToUtf8String(string _input)
        {
            var UTF8 = new System.Text.UTF8Encoding();
            Byte[] bytes = UTF8.GetBytes(_input);

            string recordString = Encoding.GetEncoding("UTF-8").GetString(bytes);

            return recordString;
        }
        /// <summary>
        /// 获取Utf8编码字符
        /// </summary>
        /// <param name="_input"></param>
        /// <returns>获取Utf8编码字符</returns>
        public static void ToGBKString(string _input)
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //byte[] bytes;
            //MemoryStream ms = new MemoryStream();
            //bf.Serialize(ms, _input);
            //ms.Seek(0, 0);
            //bytes = ms.ToArray();
            _input = ConvertToUnicodeString(_input);
            string r = "";
            MatchCollection mc = Regex.Matches(_input, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            //return r;
        }
        /// <summary>
        /// 汉字转换为Unicode编码
        /// </summary>
        /// <param name="str">要编码的汉字字符串</param>
        /// <returns>Unicode编码的的字符串</returns>
        public static string ChinessConvertToUnicodeString(string str)
        {
            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }
        /// <summary>
        /// 汉字转换为Unicode编码
        /// </summary>
        /// <param name="str">要编码的汉字字符串</param>
        /// <returns>Unicode编码的的字符串</returns>
        private static string ConvertToUnicodeString(string str)
        {
            byte[] bts = Encoding.Unicode.GetBytes(str);
            string r = "";
            for (int i = 0; i < bts.Length; i += 2) r += "\\u" + bts[i + 1].ToString("x").PadLeft(2, '0') + bts[i].ToString("x").PadLeft(2, '0');
            return r;
        }
        /// <summary>
        /// 将Unicode编码转换为汉字字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>汉字字符串</returns>
        public static string ConvertToGB2312(string _input)
        {
            _input = ConvertToUnicodeString(_input);
            string r = "";
            MatchCollection mc = Regex.Matches(_input, @"\\u([\w]{2})([\w]{2})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            byte[] bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte)int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte)int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }
        /// <summary>
        /// 转成Money
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public static string ToMoney(string _input)
        {
            return string.Format("{0:C}", Convert.ToInt32(_input));
        }
        /// <summary>
        /// 转成Money
        /// </summary>
        /// <param name="_input">字符串</param>
        /// <param name="precision">精度</param>
        /// <returns></returns>
        public static string ToMoney(string _input, int precision)
        {
            return string.Format("{0:C" + precision + "}", Convert.ToInt32(_input));
        }
        /// <summary>
        /// 格式化输出时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Format(DateTime dt, FormatDateType format = FormatDateType.Normal)
        {
            var dtfi = DateTimeFormatInfo.InvariantInfo;
            var _result = string.Empty;
            switch (format.ToString())
            {
                case "ShortDate":
                    _result = dt.ToString("d", dtfi);
                    break;
                case "FullDate":
                    _result = dt.ToString("G", dtfi);
                    break;
                case "Date":
                    _result = dt.ToString("g", dtfi);
                    break;
                case "Normal":
                    _result = dt.ToString("u", dtfi);
                    _result = _result.Substring(0, _result.Length - 1);
                    break;
                case "Time1":
                    _result = dt.ToString("t", dtfi);
                    break;
                case "Time":   
                default:
                    _result = dt.ToString("T", dtfi);
                    break;
            }
            return _result;
        }

    }
    /// <summary>
    /// 日期格式化枚举
    /// </summary>
    public enum FormatDateType
    {
        [Description("MM/dd/yyyy")]
        ShortDate,
        [Description("dddd,MMMM dd,yyyy HH:mm")]
        FullDate,
        [Description("yyyy-MM-dd HH:mm:ss")]
        Normal,
        [Description("MMMM,yyyy")]
        Date,
        [Description("HH:mm:ss")]
        Time,
        [Description("HH:mm")]
        Time1,
    }
}
