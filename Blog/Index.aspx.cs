using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog
{
    public partial class Index : System.Web.UI.Page
    {
        public List<Model.Ad> ad = new List<Model.Ad>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            for (var i = 1; i <= 10; i++)
            {
                ad.Add(new Model.Ad() { id = i, name = "name" + i });
            }
        }
    }
}