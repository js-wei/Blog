using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.Admin.UserControl
{
    public partial class DataTable : System.Web.UI.UserControl
    {
        protected string _title=string.Empty;
        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        public DataTable()
        {
            _title=this.title;
        }
        /// <summary>
        /// 数据主题
        /// </summary>
        private string title;

        public string Title
        {
            set { if (!string.IsNullOrEmpty(value)) { title = value; } else { title = "表标题"; } }
        }
        /// <summary>
        /// 数据栏目名称
        /// </summary>
        public List<Dictionary<string, string>> Conlumm { get; set; }
        /// <summary>
        /// 数据源
        /// </summary>
        public List<Dictionary<string,string>> DataSorce { get; set; }


    }
}