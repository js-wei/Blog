using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.UserControl
{
    #region 广告处理类
    public partial class Banner : UserControl.UserControlBase  //: System.Web.UI.UserControl
    {
        protected string _html = string.Empty;
        protected bool _isShow = false;
        protected bool _isfloat = false;
        protected bool _iscouplet = false;
        protected string _color = string.Empty;
        protected string _bgcolor = string.Empty;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.say();
            _html = this.GetHtmlResult();
        }
        #endregion

        private int count;

        public new int Count
        {
            set { count = value; }
        }
        private int leftId;
        public int LeftId
        {
            set { leftId = value; }
        }
        private int rightId;
        public int RightId
        {
            set { RightId = value; }
        }
        private string circleColor;

        public string CircleColor
        {
            set { circleColor = value; }
        }
        private string circleBgColor;

        public string CircleBgColor
        {
            set { circleBgColor = value; }
        }
        private string timespan;

        public string Timespan
        {
            set { timespan = value; }
        }
        private string speed;

        public string Speed
        {
            set { speed = value; }
        }
        private bool isCircle;

        public bool IsCircle
        {
            set { isCircle = value; }
        }
        private bool isAuto;
        public bool IsAuto
        {
            set { isAuto = value; }
        }
        private AdType adType = AdType.Normal;
        public AdType AdType
        {
            set { adType = value; }
        }
     
        public override string GetHtmlResult()
        {
            var result = string.Empty;
            var _model = string.IsNullOrEmpty(this.model) ? "ad" : this.model;
            var _count = 0;

            if (this.count == 0)
            {
                _count = 2;
            }
            else
            {
                _count = this.count;
            }
            var _where = "where type=" + (int)this.adType;
            if (this.columnId > 0)
            {
                _where += " and columnId = " + this.columnId;
            }
            if (this.leftId > 0 || this.rightId > 0)
            {
                if (this.leftId > 0)
                {
                    _where += " and id = " + this.leftId;
                }
                else if (this.rightId > 0)
                {
                    _where += " and id = " + this.rightId;
                }
                else
                {
                    _where += " and id in (" + this.leftId + "," + this.rightId + ")";
                }
            }
            _where += string.IsNullOrEmpty(this.condition) ? "" : " and " + this.condition;


            var _templet = this.Templet;
            var data = this._db.GetData(_model, _count, _where);

            switch (this.adType.ToString().ToLower())
            {
                case "banner":
                    result = this.CreateBannerHtml(_templet, data);
                    break;
                case "float":
                    result = this.CreateFloatHtml(_templet, data);
                    break;
                case "couplet":
                    result = this.CreateCoupletHtml(_templet, data);
                    break;
                case "normal":
                default:
                    result = this.CreateNormalHtml(_templet, data);
                    break;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string CreateStylesheet()
        {
            return base.CreateStylesheet();
        }
        /// <summary>
        /// 创建Banner
        /// </summary>
        /// <param name="_templet"></param>
        /// <param name="_d"></param>
        /// <returns></returns>
        private string CreateBannerHtml(string _templet, List<Dictionary<string, string>> _d)
        {
            var _result = string.Empty;
            _color = string.IsNullOrEmpty(this.circleColor) ? "#808080" : this.circleColor;
            _bgcolor = string.IsNullOrEmpty(this.circleBgColor) ? "orange" : this.circleBgColor;
            var _timespan = string.IsNullOrEmpty(this.timespan) ? "2000" : this.timespan;
            var _speed = string.IsNullOrEmpty(this.speed) ? "0" : this.speed;
            var _isCircle = this.isCircle ? "false" : "true";
            var _isAuto = this.isAuto ? "false" : "true";
            var _width = this.width;
            var _height = this.height;
            if (_width == 0 || _height == 0)
            {
                if (_width == 0)
                {
                    _width = 500;
                }
                if (_height == 0)
                {
                    _height = 200;
                }
            }

            if (!string.IsNullOrEmpty(_templet))
            {
                _result = this.ReplaceTemplet(_templet, _d);
            }
            else
            {
                _isShow = true;

                _result += "<div class=\"banner-container\"><div class=\"banner-image\"><ul class=\"banner-image-content\">";

                if (_d.Count > 0)
                {
                    foreach (var item in _d)
                    {
                        _result += "<li><img src=\"./Images/upfiles/" + item["savename"] + "\" width=\"" + _width + "\" height=\"" + _height + "\" /></li>";
                    }
                }
                else
                {
                    _result += "<li><img src=\"./Images/upfiles/5204630c4cf16.jpg\" width=\"" + _width + "\" height=\"" + _height + "\" /></li>";
                    _result += "<li><img src=\"./Images/upfiles/522d316a06132.jpg\" width=\"" + _width + "\" height=\"" + _height + "\" /></li>";
                }
                _result += "</ul></div></div>";

            }
            _result += "<script type=\"text/javascript\">";
            _result += "$(function(){$(\".banner-container\").BannerBox({auto:true,point:" + _isCircle + ",speed:" + _speed + ",timespan:" + _timespan + "});";
            _result += "});</script>";

            return _result;
        }
        /// <summary>
        /// 创建漂浮广告
        /// </summary>
        /// <param name="_templet"></param>
        /// <param name="_d"></param>
        /// <returns></returns>
        private string CreateFloatHtml(string _templet, List<Dictionary<string, string>> _d)
        {
            var _result = string.Empty;

            var _width = this.width;
            var _height = this.height;
            if (_width == 0 || _height == 0)
            {
                if (_width == 0)
                {
                    _width = 220;
                }
                if (_height == 0)
                {
                    _height = 100;
                }
            }

            if (!string.IsNullOrEmpty(_templet))
            {
                _result = this.ReplaceTemplet(_templet, _d);
            }
            else
            {
                _isfloat = true;

                _result += "<script type=\"text/javascript\">";
                _result += "  var ad=new ad();";
                if (_d.Count > 0)
                {
                    foreach (var item in _d)
                    {
                        _result += "ad.addItem('<a href=\"http://" + item["url"] + "\" target=\"_blank\"><img src=\"./Images/upfiles/" + item["savename"] + "\" width=\"" + _width + "\" height=\"" + _height + "\" border=\"0\"></a>');";
                    }
                }
                else
                {
                    _result += "ad.addItem('<a href=\"http://\" target=\"_blank\"><img src=\"./Images/upfiles/522d316a06132.jpg\" width=\"" + _width + "\" height=\"" + _height + "\" border=\"0\"></a>');";
                }

            }
            _result += "ad.play();";
            _result += "</script>";

            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_templet"></param>
        /// <param name="_d"></param>
        /// <returns></returns>
        private string CreateCoupletHtml(string _templet, List<Dictionary<string, string>> _d)
        {
            var _result = string.Empty;

            var _width = this.width;
            var _height = this.height;
            if (_width == 0 || _height == 0)
            {
                if (_width == 0)
                {
                    _width = 120;
                }
                if (_height == 0)
                {
                    _height = 80;
                }
            }

            if (!string.IsNullOrEmpty(_templet))
            {
                _result = this.ReplaceTemplet(_templet, _d);
            }
            else
            {
                _iscouplet = true;

                _result += "<script type=\"text/javascript\">";
                _result += "var theFloaters = new floaters();";
                if (_d.Count > 0)
                {
                    foreach (var item in _d)
                    {
                        if (Convert.ToInt32(item["id"]) == this.leftId)
                        {
                            _result += "theFloaters.addItem('followDiv1', \"document.body.clientWidth-250\"," + _width + ",'<a style=\"display:block;\" href=\"\" target=\"_blank\"><img src=\"/91.com/Public/Uploads/5425458d0ce66.jpg\" width=\"" + _width + "\" height=\"120\" border=\"0\"></a>');";

                        }
                        else
                        {
                            _result += "theFloaters.addItem(\"followDiv2\", 20, " + _width + ", '<a style=\"display:block;\" href=\"http://\" target=\"_blank\"><img src=\"/91.com/Public/Uploads/5425457ac8f96.jpg\" width=\"" + _width + "\" height=\"120\" border=\"0\"></a>');";
                        }
                    }
                }
                else
                {
                    _result += "theFloaters.addItem('followDiv1', \"document.body.clientWidth-" + _width + "\"," + _width + ",'<a style=\"display:block;\" href=\"\" target=\"_blank\"><img src=\"/91.com/Public/Uploads/5425458d0ce66.jpg\" width=\"" + _width + "\" height=\"120\" border=\"0\"></a>');";
                    _result += "theFloaters.addItem(\"followDiv2\", 20, " + _width + ", '<a style=\"display:block;\" href=\"http://\" target=\"_blank\"><img src=\"/91.com/Public/Uploads/5425457ac8f96.jpg\" width=\"" + _width + "\" height=\"120\" border=\"0\"></a>');";
                }

            }
            _result += "theFloaters.play();";
            _result += "</script>";

            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_templet"></param>
        /// <param name="_d"></param>
        /// <returns></returns>
        private string CreateNormalHtml(string _templet, List<Dictionary<string, string>> _d)
        {
            var _result = string.Empty;

            var _width = this.width;
            var _height = this.height;
            if (_width == 0 || _height == 0)
            {
                if (_width == 0)
                {
                    _width = 120;
                }
                if (_height == 0)
                {
                    _height = 80;
                }
            }

            if (!string.IsNullOrEmpty(_templet))
            {
                _result = this.ReplaceTemplet(_templet, _d);
            }
            else
            {
                if (_d.Count > 0)
                {
                    foreach (var item in _d)
                    {
                        _result += "<img src=\"" + item["savename"] + "\" width=\"" + _width + "\" height=\"" + _height + "\"" + "/>";
                    }
                }
            }
            return _result;
        }
    }
    #endregion

    #region 广告枚举类
    //广告类型
    public enum AdType
    {
        /// <summary>
        /// 一般列表
        /// </summary>
        Normal = 0,
        /// <summary>
        /// banner滚动
        /// </summary>
        Banner = 1,
        /// <summary>
        /// 漂浮广告
        /// </summary>
        Float = 2,
        /// <summary>
        /// 对联广告
        /// </summary>
        Couplet = 3
    }
    #endregion
}