using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Blog.server
{
    /// <summary>
    /// 此页面用来协助 IE6/7 预览图片，因为 IE 6/7 不支持 base64
    /// 原理是 将图片保存到缓存文件夹中，在返回显示目录
    /// </summary>
    public class preview : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string _dir = "preview";
            // Create target dir
            if (!Directory.Exists(_dir))
            {
                Directory.CreateDirectory(_dir);
            }

            bool cleanupTargetDir = true; // Remove old files
            int maxFileAge = 5 * 3600; // Temp file age in seconds

            if (cleanupTargetDir)
            {
                if (!Directory.Exists(_dir))
                {
                    context.Response.Write("{\"jsonrpc\" : \"2.0\", \"error\" : {\"code\": 100,\"message\": \"Failed to open temp directory.\"}, \"id\" : \"id\"}");
                }

                // Remove temp file if it is older than the max age and is not the current file
                context.Response.Write(context.Request.Files.Count);
            }
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