using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tool
{
    public class Config
    {
        /// <summary>
        /// 获取皮肤
        /// </summary>
        /// <returns></returns>
        public static string GetSkinUrl()
        {
            string startup = Application.ExecutablePath;       //取得程序路径
            int pp = startup.LastIndexOf("\\");
            string skin = startup.Substring(0, pp) + "/skin/MacOS.ssk";
            return skin;
        }
        /// <summary>
        /// 获取皮肤
        /// </summary>
        /// <param name="skinName">皮肤名称</param>
        /// <returns></returns>
        public static string GetSkinUrl(string skinName)
        {
            string startup = Application.ExecutablePath;       //取得程序路径
            int pp = startup.LastIndexOf("\\");
            string skin = startup.Substring(0, pp) + "/skin/" + skinName; //string icon = startup + "\\home.ico";
            return skin;
        }
        /// <summary>
        /// 获取网站Ico图标
        /// </summary>
        /// <returns></returns>
        public static string GetIconUrl()
        {
            string startup = Application.ExecutablePath;       //取得程序路径
            int pp = startup.LastIndexOf("\\");
            startup = startup.Substring(0, pp);
            string icon = startup + "\\home.ico";
            return icon;
        }
        /// <summary>
        /// 获取数据库连接串
        /// </summary>
        /// <param name="SqlServerConnString">数据库配置串名称</param>
        /// <returns></returns>
        public static string GetDataBaseConnection(string SqlServerConnString)        //取得数据库连接
        {
            string conn = System.Configuration.ConfigurationManager.AppSettings[SqlServerConnString].ToString();
            return conn;
        }
        /// <summary>
        /// 取得Image目录
        /// </summary>
        /// <returns>Image地址</returns>
        public static string GetBaseImageUrl()
        {
            string startup = Application.ExecutablePath;       //取得程序路径
            int pp = startup.LastIndexOf("\\");
            return startup.Substring(0, pp) + "/Images";
        }
        /// <summary>
        /// 取得Script目录
        /// </summary>
        /// <returns>Image地址</returns>
        public static string GetBaseScriptUrl()
        {
            string startup = Application.ExecutablePath;       //取得程序路径
            int pp = startup.LastIndexOf("\\");
            return startup.Substring(0, pp) + "/Scripts";
        }
        /// <summary>
        /// 取得Style目录
        /// </summary>
        /// <returns>Image地址</returns>
        public static string GetBaseStyleUrl()
        {
            string startup = Application.ExecutablePath;       //取得程序路径
            int pp = startup.LastIndexOf("\\");
            return startup.Substring(0, pp) + "/Style";
        }
        /// <summary>
        /// 加载相对路径资源文件
        /// </summary>
        /// <param name="resource">Array资源名称集合</param>
        /// <returns>string 返回资源</returns>
        public static string Load(Array resource)
        {
            var res = string.Empty;
            foreach (var item in resource)
            {
                var type = _type(item);
                switch (type)
                {
                    case "js":
                        res += "<script type=\"text/javascript\"";
                        res += "src=\"" + GetBaseScriptUrl() + "/" + item.ToString();
                        res += "\"></script>";
                        break;
                    case "css":
                        res += "<link type=\"text/css\"";
                        res += "href=\"" + GetBaseScriptUrl() + "/" + item.ToString();
                        res += "\"/>";
                        break;
                    case "":
                    default:
                        res += "<img ";
                        res += "src=\"" + GetBaseScriptUrl() + "/" + item.ToString();
                        res += "\">";
                        break;
                }
            }
            return res;
        }
        /// <summary>
        /// 加载绝对路径资源文件
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static string LoadRelativeResource(Array resource)
        {
            var res = string.Empty;
            foreach (var item in resource)
            {
                var type = _type(item);
                switch (type)
                {
                    case "js":
                        res += "<script type=\"text/javascript\" ";
                        res += "src=\"" + "./Scripts/" + item.ToString();
                        res += "\"></script>";
                        break;
                    case "css":
                        res += "<link rel=\"stylesheet\" ";
                        res += "href=\"" + "./Style/common.css";
                        res += "\"/>";
                        break;
                    case "":
                    default:
                        res += "<img ";
                        res += "src=\"" + "./Images/" + item.ToString();
                        res += "\">";
                        break;
                }
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetRelativeUrl()
        {
            return AppDomain.CurrentDomain.BaseDirectory.ToString();
        }
        /// <summary>
        /// 加载Jquery插件
        /// </summary>
        /// <returns></returns>
        public static string LoadJqueryPlug(JqueryPlugType jPlug = JqueryPlugType.All)
        {
            var _result = string.Empty;
            //_result += "<script type=\"text/javascript\" src=\"" + "./Scripts/jquery-1.8.2.min\"></script>";
            _result += "<script src=\"./Scripts/Jquery/jquery.1.11.2.min.js\"></script>";
            switch (jPlug.ToString())
            {
                case "Boxer":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Boxer/jquery.fs.boxer.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Boxer/jquery.fs.boxer.min.css\" />";
                    break;
                case "Dropper":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Dropper/jquery.fs.dropper.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Dropper/jquery.fs.dropper.min.css\" />";
                    break;
                case "JqueryUi":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/jquery-ui-1.11.2/jquery-ui.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/jquery-ui-1.11.2/jquery-ui.min.css\" />";
                    break;
                case "Pager":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Picker/jquery.fs.picker.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Picker/jquery.fs.picker.min.css\" />";
                    break;
                case "Roller":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Roller/jquery.fs.roller.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Roller/jquery.fs.roller.min.css\" />";
                    break;
                case "Scroller":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Scroller/jquery.fs.scroller.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Scroller/jquery.fs.scroller.min.css\" />";
                    break;
                case "Selecter":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Selecter/jquery.fs.selecter.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Selecter/jquery.fs.selecter.min.css\" />";
                    break;
                case "serializeJSON":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/serializeJSON/jquery.fs.serializejson.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/serializeJSON/jquery.fs.serializejson.min.css\" />";
                    break;
                case "serializeObject":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/serializeObject/jquery.fs.serialize-object.min.js\"></script>";
                    break;
                case "Stepper":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Stepper/jquery.fs.stepper.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Stepper/jquery.fs.stepper.min.css\" />";
                    break;
                case "Tabber":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Tabber/jquery.fs.tabber.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Tabber/jquery.fs.tabber.min.css\" />";
                    break;
                case "Tipper":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Tipper/jquery.fs.tipper.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Tipper/jquery.fs.tipper.min.css\" />";
                    break;
                case "Validation":
                    _result += "<link rel=\"stylesheet\" href=\"./Scripts/Plug/Validation//css/validationEngine.jquery.css\" type=\"text/css\"/>";
                    _result += "<link rel=\"stylesheet\" href=\"./Scripts/Plug/Validation//css/template.css\" type=\"text/css\"/>";
                    _result += "<script src=\"./Scripts/Plug/Validation/js/jquery-1.8.2.min.js\" type=\"text/javascript\"></script>";
                    _result += "<script src=\"./Scripts/Plug/Validation/js/languages/jquery.validationEngine-en.js\" type=\"text/javascript\" charset=\"utf-8\"></script>";
                    _result += "<script src=\"./Scripts/Plug/Validation/js/jquery.validationEngine.js\" type=\"text/javascript\" charset=\"utf-8\"></script>";
                    break;
                case "Zoomer":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Zoomer/jquery.fs.zoomer.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Zoomer/jquery.fs.zoomer.min.css\" />";
                    break;
                case "Form":
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/jquery.form.js\"></script>";
                    break;
                case "All":
                default:
                    //Boxer
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Boxer/jquery.fs.boxer.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Boxer/jquery.fs.boxer.min.css\" />";
                    //Dropper
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Dropper/jquery.fs.dropper.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Dropper/jquery.fs.dropper.min.css\" />";
                    //jquery-ui-1.11.2
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/jquery-ui-1.11.2/jquery-ui.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/jquery-ui-1.11.2/jquery-ui.min.css\" />";
                    //Pager
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Pager/jquery.fs.pager.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Pager/jquery.fs.pager.min.css\" />";
                    //Picker
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Picker/jquery.fs.picker.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Picker/jquery.fs.picker.min.css\" />";
                    //Roller
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Roller/jquery.fs.roller.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Roller/jquery.fs.roller.min.css\" />";
                    //Scroller
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Scroller/jquery.fs.scroller.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Scroller/jquery.fs.scroller.min.css\" />";
                    //Selecter
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Selecter/jquery.fs.selecter.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Selecter/jquery.fs.selecter.min.css\" />";
                    //serializeJSON
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/serializeJSON/jquery.fs.serializejson.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/serializeJSON/jquery.fs.serializejson.min.css\" />";
                    //serializeObject
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/serializeObject/jquery.fs.serialize-object.min.js\"></script>";
                    //Stepper
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Stepper/jquery.fs.stepper.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Stepper/jquery.fs.stepper.min.css\" />";
                    //Tabber
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Tabber/jquery.fs.tabber.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Tabber/jquery.fs.tabber.min.css\" />";
                    //Tipper
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Tipper/jquery.fs.tipper.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Tipper/jquery.fs.tipper.min.css\" />";
                    //Validation
                    _result += "<link rel=\"stylesheet\" href=\"./Scripts/Plug/Validation//css/validationEngine.jquery.css\" type=\"text/css\"/>";
                    _result += "<link rel=\"stylesheet\" href=\"./Scripts/Plug/Validation//css/template.css\" type=\"text/css\"/>";
                    _result += "<script src=\"./Scripts/Plug/Validation/js/jquery-1.8.2.min.js\" type=\"text/javascript\"></script>";
                    _result += "<script src=\"./Scripts/Plug/Validation/js/languages/jquery.validationEngine-en.js\" type=\"text/javascript\" charset=\"utf-8\"></script>";
                    _result += "<script src=\"./Scripts/Plug/Validation/js/jquery.validationEngine.js\" type=\"text/javascript\" charset=\"utf-8\"></script>";
                    //Zoomer
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/Zoomer/jquery.fs.zoomer.min.js\"></script>";
                    _result += "<link rel=\"stylesheet\" href=\"" + "./Scripts/Plug/Zoomer/jquery.fs.zoomer.min.css\" />";
                    //Form
                    _result += "<script type=\"text/javascript\" src=\"" + "./Scripts/Plug/jquery.form.js\"></script>";
                    break;
            }
            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_res"></param>
        /// <param name="_rel"></param>
        /// <returns></returns>
        public static string LoadThemes(string _name, string[] _res, string _rel = "./")
        {
            var _path = string.IsNullOrEmpty(_name) ? (GetRelativeUrl() + "Themes/metronic/media/" + _name) : System.Web.HttpContext.Current.Server.MapPath(_name);
            var _r = _name.Split('/');
            var _repath = ".";
            for (var i = 0; i < _r.Length; i++)
            {
                if (i > 1)
                {
                    _repath += "/" + _r[i];
                }
            }
            var _result = string.Empty;

            if (_res.Length <= 0)
            {
                if (_res.Length <= 0 && !string.IsNullOrEmpty(_path))
                {
                    var _directory = Directory.GetDirectories(_path);

                    foreach (var item in _directory)
                    {
                        //获取文件
                        var files = Directory.GetFiles(item);

                        foreach (var _item in files)
                        {
                            var _index = _item.LastIndexOf("\\");

                            var _reset = string.Empty;
                            var _last = _item.LastIndexOf('.');
                            var _suffix = _item.Substring(_last + 1);
                            switch (_suffix)
                            {
                                case "js":
                                    _reset = _repath + "js/" + _item.Substring(_index + 1);
                                    _result += "<script type=\"text/javascript\" src=\"" + _reset + "\"></script>";
                                    break;
                                case "css":
                                    _reset = _repath + "css/" + _item.Substring(_index + 1);
                                    _result += "<link rel=\"stylesheet\" href=\"" + _reset + "\"/>";
                                    break;
                            }

                        }
                    }
                }
            }
            else
            {
                var _directory = Directory.GetDirectories(_path);

                foreach (var item in _directory)
                {
                    //获取文件
                    var files = Directory.GetFiles(item);

                    foreach (var _item in files)
                    {
                        var _index = _item.LastIndexOf("\\");
                        var _last = _item.LastIndexOf('.');
                        var _suffix = _item.Substring(_last + 1);
                        var _reset = string.Empty;
                        var _rename = _item.Substring(_item.LastIndexOf('\\') + 1);

                        foreach (var temp in _res)
                        {
                            if (temp.Equals(_rename))
                            {
                                _reset = _repath + _suffix + "/" + _item.Substring(_index + 1);
                                switch (_suffix)
                                {
                                    case "js":
                                        _result += "<script type=\"text/javascript\" src=\"" + _reset + "\"></script>";
                                        break;
                                    case "css":
                                        _reset = _repath + "css/" + _item.Substring(_index + 1);
                                        _result += "<link rel=\"stylesheet\" href=\"" + _reset + "\"/>";
                                        break;
                                }
                            }
                        }


                    }
                }
            }

            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jType"></param>
        /// <returns></returns>
        public static string LoadJquery(JqueryType jType = JqueryType.All)
        {
            var _type = jType.ToString();
            var _result = string.Empty;
            switch (_type)
            {
                case "Jquery_1_7_1":
                    _result += _getJquerySrc(_type);
                    break;
                case "Jquery_1_8_2":
                    _result += _getJquerySrc(_type);
                    break;
                case "Jquery_1_9_0":
                    _result += _getJquerySrc(_type);
                    break;
                case "Jquery_1_9_1":
                    _result += _getJquerySrc(_type);
                    break;
                case "Jquery_1_10_0":
                    _result += _getJquerySrc(_type);
                    break;
                case "Jquery_1_10_1":
                    _result += _getJquerySrc(_type);
                    break;
                case "Jquery_1_11_0":
                    _result += _getJquerySrc(_type);
                    break;
                case "Jquery_1_11_2":
                    _result += _getJquerySrc(_type);
                    break;
                case "":
                case "All":
                default:
                    var path = GetRelativeUrl() + "Scripts/Jquery/";
                    var files = Directory.GetFiles(path, "*.js");
                    foreach (var item in files)
                    {
                        string _item = item.ToString();
                        var index = _item.LastIndexOf("\\");
                        _result += "<script type=\"text/javascript\" src=\"" + _item.Substring(index + 1) + "\"></script>";
                    }
                    break;
            }

            return _result;
        }
        /// <summary>
        /// 获取Jquery相应版本文件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string _getJquerySrc(string type)
        {
            var _result = string.Empty;
            _result = type.ToLower();
            _result = _result.Replace("_", ".");
            _result = "<script type=\"text/javascript\" src=\"./Scripts/Jquery/" + _result.ToLower() + ".min.js\"></script>";
            return _result;
        }
        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="name">文件名</param>
        /// <returns></returns>
        private static string _type(object name)
        {
            var temp = name.ToString();
            var i = temp.LastIndexOf('.');
            var suffix = temp.Substring(i + 1);
            return suffix;
        }
    }
    /// <summary>
    /// Plug插件类型
    /// </summary>
    public enum JqueryPlugType
    {
        All,
        Boxer,
        Dropper,
        JqueryUi,
        Pager,
        Picker,
        Roller,
        Scroller,
        Selecter,
        serializeJSON,
        serializeObject,
        Stepper,
        Tabber,
        Tipper,
        Validation,
        Zoomer,
        Form
    }
    /// <summary>
    /// Jquery类型
    /// </summary>
    public enum JqueryType
    {
        All,
        Jquery_1_7_1,
        Jquery_1_8_2,
        Jquery_1_9_0,
        Jquery_1_9_1,
        Jquery_1_10_0,
        Jquery_1_10_1,
        Jquery_1_11_0,
        Jquery_1_11_2,
    }
}
