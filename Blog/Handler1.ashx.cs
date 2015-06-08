using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var id = context.Request["id"].ToString();

            //模拟Json格式，或者是用Newtonsoft.Json;的dll转成Json数据

            var json = "{ \"status\": 1, \"msg\":\"you click at :" + id + "\"}";
            context.Response.Write(json);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}