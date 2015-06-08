using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.UserControl
{
    public partial class Article : UserControl.UserControlBase
    {
        protected string style = string.Empty;
        protected new string html = string.Empty;

        #region 参数信息
        private string navigeter;
        /// <summary>
        /// 获取上一条下一条地址
        /// </summary>
        public string Navigeter
        {
            get { return navigeter; }
            //set { navigeter = value; }
        }
        private string resultTag;
        /// <summary>
        /// 结果标签
        /// </summary>
        public string ResultTag
        {
            get { return resultTag; }
            //set { navigeter = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            this.navigeter = this.GetNavigater();
            this.resultTag = html = this.CreateHtml();
        }

        /// <summary>
        /// 创建HTML标签
        /// </summary>
        /// <returns></returns>
        private string CreateHtml()
        {
            var _html = string.Empty;
            var id = this.GetArticleId("id");
            var _templet = this.Templet;
            var _mod = !string.IsNullOrEmpty(this.model) ? this.model : "Article";
            var data = this._db.Find(this.model, id);

            if (data.Count > 0)
            {
                style = this.CreateStylesheet();
                if (!string.IsNullOrEmpty(_templet))
                {
                    _html = this.ReplaceTemplet(_templet, data);
                }
                else
                {
                    _html += "";
                }
            }
            return _html;
        }
        /// <summary>
        /// 获取导航链接
        /// </summary>
        /// <returns></returns>
        private string GetNavigater()
        {
            var _result = string.Empty;
            var id = this.GetArticleId("id");
            var uri = HttpContext.Current.Request.Url.AbsolutePath;
            var pre = this._db.GetPreArticle(this.model, id, this.Map);
            var next = this._db.GetNextArticle(this.model, id, this.Map);
            var style = string.IsNullOrEmpty(this.CssClassName) ? "article-navigater" : this.CssClassName;
            _result += "<div class=\"" + style + "\"><ul>";
            if (pre.Count > 0 || next.Count > 0)
            {
                if (pre.Count > 0)
                {
                    _result += "<li><a href=\"" + uri + "?id=" + pre["id"] + "\">" + pre["name"] + "</a></li>";
                }
                else
                {
                    _result += "<li><a href=\"javascript:void(0);\">没有了</a></li>";
                }
                if (next.Count > 0)
                {
                    _result += "<li><a href=\"" + uri + "?id=" + next["id"] + "\">" + next["name"] + "</a></li>";
                }
                else
                {
                    _result += "<li><a href=\"javascript:void(0);\">没有了</a></li>";
                }
            }
            _result += "</ul></div>";
            return _result;
        }
        /// <summary>
        /// 获取ArticleID
        /// </summary>
        /// <returns></returns>
        private int GetArticleId(string name = "", int id = 1)
        {
            var _id = HttpContext.Current.Request[name];

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(_id))
            {
                id = Convert.ToInt32(_id);
            }
            return id;
        }
    }
}