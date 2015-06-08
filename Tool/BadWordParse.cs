using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tool
{
    public sealed class BadWordParse
    {
        #region 私有字段
        private BadWordParse _instance;
        private Dictionary<string, object> hash = new Dictionary<string, object>();
        private byte[] fastCheck = new byte[char.MaxValue];
        private BitArray charCheck = new BitArray(char.MaxValue);
        private int maxWordLength = 0;
        private int minWordLength = int.MaxValue;
        private bool _isHave = false;
        private string _replaceString="*";
        private char _splitString = '|';
        private string _filePath = "~/Data/badwords.txt";//从配置文件中读取脏字字典
        private object _lockHelper = new object();
        private bool isFile = true;
        /// <summary>
        /// 是否是文件
        /// </summary>
        public bool IsFile
        {
            //get { return isFile; }
            set { isFile = value; }
        }
        #endregion

        #region 属性
        ///
        /// 是否含有脏字
        ///

        public bool IsHave
        {
            get { return _isHave; }
        }
        /// <summary>
        /// 替换后字符串
        /// </summary>
        public string ReplaceString
        {
            set {  _replaceString = value;  }
        }
        /// <summary>
        /// 脏字字典切割符
        /// </summary>
        public char SplitString
        {
            set { _splitString = value; }
        }
        #endregion

        #region 构造函数

        //private BadWordParse() { }
        public BadWordParse()
        {
            Init();
        }
        private void Init()
        {
            string srList = string.Empty;
            if (this.isFile)
            {
                if (this._filePath.IndexOf("~") >= 0)
                {
                    this._filePath = System.Web.HttpContext.Current.Server.MapPath(this._filePath);
                }
                string _badWordFilePath = this._filePath;
                if (File.Exists(_badWordFilePath))
                {
                    StreamReader sr = new StreamReader(_badWordFilePath, Encoding.GetEncoding("gb2312"));
                    srList = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
            }
            else
            {

            }

            string[] badwords = srList.Split('|');
            foreach (string word in badwords)
            {

                this.maxWordLength = Math.Max(this.maxWordLength, word.Length);
                this.minWordLength = Math.Min(this.minWordLength, word.Length);
                for (int i = 0; i < 7 && i < word.Length; i++)
                {
                    this.fastCheck[word[i]] |= (byte)(1 << i);
                }

                for (int i = 7; i < word.Length; i++)
                {
                    this.fastCheck[word[i]] |= 0x80;
                }

                if (word.Length == 1)
                {
                    this.charCheck[word[0]] = true;
                }
                else
                {
                    try
                    {
                        this.hash.Add(word, null);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
        public BadWordParse Instance
        {
            get
            {
                if (this._instance == null)
                {
                    lock (this._lockHelper)
                    {
                        if (this._instance == null)
                        {
                            this._instance = new BadWordParse();
                        }
                    }
                }
                return this._instance;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 判断字符串中是否含有脏字
        /// </summary>
        /// <param name="source">要判断的字符串</param>
        /// <returns>bool判断结果</returns>
        public bool HasBadWord(string source,out string filter)
        {
            bool flag = false;
            int index = 0;
            filter = string.Empty;

            while (index < source.Length)
            {


                if ((fastCheck[source[index]] & 1) == 0)
                {
                    while (index < source.Length - 1 && (fastCheck[source[++index]] & 1) == 0) ;
                }

                //单字节检测
                if (minWordLength == 1 && charCheck[source[index]])
                {
                    return true;
                }


                //多字节检测
                for (int j = 1; j <= Math.Min(maxWordLength, source.Length - index - 1); j++)
                {
                    //快速排除
                    if ((fastCheck[source[index + j]] & (1 << Math.Min(j, 7))) == 0)
                    {
                        flag = true;
                        break;
                    }

                    if (j + 1 >= minWordLength)
                    {
                        string sub = source.Substring(index, j + 1);

                        if (this.hash.ContainsKey(sub))
                        {
                            filter = sub;
                            flag = true;
                            break;
                        }
                    }
                }
                index++;
            }
            return flag;
        }

        /// <summary>  
        /// 替换字符串中的脏字为指定的字符
        /// </summary>
        /// <param name="source">要替换的字符串</param>
        /// <returns>string替换过的字符串</returns>
        public string ReplaceBadWord(string source)
        {
            var filter = string.Empty;
            this.HasBadWord(source, out filter);
            int index = 0;
            for (index = 0; index < source.Length; index++)
            {
                if ((fastCheck[source[index]] & 1) == 0)
                {
                    while (index < source.Length - 1 && (fastCheck[source[++index]] & 1) == 0) ;
                }

                //单字节检测
                if (minWordLength == 1 && charCheck[source[index]])
                {
                    //return true;
                    _isHave = true;
                    source = source.Replace(source[index], _replaceString[0]);
                    continue;
                }
                //多字节检测
                for (int j = 1; j <= Math.Min(maxWordLength, source.Length - index - 1); j++)
                {
                    //快速排除
                    if ((fastCheck[source[index + j]] & (1 << Math.Min(j, 7))) == 0)
                    {
                        break;
                    }
                    if (j + 1 >= minWordLength)
                    {
                        string sub = source.Substring(index, j + 1);

                        if (hash.ContainsKey(sub))
                        {
                            //替换字符操作
                            _isHave = true;
                            char cc = _replaceString[0];
                            string rp = _replaceString.PadRight((j + 1), cc);
                            source = source.Replace(sub, rp);
                            //记录新位置
                            index += j;
                            continue;
                        }
                    }
                }
            }
            return source;
        }
        /// <summary>
        /// 替换字符串中的脏字为指定的字符
        /// </summary>
        /// <param name="source">要替换的字符串</param>
        /// <param name="insteadString">代替字符串</param>
        /// <returns>string替换过的字符串</returns>
        public string ReplaceBadWord(string source, string insteadString)
        {
            this.ReplaceString = insteadString;
            return ReplaceBadWord(source);
        }
        #endregion

    }
}