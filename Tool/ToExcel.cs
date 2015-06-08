using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace Tool
{
    /// <summary>
    /// excel数据导入导出
    /// </summary>
    public class ToExcel
    {
        public void DataSetToExcel(DataTable dt, string colNames)
        {
            string[] colname = colNames.Split(new char[] { ';' });

            HttpResponse resp;
            resp = HttpContext.Current.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            resp.ContentType = "application/ms-excel";
            string colHeaders = "", ls_item = "";

            //定义表对象与行对象，同时用DataSet对其值进行初始化 
            //DataTable dt = ds.Tables[0];
            //DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            if (dt.IsInitialized && dt.Rows.Count!=0)
            {
                int cl = dt.Columns.Count;
            }
 

            //取得数据表各列标题，各标题之间以/t分割，最后一个列标题后加回车符 
            for (i = 0; i <= colname.Length - 1; i++)
            {
                if (i == (colname.Length - 1))//最后一列，加/n
                {
                    colHeaders += colname[i] + "\n";
                }
                else
                {
                    colHeaders += colname[i] + "\t";
                }

            }

            resp.Write(colHeaders);
            //向HTTP输出流中写入取得的数据信息 

            //逐行处理数据
            for (int n = 0; n < dt.Rows.Count; n++)
            {
                //ls_item = ls_item + dt.Rows[n]["ID_Num"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["VipID"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["Grade"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["HighVipID"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["LeaderVipID"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["VipName"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["DealerID"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["deptname"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["NetDate"].ToString() + "\t";
                ls_item = ls_item + dt.Rows[n]["NetLevel"].ToString() + "\n";

                resp.Write(ls_item);
                ls_item = "";
            }

            resp.End();
        }
        #region//DataSetToExcel2
        /// <summary>
        /// 将DataSet数据导出到Excel
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <param name="colName">Excel列名</param>
        public void DataSetToExcel2(DataTable dt, string colNames)
        {
            string[] colname = colNames.Split(new char[] { ';' });

            HttpResponse resp;
            resp = HttpContext.Current.Response;
            resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            resp.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
            resp.ContentType = "application/ms-excel";
            string colHeaders = "", ls_item = "";

            //定义表对象与行对象，同时用DataSet对其值进行初始化 
            //DataTable dt = ds.Tables[0];
            //DataRow[] myRow = dt.Select();//可以类似dt.Select("id>10")之形式达到数据筛选目的
            int i = 0;
            int cl = dt.Columns.Count;


            //取得数据表各列标题，各标题之间以/t分割，最后一个列标题后加回车符 
            for (i = 0; i <= colname.Length - 1; i++)
            {
                if (i == (colname.Length - 1))//最后一列，加/n
                {
                    colHeaders += colname[i] + "\n";
                }
                else
                {
                    colHeaders += colname[i] + "\t";
                }

            }

            resp.Write(colHeaders);
            //向HTTP输出流中写入取得的数据信息 

            if (dt.Rows.Count > 0)
            {
                DataRow[] myRow = dt.Select("NetLevel=0");
                string vipid = myRow[0]["VipID"].ToString();
                ls_item = ls_item + myRow[0]["VipID"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["Grade"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["HighVipID"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["LeaderVipID"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["VipName"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["DealerID"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["deptname"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["NetDate"].ToString() + "\t";
                ls_item = ls_item + myRow[0]["NetLevel"].ToString() + "\n";

                resp.Write(ls_item);
                ls_item = "";
            }

            resp.End();
        }
        #endregion
    }
}
