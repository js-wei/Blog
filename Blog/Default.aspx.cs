using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBhelper;
using Tool;
using System.Text.RegularExpressions;
using System.IO;

namespace Blog
{
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// 分页类
        /// </summary>
        protected Pager pg;
        /// <summary>
        /// 分数据列表
        /// </summary>
        // protected List<Model.Content> lists = new List<Model.Content>();
        /// <summary>
        /// 栏目ID
        /// </summary>
        protected string column_id = string.Empty;
        /// <summary>
        /// 资源
        /// </summary>
        protected string res = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var _bad = new Tool.BadWordParse();
                //var _name = _bad.HasBadWord("色情小電影");

                //Response.Write(_bad.ReplaceBadWord("我喜欢色情小電影的演员苍井空www.52wan.hk"));

                //var _xmlDoc = new XmlOperate();
                //Response.Write(_xmlDoc.ReaderXml());

                var _res = new string[] { "common.css" };
                res = Config.LoadRelativeResource(_res) + Config.LoadJquery(JqueryType.Jquery_1_7_1);
                //var _result = new string[] { "魏巍是个好人", "魏巍是我", "我就是魏巍" };
                //var temp = "魏巍是个好人|魏巍是我|我就是魏巍|".ConvertToArray("|");
                //foreach (var item in temp)
                //{
                //    Response.Write(item + "<br/>");
                //}
                //Response.Write(_result.ConvertToString("|")+"</br>");
                //var _r = Tool.QRCode.Create("www.baidu.com", 12);
                //Response.Write(_r);

                //var _db = new Helper();
                /* //for (var i = 0; i < 50; i++)
                //{
                //    var p = new Model.Content() { name = "12345" + i + ".mp3", content = "我是一个兵来自老百姓", path = "./Convert/12345" + i + ".mp3", timespan = DateTime.Now.ConvertToUnix(), expire = DateTime.Now.ConvertToUnix() + 10 * 60 };
                //    _db.Insert<Model.Content>(p);
                //}
                var index = Convert.ToInt32(Request["p"]) > 1 ? Convert.ToInt32(Request["p"]) : 1;
                var list = new PagerDataList();
                //分页配置
                var page = new Pager() { CurPage = index, IsShowGoto = true, IsShowDetails = false, PageSize = 5 };
                //查询条件
                var where = new Where();
                pg = list.Query<Model.Content>(page, where);
                foreach (Model.Content it in pg.PageList)
                {
                    lists.Add(it);
                }
                //加载资源文件
                var _res = new string[] { "common.css" };
                res = Config.LoadRelativeResource(_res) + Config.LoadJquery(JqueryType.Jquery_1_11_2);*/

                /*
                var content = new Model.Content() { id = 5, content = "这是一个无休止的战争", expire = DateTime.Now.ConvertToUnix() + 60 * 24, name = "翻译", path = "./Convert/12345/123456.mp3", timespan = DateTime.Now.ConvertToUnix() };
                var data = content.ConvertToDictionary<Model.Content>();
                var flag = _db.Update("Content", data);
                if(flag){
                    Response.Write("更新成功");
                }
                else
                {
                    Response.Write("更新失败");
                }
                 */
                //var insert = new List<Model.File>();
                //insert.Add(new Model.File() { name = "1", rename = "1234.mp4", description = "1234.mp4", ico = "", id = 1, path = "", size = "", sort = 0, status = 0, timespan = DateTime.Now.ConvertToUnix(), title = "", type = "mp4" });
                //insert.Add(new Model.File() { name = "2", rename = "6789.mp3", description = "1234.mp4", ico = "", id = 2, path = "", size = "", sort = 0, status = 0, timespan = DateTime.Now.ConvertToUnix(), title = "", type = "mp3" });
                ////var data = insert.ConvertToListDictionary<Model.File>();
                //var db = new Helper();

                //if (db.Insert<Model.File>(insert))
                //{
                //    Response.Write("插入成功");
                //}
                //else
                //{
                //    Response.Write("插入失败");
                //}
                //var insert = new List<Model.Test>();
                //insert.Add(new Model.Test() { id = 1, name = "这是一个测试的主键不是自增的例子" });
                //insert.Add(new Model.Test() { id = 2, name = "这是一个测试的主键不是自增的例子2" });
                //var db = new Helper() { AutoModel = true };
                //var data = new Model.File() { id = 1, name = "test.mp3", ico="./Images/123456.ico" };
                //if (db.Update("file",data.ConvertToDictionary<Model.File>()))
                //{
                //    Response.Write("更新成功");
                //}
                //else
                //{
                //    Response.Write("更新失败");
                //}
                //var str = "用户名'user500'不存在";
                //Response.Write(str.ChinessConvertToUnicodeString());


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //var upload = new UploadFile() { AutoSub = true, AutoSubType = AutoSubType.date, IsRename = true, path = "~/UploadFile", size = 50 };
            //var msg = string.Empty;
            //var data = upload.SaveFile(FileUpload1, ref msg);
            //var _insert = upload.GetFileInfo[0];
            //var db = new Helper();
            //if (string.Equals(_insert["status"], "1"))
            //{
            //    if (db.Insert("file", _insert))
            //    {
            //        Response.Write(data);
            //    }
            //    else
            //    {
            //        Response.Write("添加失败");
            //    }
            //}
            //else
            //{
            //    Response.Write(_insert["messge"]);
            //}



            //var insert = new List<Model.File>();
            //insert.Add(new Model.File() { name = "test", rename = "test", description = "", ico = "", id = 4, path = "", size = "", sort = 0, status = 0, timespan = 14566777, title = "", type = "" });
            //insert.Add(new Model.File() { name = "test1", rename = "test1", description = "", ico = "", id = 6, path = "", size = "", sort = 0, status = 0, timespan = 14566777, title = "", type = "" });
            //var data = insert.ConvertToListDictionary<Model.File>();
            //var db = new Helper();

            //if (db.Insert("file", data))
            //{
            //    Response.Write("添加成功");
            //}
            //else
            //{
            //    Response.Write("添加失败");
            //}

        }
    }
}