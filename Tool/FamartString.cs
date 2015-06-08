using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Tool
{
    public static class FamartString
    {
        public static string SplitContent(string content)
        {
            string con = "";
            if (content.Length > 40)
            {
                con = content.Remove(40) + "...";
            }
            else
            {
                con = content;
            }
            return con;
        }
        public static string SplitContent(string content, int lenght)
        {
            string con = "";
            if (content.Length > lenght)
            {
                con = content.Remove(lenght) + "...";
            }
            else
            {
                con = content;
            }
            return con;
        }
        /// <summary>
        /// 截取内容
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="lenght">长度</param>
        /// <param name="isBypass">省略号</param>
        /// <returns></returns>
        public static string SplitContent(string content, int lenght, bool isBypass)
        {
            string con = "";
            if (content.Length > lenght)
            {
                if (isBypass)
                {
                    con = content.Remove(lenght) + "...";
                }
                else
                {
                    con = content.Remove(lenght);
                }
            }
            else
            {
                con = content;
            }
            return con;
        }
        #region 废弃
        /*public static string NoHTML(string Htmlstring)  //替换HTML标记   
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
        }*/
        #endregion
        public static string FormatUrl(string url)
        {
            string uri = "";
            url = url.TrimStart();
            if (url.Contains("http://"))
            {
                uri = url.Replace("http://", "").TrimStart();
            }
            else
            {
                uri = url;
            }
            return uri;
        }
        /// <summary>
        /// 拆分关键词
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="stemp">步长</param>
        /// <returns></returns>
        public static string SplitKey(string title, int stemp)
        {
            //var eero="title like '%title like '%2%'and title like '%0%'and title like '%1%";
            string keys = "";
            var arr = title.ToArray();
            for (int i = 0; i < arr.Length; i += stemp)
            {
                var tit = "title like '%";
                keys += tit + arr[i] + "%'or ";

            }
            int remove = keys.LastIndexOf("or");
            return keys.Remove(remove);
        }
        public static string SplitKey(string title)
        {
            //var eero="title like '%title like '%2%'and title like '%0%'and title like '%1%";
            string keys = "";
            var arr = title.ToArray();
            foreach (var item in arr)
            {
                var tit = "title like '%";
                keys += tit + item + "%'or ";
            }
            int remove = keys.LastIndexOf("or");
            return keys.Remove(remove);
        }
        /// <summary>
        /// 成组地址
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public static string FormatPicUrl(string url, int index)
        {
            string uri = "";
            index = index + 1;
            uri += url + "=" + index;
            return uri;
        }
        /// <summary>
        /// 高亮显示
        /// </summary>
        /// <param name="key">关键词</param>
        /// <param name="content">内容</param>
        /// <param name="color">颜色</param>
        /// <param name="isItalic">是否倾斜</param>
        /// <param name="isBold">是否加粗</param>
        /// <returns></returns>
        public static string Highlight(string key, string content, string color, bool isItalic, bool isBold)
        {
            //font-style:italic;
            //font-weight:bold;
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
            rep_content += "'>" + key + "</span>";
            result = content.Replace(key, rep_content);
            return result;
        }
    }
}
