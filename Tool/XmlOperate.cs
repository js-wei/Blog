using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tool
{
    /// <summary>
    /// Xml操作
    /// </summary>
    public class XmlOperate
    {
        /// <summary>
        /// Xml地址
        /// </summary>
        private string _path;
        public string Path
        {
            //get { return _path; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this._path = System.Web.HttpContext.Current.Server.MapPath("~/Data/dirty_words_config.xml");
                }
                else
                {
                    if (value.ToString().IndexOf("~") >= 0)
                    {
                        this._path = System.Web.HttpContext.Current.Server.MapPath(value);
                    }
                    else
                    {
                        _path = value;
                    }

                }
            }
        }
        /// <summary>
        /// Xml对象
        /// </summary>
        private XmlDocument _xml = new XmlDocument();
        public XmlOperate()
        {
            this.Init();
        }
        public void Init()
        {
            this._path = System.Web.HttpContext.Current.Server.MapPath("~/Data/dirty_words_config.xml");
        }
        public string ReaderXml()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(this._path, settings);
            _xml.Load(reader);

            var str = ReadNode(_xml.ChildNodes);
            reader.Close();
            return "";
        }

        private List<Dictionary<string, string>> ReadNode(XmlNodeList _XmlNodeList)
        {
            var _attrs = new List<Dictionary<string,string>>();

            for (var i = 0; i < _XmlNodeList.Count; i++)
            {
                if (_XmlNodeList.Item(0).InnerText.IndexOf("?>") < 0)
                {
                    //var _attr = _XmlNodeList.Item(i).Attributes;
                    var _nodes = _XmlNodeList.Item(i).ChildNodes;

                    var _children = _XmlNodeList.Item(i).ChildNodes;

                    if (_children.Count>0)
                    {
                        for (var ii = 0; ii < _nodes.Count; ii++)
                        {

                            if (_nodes.Item(ii).ChildNodes.Count > 0)
                            {
                                this.ReadNode(_nodes.Item(ii).ChildNodes);
                            }
                            else
                            {
                                var _attr = _nodes.Item(ii).Attributes;
                                var _d = new Dictionary<string, string>();
                                for (var j = 0; j < _attr.Count; j++)
                                {
                                    _d.Clear();
                                    _d.Add("InnerText_" + _nodes.Item(ii).LocalName, _nodes.Item(ii).InnerText);
                                    _d.Add("Attributes_" + _attr.Item(j).Name, _attr.Item(j).InnerText);

                                }
                                _attrs.Add(_d);
                            }
                        }
                    }
                    else
                    {
                        var _attr = _nodes.Item(i).Attributes;
                        var _d = new Dictionary<string, string>();
                        for (var j = 0; j < _attr.Count; j++)
                        {
                            _d.Clear();
                            _d.Add("InnerText_" + _nodes.Item(i).LocalName, _nodes.Item(i).InnerText);
                            _d.Add("Attributes_" + _attr.Item(j).Name, _attr.Item(j).InnerText);

                        }
                        _attrs.Add(_d);
                    }
                    

                }
            }
            return _attrs;
        }

        public void ReaderXmlByIO()
        {
            //注意System.Text.Encoding.Default
            System.IO.StreamReader myFile = new System.IO.StreamReader(this._path, System.Text.Encoding.Default);
            string myString = myFile.ReadToEnd();//myString是读出的字符串
            myString = myString.Substring(myString.LastIndexOf("?>") + 2).Replace("\n", "").Replace("\t", "").Replace("\r", "");
            myFile.Close();
        }
    }

}
