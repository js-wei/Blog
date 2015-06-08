using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.UserControl
{
    public partial class Files : UserControl.UserControlBase
    {
        protected string _html = string.Empty;
        private bool isShowDefualt;
        /// <summary>
        /// 显示一般模板
        /// </summary>
        public bool IsShowDefualt
        {
            set { isShowDefualt = value; }
        }
        private string resultTage;
        /// <summary>
        /// 结果标签
        /// </summary>
        public string ResultTag
        {
            get { return resultTage; }
            //set { navigeter = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.resultTage = _html = this.CreateHtmlTag();
        }

        /// <summary>
        /// 创建标签
        /// </summary>
        /// <returns></returns>
        private string CreateHtmlTag()
        {
            var _result = string.Empty;
            var _count = string.IsNullOrEmpty(this.Count.ToString()) ? "" : this.Count.ToString();
            var data = this._db.GetData(this.model, Convert.ToInt32(_count), this.Map);
            var templet = this.Templet;

            if (!string.IsNullOrEmpty(templet) && !isShowDefualt)
            {
                _result = this.ReplaceTemplet(templet, data);
            }
            else
            {
                if (isShowDefualt)
                {
                    _result = this.CreateDefaultHtmlTag(data);
                }
                else
                {
                    _result = "请填写模板或使用默认模板请将IsShowDefualt为true";
                }
            }

            return _result;
        }
        /// <summary>
        /// 创建默认Html标签
        /// </summary>
        /// <param name="_d"></param>
        /// <returns></returns>
        private string CreateDefaultHtmlTag(List<Dictionary<string, string>> _d)
        {
            //样式
            var _css = this.CreateStylesheet();
            var url = System.Web.HttpContext.Current.Request.Url;
            var host = "http://" + url.Authority;
            var _redirect = string.IsNullOrEmpty(this.RedirectUrl) ? host : this.RedirectUrl;

            var _result = string.Empty;
            if (_d.Count > 0)
            {
                _result = string.IsNullOrEmpty(_css) ? "<ul" + _css + ">" : "<ul" + _css;
                foreach (var item in _d)
                {
                    _result += "<li><a href=\"" + _redirect + "/" + item["path"] + "\">" + item["rename"] + "</a></li>";
                }
                _result += "</ul>";
            }
            else
            {
                _result = "数据读取失败或数据不存在";
            }

            return _result;
        }

        public override string CreateStylesheet()
        {
            return base.CreateStylesheet();
        }
    }
}