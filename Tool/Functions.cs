using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Tool
{
    public class Functions
    {
        /// <summary>
        /// 修改文件夹或文件权限
        /// </summary>
        /// <param name="path">文件夹或文件目录地址</param>
        /// <returns></returns>
        public bool SetDirectorySecurity(string path)
        {
            var flag = true;
            DirectorySecurity fSec = new DirectorySecurity();
            var access = new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
            fSec.AddAccessRule(access);
            //设置权限
            Directory.SetAccessControl(path, fSec);
            return flag;

        }
        
        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public int ConvertToUnix(object time)
        {
            var time1 = Convert.ToDateTime(time);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time1 - startTime).TotalSeconds;
        }
        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳格式</param>
        /// <returns>C#格式时间</returns>
        public DateTime ConvertToDateTime(object timeStamp)
        {
            var temp1 = Convert.ToString(timeStamp);
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(temp1 + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        /// <summary>
        /// 去除HTML标签
        /// </summary>
        /// <param name="Htmlstring">文本</param>
        /// <returns></returns>
        public string GetNoHtmlString(object Htmlstring)  //替换HTML标记
        {
            var temp1 = Convert.ToString(Htmlstring);
            temp1 = Regex.Replace(temp1, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            temp1 = Regex.Replace(temp1, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"-->", "", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"<!--.*", "", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            temp1 = Regex.Replace(temp1, @"<img[^>]*>;", "", RegexOptions.IgnoreCase);
            temp1.Replace("<", "");
            temp1.Replace(">", "");
            temp1.Replace("\r\n", "");
            temp1 = HttpContext.Current.Server.HtmlEncode(temp1).Trim();
            return temp1;
        }
        /// <summary>
        /// 获取整型数字
        /// </summary>
        /// <param name="query">参数</param>
        /// <returns></returns>
        public int GetQueryInt(string query)
        {
            var q = HttpContext.Current.Request[query];
            return Convert.ToInt32(GetNoHtmlString(q.ToString().Trim()));
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="query">参数</param>
        /// <returns></returns>
        public string GetQueryString(object query)
        {
            var temp1 = Convert.ToString(query);
            return Convert.ToString(GetNoHtmlString(temp1.ToString().Trim()));
        }
        /// <summary>
        /// 是否邮箱地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public bool IsEmail(string _input)
        {
            return Regex.IsMatch(_input, @"^\w+@\w+\.\w+$");
        }
        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public bool IsNumber(string _input)
        {
            return Regex.IsMatch(_input, @"^\d+$");
        }
        /// <summary>
        /// 是否链接地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public bool IsHtmlUrl(string _input)
        {
            return Regex.IsMatch(_input, @"^[A-Za-z]+://[A-Za-z0-9-_]+\\.[A-Za-z0-9-_%&\?\/.=]+$");
        }
        /// <summary>
        /// 是否电话号码
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public bool IsPhone(string _input)
        {
            return Regex.IsMatch(_input, @"^(\d{3,4}-)?\d{7,8})$|(13[0-9]{9}$");
        }
        /// <summary>
        /// 是否是邮编
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public bool IsPostCode(string _input)
        {
            return Regex.IsMatch(_input, @"^(\d{3,4}-)?\d{7,8})$|(13[0-9]{9}$");
        }
        /// <summary>
        /// 获取邮件地址
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string GetEmailAddress(string _input)
        {
            return Regex.Match(_input, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").ToString();
        }
        /// <summary>
        /// 得到身份证
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string GetIdCard(string _input)
        {
            return Regex.Match(_input, @"\d{18}|\d{15}").ToString();
        }
        /// <summary>
        /// 得到汉字
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string GetHanString(string _input)
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
        public string GetChaniessString(string _input)
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
        public string GetFloat(string _input)
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
        public string GetNumber(string _input)
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
        public string[] GetValueWithSplit(string _input, string start, string end)
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
        public string GetPostCode(string _input)
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
        public string GetPhoneNumber(string _input)
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
        public string[] GetImageSrc(string _input)
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
        public string[] GetHtmlUrl(string _input)
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
        /// <param name="color">颜色</param>
        /// <param name="isItalic">是否倾斜</param>
        /// <param name="isBold">是否加粗</param>
        /// <returns></returns>
        public string HighLight(string content, string keywords, string color, string fontFamily, string fontSize, string isItalic = "true", string isBold = "true")
        {
            string result = "";
            string rep_content = "<span style='font-family:\"" + fontFamily + "\";color:" + color;

            if (!string.IsNullOrEmpty(isItalic) || !string.IsNullOrEmpty(isBold) || !string.IsNullOrEmpty(fontSize))
            {
                if (!string.IsNullOrEmpty(fontSize))
                {
                    rep_content += ";font-size:" + fontSize;
                }
                if (!string.IsNullOrEmpty(isItalic))
                {
                    if (isItalic.Equals("true"))
                    {
                        rep_content += ";font-style:italic";
                    }
                }
                if (!string.IsNullOrEmpty(isBold))
                {
                    if (isBold.IsNumber() || isBold.Equals("true") || isBold.Equals("bolder") || isBold.Equals("bold"))
                    {
                        if (isBold.Equals("true") || isBold.Equals("bold"))
                        {
                            rep_content += ";font-weight:bold;";
                        }
                        if (isBold.Equals("true") || isBold.Equals("bolder"))
                        {
                            rep_content += ";font-weight:bolder;";
                        }
                        if (isBold.IsNumber())
                        {
                            rep_content += ";font-weight:" + isBold + ";";
                        }
                    }


                }

            }

            rep_content += "'>" + keywords + "</span>";
            result = content.Replace(keywords, rep_content);
            return result;
        }
        /// <summary>
        /// 小写
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string ToLower(string _input)
        {
            return _input.ToLower();
        }
        /// <summary>
        /// 去掉空格
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string ToTrim(string _input)
        {
            return _input.Trim();
        }
        /// <summary>
        /// 去掉左边空格
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string ToTrimLeft(string _input)
        {
            return _input.TrimStart();
        }
        /// <summary>
        /// 去掉右边空格
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string ToTrimRight(string _input)
        {
            return _input.TrimEnd();
        }
        /// <summary>
        /// 大写
        /// </summary>
        /// <param name="_input"></param>
        /// <returns></returns>
        public string ToUpper(string _input)
        {
            return _input.ToUpper();
        }
        /// <summary>
        /// 将首字母大写
        /// </summary>
        /// <param name="_input"></param>
        /// <returns>首字母大写</returns>
        public string ToFirstWordsUpper(string _input)
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
        public string ToWordsReverse(string _input)
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
        public string ToCharCode(string _input)
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
        public string CharCodeToString(string _input)
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
        public string ToUtf8String(string _input)
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
        public void ToGBKString(string _input)
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
        public string ChinessConvertToUnicodeString(string str)
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
        private string ConvertToUnicodeString(string str)
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
        public string ConvertToGB2312(string _input)
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
        public string ToMoney(string _input)
        {
            return string.Format("{0:C}", Convert.ToInt32(_input));
        }
        /// <summary>
        /// 转成Money
        /// </summary>
        /// <param name="_input">字符串</param>
        /// <param name="precision">精度</param>
        /// <returns></returns>
        public string ToMoney(string _input, int precision)
        {
            return string.Format("{0:C" + precision + "}", Convert.ToInt32(_input));
        }
        /// <summary>
        /// 格式化输出时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string DateTimeFormat(DateTime dt, FormatDate format = FormatDate.Normal)
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
        /// <summary>
        /// 默认文字
        /// </summary>
        /// <returns></returns>
        public string Detault(string _str = "", string _default = "管理员很懒，没有留下任何脚印")
        {
            var _result = string.Empty;
            _result = string.IsNullOrEmpty(_str) ? _default : _str;
            return _result;
        }
        /// <summary>
        /// Object转成Dictionary<string, string>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Dictionary<string, string> ConvertToDictionary<T>(T obj)
        {
            var _obj = typeof(T).GetProperties();
            var _d = new Dictionary<string, string>();
            var key = string.Empty;
            object type = null;
            object value = null;
            foreach (var item in _obj)
            {
                try
                {
                    key = item.Name.ToString();
                    value = item.GetValue(obj).ToString();
                    type = item.PropertyType.Name;
                    if (type.ToString().ToLower().IndexOf("int") > -1 && Convert.ToInt32(value) == 0)
                    {
                        continue;
                    }
                    else
                    {
                        _d.Add(key, value.ToString());
                    }
                }
                catch
                {
                    continue;
                }
            }
            return _d;
        }
        /// <summary>
        /// Object转成List<Dictionary<string,string>>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> ConvertToListDictionary<T>(List<T> obj)
        {
            var _obj = typeof(T).GetProperties();

            var _d = new List<Dictionary<string, string>>();

            foreach (var _list in obj)
            {
                var _t = _list.ConvertToDictionary<T>();
                var _temp = new Dictionary<string, string>();
                foreach (var item in _obj)
                {

                    try
                    {
                        var key = item.Name.ToString();
                        var value = _t[key].ToString();
                        var type = item.PropertyType.Name;
                        //_temp.Add(key, value);
                        if (type.ToString().ToLower().IndexOf("int") > -1 && Convert.ToInt32(value) == 0)
                        {
                            continue;
                        }
                        else
                        {
                            _temp.Add(key, value);
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
                _d.Add(_temp);
            }

            return _d;
        }
        /// <summary>
        /// md5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetMD5Hash(String input)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = md5.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            return s.ToString();
        }

    }
    /// <summary>
    /// 日期格式化枚举
    /// </summary>
    public enum FormatDate
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
