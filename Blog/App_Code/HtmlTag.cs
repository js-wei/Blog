using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.App_Code
{
    public class HtmlTag
    {
        #region 参数信息
        /// <summary>
        /// 
        /// </summary>
        public Helper _db = new Helper();
        /// <summary>
        /// htmlTag
        /// </summary>
        public string html = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

        [Category("设置")]
        [Description("获取或设置自定义控件的Title")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Title { get; set; }

        [Category("设置")]
        [Description("获取或设置自定义控件的文字。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text { get; set; }

        /// <summary>
        /// 获取或设置自定义控件的高
        /// </summary>
        public int height;
        [Category("设置")]
        [Description("获取或设置自定义控件的高")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Height
        {
            get { return height; }
            set { if (value == 0) { height = 0; } else { height = value; } }
        }
        /// <summary>
        /// 获取或设置自定义控件的宽
        /// </summary>
        public int width;
        [Category("设置")]
        [Description("获取或设置自定义控件的宽")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Width
        {
            get { return width; }
            set { if (value == 0) { width = 0; } else { width = value; } }
        }
        /// <summary>
        /// 获取或设置自定义控件的字体。
        /// </summary>
        public string font;
        [Category("设置")]
        [Description("设置自定义控件的字体。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Font
        {
            get { return font; }
            set { if (string.IsNullOrEmpty(value)) { value = "宋体"; } else { font = value; } }
        }
        /// <summary>
        /// 获取或设置自定义List控件的字体。
        /// </summary>
        public string fontSize;
        [Category("设置")]
        [Description("设置自定义控件的字体大小。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string FontSize
        {
            get { return font; }
            set { if (string.IsNullOrEmpty(value)) { value = "12px"; } else { fontSize = value; } }
        }
        public FontWeight fontWeightType;
        [Category("设置")]
        [Description("设置自定义控件的字体粗细")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FontWeight FontWeightType
        {
            get { return fontWeightType; }
            set { if (string.IsNullOrEmpty(value.ToString())) { value = FontWeight.normal; } else { fontWeightType = value; } }
        }
        /// <summary>
        /// 获取或设置自定义控件的字体样式。
        /// </summary>
        public FontStyle fontStyleType = FontStyle.normal;
        [Category("设置")]
        [Description("设置自定义控件的字体样式。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FontStyle FontStyleType
        {
            get { return fontStyleType; }
            set { if (string.IsNullOrEmpty(value.ToString())) { value = FontStyle.normal; } else { fontStyleType = value; } }
        }
        /// <summary>
        /// 获取或设置自定义控件的栏目ID
        /// </summary>
        public int columnId;
        [Category("控件的数据查询相关")]
        [Description("控件的栏目ID,获取相应的栏目数据。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        /// <summary>
        /// List控件的栏目查询条件
        /// </summary>
        public int ColumnId
        {
            get { return columnId; }
            set { columnId = value; }
        }
        /// <summary>
        /// 控件的数据查询相关
        /// </summary>
        public string condition;
        [Category("控件的数据查询相关")]
        [Description("控件的栏目查询条件。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        /// <summary>
        /// 控件的栏目查询排序
        /// </summary>
        public string orderBy;
        [Category("控件的数据查询相关")]
        [Description("控件的栏目查询排序。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string OrderBy
        {
            get { return orderBy; }
            set { orderBy = value; }
        }
        /// <summary>
        /// 控件的栏目查询分组
        /// </summary>
        public string groupBy;
        [Category("控件的数据查询相关")]
        [Description("控件的栏目查询分组。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string GroupBy
        {
            get { return orderBy; }
            set { groupBy = value; }
        }
        /// <summary>
        /// 控件的模型名称
        /// </summary>
        public string model;

        [Category("控件的数据查询相关")]
        [Description("控件的模型名称。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Model
        {
            get { return orderBy; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    model = "Article";
                }
                else
                {
                    model = value;
                }
            }
        }
        /// <summary>
        /// 控件的模型查询字段
        /// </summary>
        public string field;
        [Category("控件的数据查询相关")]
        [Description("控件的模型查询字段。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Field
        {
            get { return field; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    string temp = string.Empty;
                    var data = _db.GetModelFields(this.model);
                    foreach (var item in data)
                    {
                        temp += "," + item.ToString();
                    }
                    field = temp.Substring(1);
                }
                else
                {
                    field = value;
                }
            }
        }

        private string cssClassName;
        [Category("设置")]
        [Description("设置控件的CSS-Class名。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CssClassName
        {
            get { return cssClassName; }
            set { cssClassName = value; }
        }

        [Category("设置")]
        [Description("设置控件的CSS-ID名。")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CssClassId { get; set; }

        [Category("控件的数据查询相关")]
        [Description("设置控件的跳转地址")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string RedirectUrl { get; set; }

        [Category("控件的数据查询相关")]
        [Description("设置控件的模板")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Templet { get; set; }
        [Category("控件的数据查询相关")]
        [Description("设置控件的读取条数")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Count { get; set; }

        private string map;
        /// <summary>
        /// 获取查询条件
        /// </summary>
        [Category("控件的数据查询相关")]
        [Description("获取控件的查询条件")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Map
        {
            get { return map; }
            //set { map = value; }
        }
        private string _uri;

        public string Uri
        {
            get { return _uri; }
        }
        private string _css;

        public string Css
        {
            get { return _css; }
        }
        /// <summary>
        /// FontWeight样式
        /// </summary>
        public enum FontWeight
        {
            normal,
            bold,
            bolder
        }
        /// <summary>
        /// FontStyle样式
        /// </summary>
        public enum FontStyle
        {
            normal,
            italic,
            oblique
        }
        #endregion

        #region 构造函数
        public UserControlBase()
        {
            this.map = this.GetMap();
            this.model = this.Model;
            this._uri = HttpContext.Current.Request.Url.AbsolutePath;
            this._css = string.IsNullOrEmpty(this.CssClassName) ? "article-navigater" : this.CssClassName;
        }
        #endregion

        #region 创建标签
        public virtual string CreateHtmlTag(List<Dictionary<string, string>> _list)
        {
            var _html = string.Empty;
            var _resultTag = new string[_list.Count];
            var templet = this.Templet;                 //获取模板

            if (string.IsNullOrEmpty(templet))
            {
                _html = "<ul" + this.CreateStylesheet();
                var uri = string.IsNullOrEmpty(this.RedirectUrl) ? "" : this.RedirectUrl;
                var keys = this.field.Split(',');
                foreach (var item in _list)
                {
                    var values = item.Values;
                    _html += "<li><a href=\"" + uri + "?id=" + item[keys[0]] + "\">" + item[keys[1]] + "</a></li>";
                }
                _html += "</ul>";
            }
            else
            {
                _html = this.ReplaceTemplet(templet, _list);
            }
            return _html;
        }
        #endregion

        #region 替换模板
        /// <summary>
        /// 替换Html标签
        /// </summary>
        /// <param name="templet">模板</param>
        /// <param name="_list">List<Dictionary<string, string>>数据集合</param>
        /// <returns></returns>
        public string ReplaceTemplet(string templet, List<Dictionary<string, string>> _list)
        {
            string _templet = string.Empty;
            var i = 0;
            var _reg = this.GetValueWithSplit(templet, "{", "}");
            var temp = string.Empty;
            if (_list.Count > 0)
            {
                foreach (var item in _list)
                {
                    temp = templet;
                    for (var j = 0; j < _reg.Length; j++)
                    {
                        //拆分函数
                        var function = _reg[j].Split('|');

                        if (function.Length > 1)
                        {
                            //拆分参数
                            var _param = function[1].Split(',');
                            //不带参数，使用本身
                            if (_param.Length <= 1)
                            {
                                var fuc = string.Empty;
                                var _parameter = this.MakeParameter(_param, item[function[0]], out fuc);
                                var _result_item = string.Empty;
                                _result_item = this.CallbackByFunctionName(fuc, _parameter);
                                for (var k = 2; k < function.Length; k++)
                                {
                                    _parameter = this.MakeParameter(function, _result_item, out fuc, k);
                                    _result_item = this.CallbackByFunctionName(fuc, _parameter);
                                }
                                temp = temp.Replace("{" + _reg[j] + "}", _result_item);
                            }
                            else //带参数，###代表本身
                            {
                                var fuc = string.Empty;
                                var _parameter = this.MakeParameter(_param, item[function[0]], out fuc);
                                var _result_item = string.Empty;
                                _result_item = this.CallbackByFunctionName(fuc, _parameter);
                                temp = temp.Replace("{" + _reg[j] + "}", _result_item);
                            }
                        }
                        else
                        {
                            temp = temp.Replace("{" + _reg[j] + "}", item[_reg[j]]);
                        }
                    }
                    _templet += temp;
                    i++;
                }
            }
            else
            {
                _templet = "数据查询失败或数据不存在";
            }
            return _templet;
        }
        /// <summary>
        /// 替换Html标签
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="templet">模板</param>
        /// <param name="data">Dictionary<string, string>数据集合</param>
        /// <returns>,Dictionary<string,string> data</returns>
        public string ReplaceTemplet(string templet, Dictionary<string, string> _d)
        {
            string _templet = string.Empty;
            var _reg = this.GetValueWithSplit(templet, "{", "}");
            var temp = templet;
            if (_d.Count > 0)
            {
                for (var j = 0; j < _reg.Length; j++)
                {
                    var function = _reg[j].Split('|');
                    if (function.Length > 1)
                    {
                        var _result_item = string.Empty;
                        var _value = _d[function[0]];
                        _result_item = this.CallbackByFunctionName(function[1], _value);
                        for (var k = 2; k < function.Length; k++)
                        {
                            _result_item = this.CallbackByFunctionName(function[k], _result_item);
                        }
                        temp = temp.Replace("{" + _reg[j] + "}", _result_item);
                    }
                    else
                    {
                        temp = temp.Replace("{" + _reg[j] + "}", _d[_reg[j]]);
                    }
                }
                _templet += temp;
            }
            else
            {
                _templet = "数据查询失败或数据不存在";
            }
            return _templet;
        }
        /// <summary>
        /// 替换Html标签基类
        /// </summary>
        /// <param name="templet">模板</param>
        /// <param name="_list"> List<Dictionary<string, string>>数据集合</param>
        /// <returns></returns>
        public virtual string CreateHtmlTag(string templet, List<Dictionary<string, string>> _list)
        {
            string _templet = string.Empty;
            var i = 0;
            var _reg = this.GetValueWithSplit(templet, "{", "}");
            var temp = string.Empty;
            if (_list.Count > 0)
            {
                foreach (var item in _list)
                {
                    temp = templet;
                    for (var j = 0; j < _reg.Length; j++)
                    {
                        //拆分函数
                        var function = _reg[j].Split('|');

                        if (function.Length > 1)
                        {
                            //拆分参数
                            var _param = function[1].Split(',');
                            //不带参数，使用本身
                            if (_param.Length <= 1)
                            {
                                var fuc = string.Empty;
                                var _parameter = this.MakeParameter(_param, item[function[0]], out fuc);
                                var _result_item = string.Empty;
                                _result_item = this.CallbackByFunctionName(fuc, _parameter);
                                for (var k = 2; k < function.Length; k++)
                                {
                                    _parameter = this.MakeParameter(function, _result_item, out fuc, k);
                                    _result_item = this.CallbackByFunctionName(fuc, _parameter);
                                }
                                temp = temp.Replace("{" + _reg[j] + "}", _result_item);
                            }
                            else //带参数，###代表本身
                            {
                                var fuc = string.Empty;
                                var _parameter = this.MakeParameter(_param, item[function[0]], out fuc);
                                var _result_item = string.Empty;
                                _result_item = this.CallbackByFunctionName(fuc, _parameter);
                                temp = temp.Replace("{" + _reg[j] + "}", _result_item);
                            }
                        }
                        else
                        {
                            temp = temp.Replace("{" + _reg[j] + "}", item[_reg[j]]);
                        }
                    }
                    _templet += temp;
                    i++;
                }
            }
            else
            {
                _templet = "数据查询失败或数据不存在";
            }
            return _templet;
        }
        /// <summary>
        /// 制造参数
        /// </summary>
        /// <param name="_param">参数字符串</param>
        /// <param name="item">当前字段默认值</param>
        /// <returns></returns>
        private string[] MakeParameter(string[] _param, string item, out string fuc, int index = 0)
        {
            fuc = string.Empty;

            //处理参数
            var _p = _param[index].Split('=');
            if (_p.Length > 1)
            {
                var _parameters = _p[1].Split(',');
                var _placeholder = string.Equals(_parameters[_parameters.Length - 1], "###") ? item : "";
                var _tv = _p[1].Replace("'", "");
                fuc = _p[0];
                //动态参数添加
                var _relIndex = _parameters.Length;
                var _result = new string[_relIndex];
                _result[0] = _placeholder;
                var j = 1;
                if (_parameters.Length > 1)
                {
                    for (var i = 0; i < _parameters.Length - 1; i++)
                    {
                        _result[j] = _parameters[i].Replace("'", "");
                        j++;
                    }
                }
                else
                {
                    string[] _res = new string[2];
                    _res[1] = _parameters[0].Replace("'", "");
                    return _res;
                }
                return _result;
            }
            else
            {
                fuc = _p[0];
                string[] _result = new string[1];
                _result[0] = item;
                return _result;
            }
        }
        #endregion

        #region 创建控件样式属性
        public virtual string CreateStylesheet()
        {
            var _result = string.Empty;
            if (!string.IsNullOrEmpty(this.font) || !string.IsNullOrEmpty(this.fontSize) || this.width > 0 || this.height > 0)
            {
                _result = "style='";
                if (!string.IsNullOrEmpty(this.font))
                {
                    _result += "font-family:" + this.font + "';font-weight:'" + this.fontStyleType.ToString() + "';";
                }
                else if (!string.IsNullOrEmpty(this.font) && this.width > 0)
                {
                    _result += "font-family:" + this.font + "';font-weight:'" + this.fontStyleType.ToString() + "';";
                    _result += "width:'" + this.width + "px';";
                }
                else if (!string.IsNullOrEmpty(this.font) && this.width > 0 && this.height > 0)
                {
                    _result += "font-family:" + this.font + "';font-weight:'" + this.fontStyleType.ToString() + "';";
                    _result += "width:'" + this.width + "px';";
                    _result += "height:'" + this.height + "px';";
                }
                else if (!string.IsNullOrEmpty(this.font) && this.width > 0 && !string.IsNullOrEmpty(this.fontSize))
                {
                    _result += "font-family:" + this.font + "';font-weight:'" + this.fontStyleType.ToString() + "';";
                    _result += "width:'" + this.width + "px';";
                    _result += "font-size:'" + this.fontSize + "px';";
                }
                else if (!string.IsNullOrEmpty(this.font) && this.width > 0 && this.height > 0 && !string.IsNullOrEmpty(this.fontSize))
                {
                    _result += "font-family:" + this.font + "';font-weight:'" + this.fontStyleType.ToString() + "';";
                    _result += "width:'" + this.width + "px';";
                    _result += "height:'" + this.height + "px';";
                    _result += "font-size:'" + this.fontSize + "px';";
                }
                else if (this.width > 0)
                {
                    _result += "width:'" + this.width + "px';";
                }
                else if (this.height > 0)
                {
                    _result += "height:'" + this.height + "px';";
                }
                else if (!string.IsNullOrEmpty(this.fontSize))
                {
                    _result += "font-size:'" + this.fontSize + "px';";
                }
            }
            if (!string.IsNullOrEmpty(_result))
            {
                _result += "\"";
            }
            var stylesheet = _result;
            var _result1 = string.Empty;
            if (!string.IsNullOrEmpty(this.CssClassName) || !string.IsNullOrEmpty(this.CssClassId) || !string.IsNullOrEmpty(stylesheet))
            {
                if (!string.IsNullOrEmpty(this.CssClassName) && string.IsNullOrEmpty(this.CssClassId))
                {
                    _result1 = " class=\"" + this.CssClassName + "\">";
                }
                else if (!string.IsNullOrEmpty(this.CssClassId) && string.IsNullOrEmpty(this.CssClassName))
                {
                    _result1 = " id=\"" + this.CssClassId + "\" >";
                }
                else if (!string.IsNullOrEmpty(this.CssClassName) && !string.IsNullOrEmpty(stylesheet) && string.IsNullOrEmpty(this.CssClassId))
                {
                    _result1 = " class=\"" + this.CssClassName + "\" " + stylesheet + ">";
                }
                else if (!string.IsNullOrEmpty(this.CssClassId) && !string.IsNullOrEmpty(stylesheet) && string.IsNullOrEmpty(this.CssClassName))
                {
                    _result1 = " class=\"" + this.CssClassId + "\" " + stylesheet + ">";
                }
                else if (!string.IsNullOrEmpty(this.CssClassId) && string.IsNullOrEmpty(stylesheet) && !string.IsNullOrEmpty(this.CssClassName))
                {
                    _result1 = " id=\"" + this.CssClassId + "\" class=\"" + this.CssClassName + "\">";
                }
                else if (!string.IsNullOrEmpty(this.CssClassName) && !string.IsNullOrEmpty(this.CssClassId))
                {
                    _result1 = " class=\"" + this.CssClassName + "\" " + this.CssClassId + ">";
                }
                else if (!string.IsNullOrEmpty(stylesheet))
                {
                    _result1 = " id=\"" + this.CssClassId + "\" class=\"" + this.CssClassId + "\" " + stylesheet + ">";
                }
            }
            return _result1;
        }
        #endregion

        #region 获取数据
        public virtual string GetHtmlResult()
        {
            var _data_list = new DataTable();

            var _name = this.model;
            var _where = this.condition;
            var _order = this.orderBy;
            var _field = string.Empty;
            var _count = (this.Count > 0) ? this.Count.ToString() : "";

            if (string.IsNullOrEmpty(this.field) || string.Equals(this.field, "*"))
            {
                var path = AppDomain.CurrentDomain.BaseDirectory.ToString() + "/Cache/cache_" + _name + "_fields.txt";
                if (string.Equals(this.field, "*"))     //读缓存文件
                {
                    if (File.Exists(path))
                    {
                        // Open the stream and read it back.
                        using (StreamReader sr = File.OpenText(path))
                        {
                            var j = 0;
                            string s = string.Empty;
                            while ((s = sr.ReadLine()) != null)
                            {
                                if (j++ > 0)
                                {
                                    this.field = s;
                                }
                            }
                        }
                    }
                    else
                    {
                        //数据库查询获取字段
                        this.field = _db.GetModelFieldsWithoutType(_name);
                    }
                }
                else
                {
                    //数据库查询获取字段
                    this.field = _db.GetModelFieldsWithoutType(_name);
                }
                list = this.GetDataSource(_name, _count, _where, _order);
            }
            else
            {
                _field = this.field;
                list = this.GetDataSource(_name, _field, _count, _where, _order);
            }

            html = this.CreateHtmlTag(list);

            return html;
        }
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>List<string, string></returns>
        public virtual List<Dictionary<string, string>> GetDataSource(string _name, string _count, string _where, string _order)
        {
            var data = _db.GetDataTable(_name, "*", _count, _where, _order);

            foreach (DataRow dr in data.Rows)
            {
                var _d = new Dictionary<string, string>();
                foreach (var item in this.field.Split(','))
                {
                    _d.Add(item.ToString(), dr[item].ToString());
                }
                list.Add(_d);
            }
            return list;
        }
        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="fileds"></param>
        /// <param name="_count"></param>
        /// <param name="_where"></param>
        /// <param name="_order"></param>
        /// <returns></returns>
        public virtual List<Dictionary<string, string>> GetDataSource(string _name, string fileds, string _count, string _where, string _order)
        {
            var data = _db.GetDataTable(_name, fileds, _count, _where, _order);

            foreach (DataRow dr in data.Rows)
            {
                var _d = new Dictionary<string, string>();
                foreach (var item in this.field.Split(','))
                {
                    _d.Add(item.ToString(), dr[item].ToString());
                }
                list.Add(_d);
            }
            return list;
        }
        #endregion

        #region 获取起讫字符之间的值
        private string[] GetValueWithSplit(string str, string start, string end)
        {

            // 搜索匹配的字符串
            MatchCollection matches = Regex.Matches(str, @"(?<=(" + start + "))[.\\s\\S]*?(?=(" + end + "))", RegexOptions.IgnoreCase);
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
        #endregion

        #region 调用方法
        /// <summary>
        /// 根据方法命调用方法
        /// </summary>
        /// <param name="name">方法名</param>
        /// <returns></returns>
        private string CallbackByFunctionName(string name)
        {
            Type T = null;
            var func = new Functions();
            if (func != null)
            {
                var tool = new Functions();
                T = tool.GetType();
            }
            else
            {
                var tool = new Tool.Functions();
                T = tool.GetType();
            }
            //反射出类的实例
            object o = Activator.CreateInstance(T);
            //System.Reflection.PropertyInfo pi = T.GetProperty("xq_id");
            //获得方法信息
            System.Reflection.MethodInfo mi = T.GetMethod(name);
            string strTmp = mi.Invoke(o, null).ToString();
            return strTmp;
        }
        /// <summary>
        /// 根据方法命调用方法
        /// </summary>
        /// <param name="name">方法名</param>
        /// <param name="_paramer">string[]方法参数</param>
        /// <returns></returns>
        private string CallbackByFunctionName(string name, string[] _paramer)
        {
            Type T = null;
            var func = new Functions();
            if (func != null)
            {
                var tool = new Functions();
                T = tool.GetType();
            }
            else
            {
                var tool = new Tool.Functions();
                T = tool.GetType();
            }

            //反射出类的实例
            object o = Activator.CreateInstance(T);
            //System.Reflection.PropertyInfo pi = T.GetProperty("xq_id");
            //获得方法信息
            System.Reflection.MethodInfo mi = T.GetMethod(name);
            var param = mi.GetParameters();
            Object[] obj = new Object[param.Length];
            for (var i = 0; i < param.Length; i++)
            {
                if (i < _paramer.Length)
                {
                    obj[i] = _paramer[i];
                }

            }
            string strTmp = mi.Invoke(o, obj).ToString();
            return strTmp;
        }
        /// <summary>
        /// 根据方法命调用方法
        /// </summary>
        /// <param name="name">方法名</param>
        /// <param name="_paramer">string方法参数</param>
        /// <returns></returns>
        private string CallbackByFunctionName(string name, string _paramer)
        {
            Type T = null;
            var func = new Functions();
            if (func != null)
            {
                var tool = new Functions();
                T = tool.GetType();
            }
            else
            {
                var tool = new Tool.Functions();
                T = tool.GetType();
            }
            //反射出类的实例
            object o = Activator.CreateInstance(T);
            //System.Reflection.PropertyInfo pi = T.GetProperty("xq_id");
            //获得方法信息
            System.Reflection.MethodInfo mi = T.GetMethod(name);
            var param = mi.GetParameters();
            Object[] obj = new Object[] { _paramer };
            string strTmp = mi.Invoke(o, obj).ToString();
            return strTmp;
        }
        #endregion

        #region 获取查询条件
        private string GetMap()
        {
            string _result = string.Empty;
            var where = string.IsNullOrEmpty(this.Condition) ? "" : "where = " + this.Condition;
            var order = string.IsNullOrEmpty(this.orderBy) ? "" : "order by " + this.orderBy;
            _result = where + order;
            return _result;
        }
        #endregion

        #region 获取参数
        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetIdParamter(string name, int id = 1)
        {
            var _id = HttpContext.Current.Request[name];

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(_id))
            {
                id = Convert.ToInt32(_id);
            }
            return id;
        }
        #endregion
    }
}