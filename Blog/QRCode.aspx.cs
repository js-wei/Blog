using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tool;

namespace Blog
{
    public partial class QRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var _result = Tool.QRCode.Create(TextBox1.Text.ToString().Trim(), 8, "", true);
            //Response.Write(_result);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //var _result = Tool.QRCode.Recognition();
            //_CombinImage();
            //Response.Write(_result);
        }
        /// <summary>
        /// 组合二维码
        /// </summary>
        private void _CombinImage()
        {
            var path = System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") +
                "\\a1e6a795-c10e-42be-8b2a-aa3bbd8d0047.png";    //favicon.ico
            var bgpath = System.Web.HttpContext.Current.Server.MapPath(@"~\QRCode\upload") + "\\favicon.png";    //
            var bg = System.Drawing.Image.FromFile(path);
            var QRCodeStr = string.Empty;
            Tool.QRCode.CombinImage(bg, bgpath, out QRCodeStr);
            Image1.ImageUrl = QRCodeStr;
        }
    }
}