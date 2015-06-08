using DBhelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;

namespace Blog.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected string _re = string.Empty;
        private Helper db = new Helper();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var admin = new Model.Admin() { username = "admin", password = "E10ADC3949BA59ABBE56E057F20F883E", email = "524314430@qq.com", address = "江苏省徐州市", tel = "18550431182", login_time = DateTime.Now.ConvertToUnix().ToString() };
                //if (db.Insert<Model.Admin>(admin))
                //{
                //    Response.Write("OK");
                //}
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            var _name = username.Value;
            var _pwd = password.Value;
            var _remember = CheckBox1.Checked;
            var user = db.Find("admin", "username= '" + _name + "'");
            if (string.IsNullOrEmpty(_name))
            {
                Response.Write("<script>alert('账号不能为空');</script>");
                return;
            }
            if (user.Count > 0)
            {
                if (user["password"] == _pwd.Trim().ToMD5())
                {
                    if (_remember)
                    {
                        db.Update<Blog.Model.Admin>(new Blog.Model.Admin() { remember = 1, id = Convert.ToInt32(user["id"]) });
                    }
                    else
                    {
                        db.Update<Blog.Model.Admin>(new Blog.Model.Admin() { remember = 0, id = Convert.ToInt32(user["id"]) });
                    }
                    //
                    Session["aid"] = user["id"];
                    Session["aname"] = user["username"];
                    Response.Redirect("~/Admin/Default.aspx");
                }
                else
                {
                    Response.Write("<script>alert('密码错误，请重新输入');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('账号不存在');</script>");
            }
        }
    }
}