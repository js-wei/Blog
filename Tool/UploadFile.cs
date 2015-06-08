using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace Tool
{
    #region 文件上传操作类
    /// <summary>
    /// 文件上传操作类
    /// </summary>
    public class UploadFile
    {
        #region 参数信息
        private bool autoSub = false;
        /// <summary>
        /// 自动创建子目录
        /// </summary>
        public bool AutoSub
        {
            set { autoSub = value; }
        }
        private AutoSubType autoSubType = AutoSubType.date;
        /// <summary>
        /// 子目录类型
        /// </summary>
        public AutoSubType AutoSubType
        {
            set { autoSubType = value; }
        }
        /// <summary>
        /// 文件大笑
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 上传路径
        /// </summary>
        public string path { get; set; }
        private string md5Hash;
        /// <summary>
        /// md5生成串
        /// </summary>
        public string Md5Hash
        {
            set { md5Hash = value; }
        }
        private string[] fileType;
        /// <summary>
        /// 允许上传文件
        /// </summary>
        public string[] FileType
        {
            //get { return fileType; }
            set { fileType = value; }
        }
        private bool isRename = false;
        /// <summary>
        /// 是否重命名
        /// </summary>
        public bool IsRename
        {
            set { isRename = value; }
        }
        private List<Dictionary<string, string>> getFileInfo;
        /// <summary>
        /// 获取上传文件信息
        /// </summary>
        public List<Dictionary<string, string>> GetFileInfo
        {
            get { return getFileInfo; }
        }

        private string massege;

        public string Massege
        {
            get { return massege; }
        }

        #endregion

        #region 构造函数
        public UploadFile()
        {
            //默认上传目录
            this.path = "~/UploadFile";
            //默认md5串
            this.md5Hash = "hello";
            //默认允许最大
            this.size = 5;
            //默认允许上传类型
            this.FileType = new string[] { ".jpg", ".gif", ".jpeg", ".mp3", ".mp4", ".zip", ".rar", ".txt" };
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileUpload">文件</param>
        /// <param name="path">文件上传路径</param>
        /// <returns>bool</returns>
        private bool SaveFile(FileUpload fileUpload, string path)
        {
            //上传
            fileUpload.SaveAs(HttpContext.Current.Server.MapPath(path));
            //fileUpload.SaveAs(path);
            return true;
        }
        #endregion

        #region checkFile
        /// <summary>
        /// 上传文件 文件类型 大小KB 路径 必须在指定范围以内
        /// </summary>
        /// <param name="fileUpload">要上传文件</param>
        /// <param name="Path">上传路径</param>
        /// <param name="FileType">数组:文件后缀名</param>
        /// <param name="length">文件大小KB</param>
        /// <returns>bool</returns>
        private bool SaveFile(FileUpload fileUpload, string Path, ref string msg)
        {
            //获取文件要保存的路径   //从配置文件读取
            string FilePath = Path;
            //获取文件后缀名
            string strEx = Path.Substring(Path.LastIndexOf("."));
            //定义是否包含数组内的文件格式
            bool ft = false;
            var FileType = this.fileType;
            //得到FileType数组长度
            int len = FileType.Length;
            string strType = "";
            for (int i = 0; i < len; i++)
            {
                var temp = string.Empty;
                if (FileType[i].IndexOf('.') < 0)
                {
                    temp = "." + FileType[i];
                }
                else
                {
                    temp = FileType[i];
                }
                if (temp.ToLower() == strEx.ToLower())
                {
                    ft = true;
                    break;
                }
                strType += FileType[i] + "或";
            }
            var length = this.size * 1024;
            //如果包含文件后缀名
            if (ft)
            {
                if (((int.Parse(fileUpload.FileContent.Length.ToString())) / 1024) < length)
                {
                    if (Path != "")
                    {
                        //string strPath = FilePath + Path;
                        SaveFile(fileUpload, Path);
                        return true;
                    }
                    else
                    {
                        //目录为空
                        return false;
                    }
                }
                else
                {
                    //大小超出范围
                    string s = "对不起,您上传文件的大小超出范围!不能超过" + this.size + "M";
                    msg = s;
                    return false;
                }
            }
            else
            {
                //后缀名不合格
                strType = strType.Substring(0, strType.Length - 1);
                string s = "对不起,您上传文件的格式不正确!标准格式应为:" + strType;
                this.massege = msg = s;
                return false;
            }
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileUpload">上传文件</param>
        /// <param name="path">路径 文件夹+文件名</param>
        /// <param name="FileType">文件类型</param>
        /// <returns>string path路径</returns>
        public string SaveFile(FileUpload fileUpload, ref string msg)
        {
            //if (fileUpload.HasFile)
            //{
            //获取物理路径
            string FileName = fileUpload.PostedFile.FileName;
            //获取后缀名
            string strEx = FileName.Substring(FileName.LastIndexOf("."));
            var _name = FileName;
            var _rename = string.Empty;
            //获取文件大小KB  //从配置文件读取
            int FileSize = this.size;
            //获取文件要保存路径  //从配置文件读取
            string FilePath = this.path;
            var fullPath = string.Empty;
            var _size = fileUpload.PostedFile.ContentLength.ToString();
            var _type = fileUpload.PostedFile.ContentType.ToString();
            var basePath = string.Empty;
            var _list =new  List<Dictionary<string, string>>();
            Dictionary<string, string> _d = new Dictionary<string, string>();
            // 
            if (FilePath != "")
            {
                //自动创建子目录
                if (this.autoSub)
                {
                    basePath = FilePath += "/" + this.GetSunDirectoryName(this.autoSubType);
                }
                if (this.isRename)
                {
                    _rename = this.GetFileRename() + strEx;
                    fullPath = FilePath + "/" + _rename;
                }
                else
                {
                    fullPath = FilePath + "/" + _name;
                }
                var _path = ConvertToRelative(basePath);
                //如果源文件夹不存在则创建
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                if (SaveFile(fileUpload, fullPath, ref msg))
                {
                    _size = (Math.Round(Convert.ToInt32(_size) / 1024.0, 2)).ToString();
                    _d.Add("status", "1");
                    _d.Add("name", _name);
                    _d.Add("rename", _rename);
                    _d.Add("path", fullPath.Replace("~", "."));
                    _d.Add("size", _size + "kB");
                    _d.Add("type", strEx);
                    _d.Add("timespan", DateTime.Now.ConvertToUnix().ToString());
                }
                else
                {
                    _d.Add("status", "0");
                    _d.Add("messge", msg);
                }
            }
            _list.Add(_d);
            this.getFileInfo = _list;
            var data = JsonConvert.SerializeObject(_list);
            return data;
        }
        /// <summary>
        /// 上传多文件
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SaveMultiFile(ref string msg)
        {
            var files = HttpContext.Current.Request.Files;
            var _result = new List<Dictionary<string, string>>();


            if (files.Count > 0)
            {
                var temp = new Dictionary<string, string>();
                for (var i = 0; i < files.Count; i++)
                {

                    //获取物理路径
                    string FileName = files[i].FileName;
                    //获取后缀名
                    string strEx = FileName.Substring(FileName.LastIndexOf("."));
                    var _name = FileName;
                    var _rename = string.Empty;
                    //获取文件大小KB  //从配置文件读取
                    int FileSize = this.size;
                    //获取文件要保存路径  //从配置文件读取
                    string FilePath = this.path;
                    var fullPath = string.Empty;
                    var _size = files[i].ContentLength.ToString();
                    var _type = files[i].ContentType.ToString();
                    var basePath = string.Empty;

                    // 
                    if (FilePath != "")
                    {
                        //自动创建子目录
                        if (this.autoSub)
                        {
                            basePath = FilePath += "/" + this.GetSunDirectoryName(this.autoSubType);
                        }
                        if (this.isRename)
                        {
                            _rename = this.GetFileRename() + strEx;
                            fullPath = FilePath + "/" + _rename;
                        }
                        else
                        {
                            fullPath = FilePath + "/" + _name;
                        }
                        var _path = ConvertToRelative(basePath);
                        //如果源文件夹不存在则创建
                        if (!Directory.Exists(_path))
                        {
                            Directory.CreateDirectory(_path);
                        }
                        if (_ckeck(files, i))
                        {

                            var savepath = this.ConvertToRelative(fullPath);
                            files[i].SaveAs(savepath);
                            _size = (Math.Round(Convert.ToInt32(_size) / 1024.0, 2)).ToString();

                            //string[,] _reg = { { "status", "ok" }, { "name", _name }, { "rename", _rename }, { "size", _size }, { "suffix", strEx }, { "savepath", fullPath.Replace("~/", "./") }, { "timespan", DateTime.Now.ConvertToUnix().ToString() } };
                            temp.Add("status", "0");
                            temp.Add("name", _name);
                            temp.Add("rename", _rename);
                            temp.Add("size", _size);
                            temp.Add("type", strEx);
                            temp.Add("savepath", fullPath.Replace("~/", "./"));
                            temp.Add("timespan", DateTime.Now.ConvertToUnix().ToString());
                            //_result.Add(_reg);
                        }
                        else
                        {
                            //string[,] _reg = { { "status", "error" }, { "massge", this.massege }, { "timespan", DateTime.Now.ConvertToUnix().ToString() } };
                            //_result.Add(_reg);
                            temp.Add("status", "1");
                            temp.Add("massge", this.massege);
                            temp.Add("timespan", DateTime.Now.ConvertToUnix().ToString());
                        }

                    }
                }
                _result.Add(temp);
            }
            this.getFileInfo = _result;
            var data = JsonConvert.SerializeObject(_result);
            return data;
        }
        #endregion

        #region 目录名
        /// <summary>
        /// 获取上传子目录名
        /// </summary>
        /// <returns></returns>
        private string GetSunDirectoryName(AutoSubType _type = AutoSubType.date)
        {
            var _result = string.Empty;
            switch (_type.ToString())
            {
                case "md5":
                    _result = this.GetMD5Hash(this.md5Hash).Substring(20, 6);
                    break;
                case "guid":
                    _result = Guid.NewGuid().ToString().Substring(30, 6);
                    break;
                case "date":
                default:
                    _result = DateTime.Now.ToString("yyMMdd");
                    break;
            }
            return _result;
        }
        /// <summary>
        /// 验证文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool _ckeck(System.Web.HttpFileCollection files, int index)
        {
            var flag = true;
            //获取物理路径
            string FileName = files[index].FileName;
            //获取后缀名
            string strEx = FileName.Substring(FileName.LastIndexOf("."));
            var _size = files[index].ContentLength.ToString();
            //获取文件要保存的路径 

            //定义是否包含数组内的文件格式
            bool ft = false;
            var FileType = this.fileType;
            //得到FileType数组长度
            int len = FileType.Length;
            string strType = "";
            for (int i = 0; i < len; i++)
            {
                var temp = string.Empty;
                if (FileType[i].IndexOf('.') < 0)
                {
                    temp = "." + FileType[i];
                }
                else
                {
                    temp = FileType[i];
                }
                if (temp.ToLower() == strEx.ToLower())
                {
                    ft = true;
                    break;
                }
                strType += FileType[i] + "或";
            }
            var length = this.size * 1024;
            //如果包含文件后缀名
            if (ft)
            {
                if (((int.Parse(files[index].ContentLength.ToString())) / 1024) < length)
                {
                    flag = true;
                }
                else
                {
                    //大小超出范围
                    string s = "对不起,您上传文件的大小超出范围!不能超过" + length + "KB";
                    this.massege = s;
                    flag = false;
                }
            }
            else
            {
                //后缀名不合格
                strType = strType.Substring(0, strType.Length - 1);
                string s = "对不起,您上传文件的格式不正确!标准格式应为:" + strType;
                this.massege = s;
                return false;
            }

            return flag;
        }
        #endregion

        #region 得到重置文件名
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <returns></returns>
        private string GetFileRename()
        {
            var _result = string.Empty;
            _result = DateTime.Now.ConvertToUnix().ToString();
            return _result;
        }
        /// <summary>
        /// md5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string GetMD5Hash(string input)
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
        #endregion

        #region 转成相对路径
        private string ConvertToRelative(string _path)
        {
            var _result = string.Empty;
            if (_path.IndexOf('~') >= 0)
            {
                _result = HttpContext.Current.Server.MapPath(_path);
            }
            else
            {
                _result = _path;
            }
            return _result;
        }
        #endregion
    }
    #endregion

    #region 子目录枚举
    /// <summary>
    /// 创建子目录类型
    /// </summary>
    public enum AutoSubType
    {
        date,
        md5,
        guid
    }
    #endregion

}
