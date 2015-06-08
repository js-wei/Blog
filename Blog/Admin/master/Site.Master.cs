﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Blog.Admin.master
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["aid"] == null || HttpContext.Current.Session["aname"] == null || string.IsNullOrEmpty(HttpContext.Current.Session["aid"].ToString()) || string.IsNullOrEmpty(HttpContext.Current.Session["aname"].ToString()))
                {
                    //Response.Redirect("~/Admin/Login.aspx");
                }
            }
        }
    }
}