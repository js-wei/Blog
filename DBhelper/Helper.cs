using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Web;

namespace DBhelper
{
    #region 数据库操作
    public class Helper
    {
        #region 数据库连接相关
        private string sqlConnection;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string SqlConnection
        {
            get { return sqlConnection; }
            set { sqlConnection = value; }
        }
        private bool cache;

        public bool Cache
        {
            set { cache = value; }
        }
        /// <summary>
        /// 是否缓存
        /// </summary>

        private bool autoModel;
        /// <summary>
        /// 是否缓存
        /// </summary>
        public bool AutoModel
        {
            set
            {
                if (value)
                {
                    this.CreateModel();
                }
            }
        }
        /// <summary>
        /// 构函数
        /// </summary>
        public Helper()
        {
            this.sqlConnection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            this.cache = this.IsCache();
            this.autoModel = this.IsAutoModel();
            if (this.cache)
            {
                this.CreateModel();
            }
        }
        /// <summary>
        ///  构函数
        /// </summary>
        /// <param name="sqlConnectionString">数据库连接配置名</param>
        public Helper(string sqlConnectionString)
        {
            this.sqlConnection = ConfigurationManager.ConnectionStrings[sqlConnectionString].ConnectionString;
            this.cache = this.IsCache();
            this.autoModel = this.IsAutoModel();
            if (this.cache)
            {
                this.CreateModel();
            }
        }
        /// <summary>
        /// 构函数
        /// </summary>
        /// <param name="server">服务器名</param>
        /// <param name="database">数据库名</param>
        public Helper(string server, string database)
        {
            this.sqlConnection = "server=" + server + ";database=" + database + ";uid=sa;pwd=123";
            this.cache = this.IsCache();
            this.autoModel = this.IsAutoModel();
            if (this.cache)
            {
                this.CreateModel();
            }
        }
        /// <summary>
        /// 构函数
        /// </summary>
        /// <param name="AutoCreateModel"></param>
        /// <param name="Cache"></param>
        public Helper(bool AutoCreateModel, bool Cache)
        {
            this.sqlConnection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            this.cache = this.IsCache();
            this.autoModel = this.IsAutoModel();
            if (this.cache)
            {
                this.CreateModel();
            }
        }
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <returns>SqlConnection 数据库连接</returns>
        private SqlConnection getConnection()
        {
            return new SqlConnection(this.sqlConnection);
        }
        /// <summary>
        /// 数据库连接
        /// </summary>
        /// <returns></returns>
        internal SqlConnection GetConnection()
        {
            return new SqlConnection(this.sqlConnection);
        }
        #endregion

        #region 万能添加，默认的需要Model与数据库字段对应  by:wei 2013-08-05
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public bool Insert<T>(T t, string tableName, string pk = "id")
        {
            string sql1 = "";
            string sql2 = "";
            bool flag = true;
            var _pk = string.Empty;
            var _fd = this.GetFiledsType(tableName, out _pk);

            if (t == null)
            {
                return false;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return false;
            }

            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                var pptn = item.PropertyType.Name;

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (!string.IsNullOrEmpty(_pk))
                    {
                        if (name != "" && name != pk)
                        {
                            if (value == null)
                            {
                                value = "";
                            }
                            if (pptn == "String")
                            {
                                value = "'" + value + "'";
                            }
                            sql1 += name + ",";
                            sql2 += value + ",";
                        }
                    }
                    else
                    {
                        sql1 += name + ",";
                        if (value == null)
                        {
                            value = "";
                        }
                        if (pptn == "String")
                        {
                            value = "'" + value + "'";
                        }
                    }
                }
            }
            string sql = "insert into " + tableName + "(" + sql1.Remove(sql1.LastIndexOf(',')) + ") values" + "(" + sql2.Remove(sql2.LastIndexOf(',')) + ")";

            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() == 1;
                }
                catch
                {
                    return false;
                }
            }

            return flag;
        }
        /// <summary>
        /// 添加 默认对象命映射表名，主键为自增的id
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public bool Insert<T>(T t, string pk = "id")
        {
            string sql1 = "";
            string sql2 = "";
            bool flag = true;
            if (t == null)
            {
                return false;
            }
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            var _pk = string.Empty;
            var _fd = this.GetFiledsType(tableName, out _pk);

            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return false;
            }

            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                var pptn = item.PropertyType.Name;

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (!string.IsNullOrEmpty(_pk))
                    {
                        if (name != "" && name != pk)
                        {
                            if (value == null)
                            {
                                value = "";
                            }
                            if (pptn == "String")
                            {
                                value = "'" + value + "'";
                            }
                            sql1 += name + ",";
                            sql2 += value + ",";
                        }
                    }
                    else
                    {
                        if (value == null)
                        {
                            value = "";
                        }
                        if (pptn == "String")
                        {
                            value = "'" + value + "'";
                        }
                        sql1 += name + ",";
                        sql2 += value + ",";
                    }
                }
            }
            string sql = "insert into " + tableName + "(" + sql1.Remove(sql1.LastIndexOf(',')) + ") values" + "(" + sql2.Remove(sql2.LastIndexOf(',')) + ")";

            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() == 1;
                }
                catch
                {
                    return false;
                }
            }

            return flag;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Insert<T>(List<T> t, string pk = "id")
        {
            string sql1 = "";
            string sql2 = "";
            var _index = 0;
            bool flag = true;
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            var _pk = string.Empty;
            var _fd = this.GetFiledsType(tableName, out _pk);
            var _values = string.Empty;

            if (t == null)
            {
                return false;
            }
            foreach (var _item in t)
            {
                _values += ",(";
                var temp = string.Empty;

                PropertyInfo[] properties = _item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (properties.Length <= 0)
                {
                    flag = false;
                    break;
                }
                foreach (PropertyInfo item in properties)
                {
                    string name = item.Name;
                    object value = item.GetValue(_item, null);
                    var pptn = item.PropertyType.Name;

                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                    {
                        if (!string.IsNullOrEmpty(_pk))
                        {
                            if (name != "" && name != pk)
                            {
                                if (value == null)
                                {
                                    value = "";
                                }
                                if (_index >= 1)
                                {
                                    sql1 += name + ",";
                                }
                                if (pptn == "String")
                                {
                                    temp += "," + "'" + value + "'";
                                }
                                else
                                {
                                    temp += "," + value;
                                }

                            }
                        }
                        else
                        {
                            if (_index >= 1)
                            {
                                sql1 += name + ",";
                            }

                            if (pptn == "String")
                            {
                                temp += "," + "'" + value + "'";
                            }
                            else
                            {
                                temp += "," + value;
                            }
                        }
                    }
                }
                temp = temp.Substring(1);
                _values += temp;
                _values += ")";
                _index++;
            }
            sql2 = _values;
            string sql = string.Format("insert into {0} ({1}) values {2}", tableName, sql1.Remove(sql1.LastIndexOf(',')), sql2.Substring(1));
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() >= 1;
                }
                catch
                {
                    return false;
                }
            }

            return flag;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="_d">数据</param>
        /// <returns></returns>
        public bool Insert(string tableName, Dictionary<string, string> _d)
        {
            var flag = true;
            tableName = "[" + tableName + "]";     //获取表名
            var _fildes = string.Empty;
            var _values = string.Empty;
            var _ft = this.GetFiledsType(tableName);
            foreach (var item in _d)
            {
                _fildes += ",[" + item.Key + "]";
            }
            string sql = string.Format("insert into {0} ({1}) values ({2})", tableName, _fildes.Substring(1), this.MergeInsertValue(_ft, _d).Substring(1));

            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() == 1;
                }
                catch
                {
                    return false;
                }
            }
            return flag;
        }
        /// <summary>
        /// 多个插入
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="_d"></param>
        /// <returns></returns>
        public bool Insert(string tableName, List<Dictionary<string, string>> _d)
        {
            var flag = true;
            tableName = "[" + tableName + "]";     //获取表名
            var _fildes = string.Empty;
            var _values = string.Empty;
            var _pk = string.Empty;
            var _ft = this.GetFiledsType(tableName, out _pk);
            foreach (var _item in _d[0])
            {
                if (!string.IsNullOrEmpty(_pk))
                {
                    if (!string.Equals(_item.Key.ToString(), _pk))
                    {
                        _fildes += "," + "[" + _item.Key + "]";
                    }
                }
                else
                {
                    _fildes += "," + "[" + _item.Key + "]";
                }
            }
            foreach (var item in _d)
            {
                _values += ",(";
                var _temp = string.Empty;
                foreach (var temp in item)
                {
                    if (!string.IsNullOrEmpty(_pk))
                    {
                        if (!string.Equals(temp.Key.ToString(), _pk))
                        {
                            if (_ft[temp.Key].IndexOf("char") >= 0 || _ft[temp.Key].IndexOf("text") >= 0)
                            {
                                _temp += ",'" + temp.Value + "'";
                            }
                            else
                            {
                                _temp += "," + temp.Value;
                            }
                        }
                    }
                    else
                    {
                        if (_ft[temp.Key].IndexOf("char") >= 0 || _ft[temp.Key].IndexOf("text") >= 0)
                        {
                            _temp += ",'" + temp.Value + "'";
                        }
                        else
                        {
                            _temp += "," + temp.Value;
                        }
                    }
                }
                _temp = _temp.Substring(1);
                _values += _temp + ")";
            }
            string sql = string.Format("insert into {0} ({1}) values {2}", tableName, _fildes.Substring(1), _values.Substring(1));

            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() >= 1;
                }
                catch
                {
                    return false;
                }
            }
            return flag;
        }
        /// <summary>
        /// 组合插入数据
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, CommandType cmdType, string cmdText, IDataParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if (parameter != null)
                    {
                        if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parameter);
                    }
                }
            }
        }
        /// <summary>
        /// 组合插入数据
        /// </summary>
        /// <param name="_d">数据字段集合</param>
        /// <param name="_t">数据集合</param>
        /// <returns></returns>
        private string MergeInsertValue(Dictionary<string, string> _d, Dictionary<string, string> _t)
        {
            var _result = string.Empty;

            foreach (var item in _t)
            {
                if (_d[item.Key].IndexOf("char") >= 0 || _d[item.Key].IndexOf("text") >= 0)
                {
                    _result += ",'" + item.Value + "'";
                }
                else
                {
                    _result += "," + item.Value;
                }
            }
            return _result;
        }
        /// <summary>
        /// 组合插入数据
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="_t">数据</param>
        /// <returns></returns>
        private string MergeInsertValue(string tableName, Dictionary<string, string> _t)
        {
            var _result = string.Empty;
            var _d = this.GetFiledsType(tableName);
            foreach (var item in _t)
            {
                if (_d[item.Key].IndexOf("char") >= 0 || _d[item.Key].IndexOf("text") >= 0)
                {
                    _result += ",'" + item.Value + "'";
                }
                else
                {
                    _result += "," + item.Value;
                }
            }
            return _result;
        }
        #endregion

        #region 更新,万能更新，需要将对象名与表名对应，对象属性与表字段对应    by: wei 2013-08-07
        /// <summary>
        /// 更新，表名默认为对象名，主键为id
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public bool Update<T>(T t, int id)
        {
            string sql1 = " ";
            bool flag = true;
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            if (t == null)
            {
                return false;
            }

            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return false;
            }

            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                var pptn = item.PropertyType.Name;  //属性类型
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (name != "" && name != "id")
                    {
                        if (value == null)
                        {
                            value = "";
                        }
                        if (pptn == "String")
                        {
                            value = "'" + value + "'";
                        }
                        sql1 += name + "=" + value + ",";
                        //sql2 += value + ",";
                    }
                }

            }
            //string sql = "update " + tableName + " set " + sql1.Remove(sql1.LastIndexOf(',')) + "where id=" + id;
            string sql = string.Format("update {0} set {1} where id= {2}", tableName, sql1.Remove(sql1.LastIndexOf(',')), id);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() == 1;
                }
                catch
                {
                    return false;
                }
            }

            return flag;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="pk">主键</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public bool Update<T>(T t, int id, string pk = "id")
        {
            string sql1 = " ";
            bool flag = true;
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            var _fd = this.Find(tableName, id);
            if (t == null)
            {
                return false;
            }

            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return false;
            }

            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                var pptn = item.PropertyType.Name;  //属性类型
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (name != "" && name != pk)
                    {
                        if (value == null)
                        {
                            continue;
                        }
                        if (pptn == "String")
                        {
                            value = "'" + value + "'";
                        }
                        if (Convert.ToInt32(_fd[name]) != 0 && Convert.ToInt32(value) == 0)
                        {
                            continue;
                        }
                        sql1 += name + "=" + value + ",";
                        //sql2 += value + ",";
                    }
                }

            }
            string sql = string.Format("update {0} set {1} where {2} = {3}", tableName, sql1.Remove(sql1.LastIndexOf(',')), pk, id);

            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() == 1;
                }
                catch
                {
                    return false;
                }
            }

            return flag;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="_d"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public bool Update(string tableName, Dictionary<string, string> _d, string pk = "id")
        {
            var flag = true;
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = "update " + tableName + " set ";
            var temp = string.Empty;
            var where = string.Empty;
            var _fd = this.GetFieldsAndType(tableName);
            foreach (var item in _d)
            {
                if (string.Equals(item.Key, pk))
                {
                    where = " where " + pk + " = " + item.Value.ToString();
                }
                else
                {
                    if (_fd[item.Key].IndexOf("char") > -1 || _fd[item.Key].IndexOf("text") > -1)
                    {
                        temp += "," + item.Key.ToString() + " = '" + item.Value.ToString() + "' ";
                    }
                    else
                    {
                        temp += "," + item.Key.ToString() + " = " + item.Value.ToString();
                    }

                }
            }
            sql += temp.Substring(1) + where;
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() == 1;
                }
                catch
                {
                    return false;
                }
            }
            return flag;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="t">数据对象</param>
        /// <returns></returns>
        public bool Update<T>(T t, string pk = "id")
        {
            string sql1 = string.Empty;
            string _where = string.Empty;
            bool flag = true;
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            var _fd = new Dictionary<string, string>();
            if (t == null)
            {
                return false;
            }

            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return false;
            }
            foreach (PropertyInfo _item in properties)
            {
                string name = _item.Name;
                object value = _item.GetValue(t, null);
                var pptn = _item.PropertyType.Name;  //属性类型
                if (_item.PropertyType.IsValueType || _item.PropertyType.Name.StartsWith("String"))
                {
                    if (name != "" && name == pk)
                    {
                        _fd = this.Find(tableName, Convert.ToInt32(value));
                    }
                }
            }

            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                var pptn = item.PropertyType.Name;  //属性类型
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    if (name != "" && name != pk)
                    {
                        if (value == null)
                        {
                            continue;
                        }
                        if (pptn == "String")
                        {
                            value = "'" + value + "'";
                        }
                        else
                        {
                            if (int.Parse(_fd[name]) != 0 && Convert.ToInt32(value) == 0 && int.Parse(_fd[name]) != 1)
                            {
                                continue;
                            }
                           
                        }

                        sql1 += name + "=" + value + ",";
                        //sql2 += value + ",";
                    }
                    else
                    {
                        _fd = this.Find(tableName, Convert.ToInt32(value));
                        if (pptn == "String")
                        {
                            _where = "where " + name + "='" + value + "'";
                        }
                        else
                        {
                            _where = "where " + name + "=" + value;
                        }
                    }
                }

            }

            string sql = string.Format("update {0} set {1} {2}", tableName, sql1.Remove(sql1.LastIndexOf(',')), _where);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                try
                {
                    flag = cmd.ExecuteNonQuery() == 1;
                }
                catch
                {
                    return false;
                }
            }

            return flag;
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public bool Update<T>(List<T> t, string pk = "id")
        {

            bool flag = true;
            if (t == null)
            {
                return false;
            }
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            foreach (var _item in t)
            {
                var _where = string.Empty;
                var _values = string.Empty;

                PropertyInfo[] properties = _item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (properties.Length <= 0)
                {
                    flag = false;
                    break;
                }
                foreach (PropertyInfo item in properties)
                {
                    string name = item.Name;
                    object value = item.GetValue(_item, null);
                    var pptn = item.PropertyType.Name;

                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                    {
                        if (name != "" && name != pk)
                        {
                            if (value == null)
                            {
                                //value = "";
                                continue;
                            }
                            if (pptn == "String")
                            {
                                _values += "," + name + "='" + value + "'";
                            }
                            else
                            {
                                _values += "," + name + "=" + value;
                            }
                        }
                        else
                        {
                            _where = " where " + pk + " = " + value;
                        }
                    }
                }
                string sql = string.Format("update {0} set {1} {2}", tableName, _values.Substring(1), _where);
                using (var conn = this.getConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand() { Connection = conn, CommandText = sql, CommandType = CommandType.Text };
                    try
                    {
                        flag = cmd.ExecuteNonQuery() >= 1;
                    }
                    catch
                    {
                        flag = false;
                        continue;
                    }
                }
            }
            return flag;
        }
        #endregion

        #region 查找数据
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<T> GetData<T>(int count = 0)
        {
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            string sql = string.Empty;

            if (count == 0)
            {
                sql = string.Format("SELECT  * FROM {0}  ", tableName);
            }
            else
            {
                sql = string.Format("SELECT TOP {0} * FROM {1}", count, tableName);
            }

            var t = new List<T>();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                t = DataReader2Model.ReaderToListModel<T>(reader);
            }
            return t;
        }
        /// <summary>
        /// 查找数据 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<T> GetData<T>(int count = 0, string where = "", string order = "")
        {
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            string sql = string.Empty;
            var map = this._map(where, order);

            if (count == 0)
            {
                sql = string.Format("SELECT  * FROM {0} {1} ", tableName, map);
            }
            else
            {
                sql = string.Format("SELECT TOP {0} * FROM {1} {2}", count, tableName, map);
            }
            var t = new List<T>();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                t = DataReader2Model.ReaderToListModel<T>(reader);
            }
            return t;
        }
        /// <summary>
        /// 查找数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="count"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetData(string tableName, int count = 0, string where = "", string order = "")
        {
            var t = new List<Dictionary<string, string>>();
            var table = "[" + tableName + "]";     //获取表名
            var map = this._map(where, order);
            string sql = string.Empty;

            if (count == 0)
            {
                sql = string.Format("SELECT  * FROM {0} {1} ", table, map);
            }
            else
            {
                sql = string.Format("SELECT TOP {0} * FROM {1} {2}", count, table, map);
            }


            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                t = DataReader2Model.ReaderToListDictionary(reader);
            }
            return t;
        }
        /// <summary>
        /// 查找一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, string> Find<T>(int id, string where = "", string pk = "id")
        {
            Dictionary<string, string> _d = new Dictionary<string, string>();
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = "select * from " + tableName + " where " + pk + " = " + id + where;
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                _d = DataReader2Model.ReaderToDictionary<T>(reader);
            }
            return _d;
        }
        /// <summary>
        /// 查找一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_name"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Dictionary<string, string> Find(string _name, string where = "")
        {
            Dictionary<string, string> _d = new Dictionary<string, string>();
            var tableName = "[" + _name + "]";     //获取表名
            where = string.IsNullOrEmpty(where) ? "" : where;
            string sql = "select * from " + tableName + " where " + where;
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                _d = DataReader2Model.ReaderToDictionary(reader);
            }
            return _d;
        }
        /// <summary>
        /// 查找一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public T FindWithModel<T>(int id, string pk = "id")
        {
            var tableName = "[" + typeof(T).Name + "]";     //获取表名
            string sql = "select * from " + tableName + " where " + pk + " = " + id;
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                var _d = DataReader2Model.ReaderToModel<T>(reader);
                return _d;
            }

        }

        /// <summary>
        /// 查找一条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Dictionary<string, string> Find(string tableName, int id, string where = "", string pk = "id")
        {
            Dictionary<string, string> _d = new Dictionary<string, string>();
            var table = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";     //获取表名
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = "select * from " + table + " where " + pk + " = " + id + where;
            var t = new Dictionary<string, string>();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                _d = DataReader2Model.ReaderToDictionary(reader);
            }
            return _d;
        }
        /// <summary>
        /// 上一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetPreArticle<T>(int id, string where = "", string pk = "id")
        {
            /*
             方法一：
            string preSql = "select top 1 * from news where news_id < " + id + " order by news_id DESC"
            string nextSql = "select top 1 * from news where news_id > " + id + " order by news_id ASC"
            方法二：
            string preSql = "select * from [news] where news_id = (select MAX(news_id) from [news] where news_id<"+ id + ")";
            string nextSql = "select * from [news] where news_id = (select MIN(news_id) from [news] where news_id>"+ id + ")";
             */
            var t = new Dictionary<string, string>();
            var name = "[" + typeof(T).Name + "]";
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = string.Format("select top 1 * from {0} where {2} < {1} {3} order by {2} DESC", name, id, pk, where);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                t = DataReader2Model.ReaderToDictionary<T>(reader);
            }
            return t;
        }

        /// <summary>
        /// 上一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public T GetPreArticleWithModel<T>(int id, string where = "", string pk = "id")
        {

            var t = new Dictionary<string, string>();
            var name = "[" + typeof(T).Name + "]";
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = string.Format("select top 1 * from {0} where {2} < {1} {3} order by {2} DESC", name, id, pk, where);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();

                return DataReader2Model.ReaderToModel<T>(reader);
            }

        }
        /// <summary>
        /// 上一条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetPreArticle(string tableName, int id, string where = "", string pk = "id")
        {
            var t = new Dictionary<string, string>();
            var name = "[" + tableName + "]";
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = string.Format("select top 1 * from {0} where {2} < {1} {3} order by {2} DESC", name, id, pk, where);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                t = DataReader2Model.ReaderToDictionary(reader);
            }
            return t;
        }
        /// <summary>
        /// 下一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetNextArticle<T>(int id, string where = "", string pk = "id")
        {
            var t = new Dictionary<string, string>();
            var name = "[" + typeof(T).Name + "]";
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = string.Format("select top 1 * from {0} where {2} > {1} {3} order by {2} ASC", name, id, pk, where);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                t = DataReader2Model.ReaderToDictionary<T>(reader);
            }
            return t;
        }
        /// <summary>
        /// 下一条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public T GetNextArticleWithModel<T>(int id, string where = "", string pk = "id")
        {
            var t = new Dictionary<string, string>();
            var name = "[" + typeof(T).Name + "]";
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = string.Format("select top 1 * from {0} where {2} > {1} {3} order by {2} ASC", name, id, pk, where);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                return DataReader2Model.ReaderToModel<T>(reader);
            }

        }
        /// <summary>
        /// 下一条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <param name="pk"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetNextArticle(string tableName, int id, string where = "", string pk = "id")
        {
            var t = new Dictionary<string, string>();
            var name = "[" + tableName + "]";
            where = string.IsNullOrEmpty(where) ? "" : " and " + where;
            string sql = string.Format("select top 1 * from {0} where {2} > {1} {3} order by {2} ASC", name, id, pk, where);
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                t = DataReader2Model.ReaderToDictionary(reader);
            }
            return t;
        }
        #endregion

        #region 查找 返回 数据集DataSet和DataTable

        /// <summary>
        /// 得到DataTable数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Format("select * from {0} {1}", tableName, map);
            DataTable dt = new DataTable();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                try
                {
                    sda.Fill(dt);
                }
                catch
                {
                    dt = null;
                }
            }
            return dt;
        }
        /// <summary>
        /// 得到DataTable数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName, int count, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = "";
            if (count > 0)
            {
                sql = string.Format("select top {0} {1} from {2} {3}", count, tableName, map);
            }
            else
            {
                sql = string.Format("select  from {1} {2}", tableName, map);
            }
            DataTable dt = new DataTable();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                try
                {
                    sda.Fill(dt);
                }
                catch
                {
                    dt = null;
                }
            }
            return dt;
        }

        /// <summary>
        /// 得到DataTable数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">字段</param>
        /// <param name="count">条数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName, string fileds, int count, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = "";
            if (count > 0)
            {
                sql = string.Format("select top {0} {1} from {2} {3}", count, fileds, tableName, map);
            }
            else
            {
                sql = string.Format("select *  from {0} {1}", tableName, map);
            }
            DataTable dt = new DataTable();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                try
                {
                    sda.Fill(dt);
                }
                catch
                {
                    dt = null;
                }
            }
            return dt;
        }
        /// <summary>
        /// 得到DataTable数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fileds">字段</param>
        /// <param name="count">条数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName, string fileds, string count, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Empty;
            if (!string.IsNullOrEmpty(count))
            {
                sql = string.Format("select top {0} {1} from {2} {3}", count, fileds, tableName, map);
            }
            else
            {
                sql = string.Format("select {0}  from {1} {2}", fileds, tableName, map);
            }
            DataTable dt = new DataTable();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                try
                {
                    sda.Fill(dt);
                }
                catch
                {
                    dt = null;
                }
            }
            return dt;
        }
        /// <summary>
        /// 得到DataTable数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filed">字段<param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName, string filed, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Format("select {0} from {1} {2}", filed, tableName, map);
            DataTable dt = new DataTable();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                try
                {
                    sda.Fill(dt);
                }
                catch
                {
                    dt = null;
                }
            }
            return dt;
        }
        /// <summary>
        /// 得到DataTable数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string tableName)
        {
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Format("select * from {0}", tableName);
            DataTable dt = new DataTable();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                try
                {
                    sda.Fill(dt);
                }
                catch
                {
                    dt = null;
                }
            }
            return dt;
        }


        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DataSet GetDataSet<T>()
        {
            var tableName = "[" + typeof(T).Name + "]";

            string sql = string.Format("select * from {0}", tableName);
            DataSet ds = new DataSet();
            SqlDataAdapter sdp = new SqlDataAdapter(sql, getConnection());
            sdp.Fill(ds);

            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="name">表名</param>
        /// <returns></returns>
        public DataSet GetDataSet<T>(string filde, string where, string order, string name)
        {
            var tableName = "[" + typeof(T).Name + "]";
            filde = string.IsNullOrEmpty(filde) ? "*" : filde;
            var map = this._map(where, order);
            string sql = string.Format("select {0} from {1} {2}", filde, tableName, map);
            DataSet ds = new DataSet();
            SqlDataAdapter sdp = new SqlDataAdapter(sql, getConnection());
            if (string.IsNullOrEmpty(name))
            {
                sdp.Fill(ds);
            }
            else
            {
                sdp.Fill(ds, name);
            }

            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="name">保存的DataSet表名</param>
        /// <returns></returns>
        public DataSet GetDataSet<T>(string where, string order, string name)
        {
            var tableName = "[" + typeof(T).Name + "]";

            var map = this._map(where, order);
            string sql = string.Format("select * from {0} {1}", tableName, map);
            DataSet ds = new DataSet();
            SqlDataAdapter sdp = new SqlDataAdapter(sql, getConnection());
            if (string.IsNullOrEmpty(name))
            {
                sdp.Fill(ds);
            }
            else
            {
                sdp.Fill(ds, name);
            }

            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="count">条数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="name">保存的DataSet表名</param>
        /// <returns>DataSet数据</returns>
        public DataSet GetDataSet<T>(int count, string where, string order, string name)
        {
            var tableName = "[" + typeof(T).Name + "]";

            var map = this._map(where, order);
            string sql = string.Format("select top{0} * from {1} {2}", count, tableName, map);
            DataSet ds = new DataSet();
            SqlDataAdapter sdp = new SqlDataAdapter(sql, getConnection());
            if (string.IsNullOrEmpty(name))
            {
                sdp.Fill(ds);
            }
            else
            {
                sdp.Fill(ds, name);
            }

            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataSet数据</returns>
        public DataSet GetDataSet(string tableName, string where, string order)
        {
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var map = this._map(where, order);
            var sql = string.Format("select * from {0} {1}", tableName, map);
            DataSet ds = new DataSet();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="count">条数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataSet数据</returns>
        public DataSet GetDataSet(string tableName, int count, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Format("select top {0} * from {1} {2}", count, tableName, map);
            DataSet ds = new DataSet();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="field">字段</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string tableName, string field, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Format("select {0} from {1} {2}", field, tableName, map);
            DataSet ds = new DataSet();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="count">条数</param>
        /// <param name="field">字段</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns>DataSet数据</returns>
        public DataSet GetDataSet(string tableName, int count, string field, string where, string order)
        {
            var map = this._map(where, order);
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Format("select top {0} {1} from {2} {3}", count, field, tableName, map);
            DataSet ds = new DataSet();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string tableName)
        {
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            var sql = string.Format("select * from {0}", tableName);
            DataSet ds = new DataSet();
            using (var conn = this.getConnection())
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(ds);
            }
            return ds;
        }

        /// <summary>
        /// 查找 返回数据集DataSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">保存的DataSet表名</param>
        /// <returns></returns>
        public DataSet GetDataSet<T>(string name)
        {
            var tableName = "[" + typeof(T).Name + "]";
            string sql = string.Format("select * from {0} ", tableName);
            DataSet ds = new DataSet();
            SqlDataAdapter sdp = new SqlDataAdapter(sql, getConnection());
            if (string.IsNullOrEmpty(name))
            {
                sdp.Fill(ds);
            }
            else
            {
                sdp.Fill(ds, name);
            }
            return ds;
        }

        #endregion

        #region 条件映射
        private string _map(string where, string order)
        {
            string map = string.Empty;
            if (!string.IsNullOrEmpty(where) || !string.IsNullOrEmpty(order))
            {
                if (where.IndexOf("where") <= 0 && !string.IsNullOrEmpty(where))
                {
                    where = String.IsNullOrEmpty(where) ? "" : where;
                }
                else
                {
                    where = String.IsNullOrEmpty(where) ? "" : " where " + where;
                }

                if (order.IndexOf("order") <= 0 && !string.IsNullOrEmpty(order))
                {
                    order = String.IsNullOrEmpty(order) ? "" : order;
                }
                else
                {
                    order = String.IsNullOrEmpty(order) ? "" : " order by " + order;
                }
            }
            map = where + order;
            return map;
        }
        public string _map(string where, string order, string group)
        {
            string map = string.Empty;
            where = String.IsNullOrEmpty(where) ? "" : " where " + where;
            order = String.IsNullOrEmpty(where) ? "" : " order by " + order;
            group = String.IsNullOrEmpty(group) ? "" : " group by " + group;
            map = where + order + group;
            return map;
        }
        public string _map(string where, string order, string group, string having)
        {
            string map = string.Empty;
            where = String.IsNullOrEmpty(where) ? "" : " where " + where;
            order = String.IsNullOrEmpty(where) ? "" : " order by " + order;
            group = String.IsNullOrEmpty(group) ? "" : " group by " + group;
            having = String.IsNullOrEmpty(having) ? "" : " having " + having;
            map = where + order + group;
            return map;
        }
        #endregion

        #region 组合查询条件
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        private static List<string> CheckWhere(Where where)
        {
            var _where = new List<string>();
            var _where1 = new List<string>();
            if (where == null)
            {
                throw new Exception("未实例化数据模型");
            }

            var properties = where.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                throw new Exception("未实例化的对象");
            }

            foreach (var item in properties)
            {
                var name = item.Name;
                var value = item.GetValue(@where, null);
                var pptn = item.PropertyType.Name; //属性类型
                if (!item.PropertyType.IsValueType && !item.PropertyType.Name.StartsWith("String")) continue;
                if (name == "") continue;

                if (value == null || pptn != "String") continue;
                var value1 = value.ToString();
                if (name == "fields" && value1 != "*")
                {
                    value = "[" + value + "]";
                }
                _where.Add(value.ToString());
            }
            var check = string.IsNullOrEmpty(_where[0]) ? "*" : _where[0];
            var filter = string.IsNullOrEmpty(_where[1]) ? "" : " where " + _where[1];
            var order = string.IsNullOrEmpty(_where[2]) ? "" : " order by " + _where[2];
            var group = string.IsNullOrEmpty(_where[3]) ? "" : " group by " + _where[3];
            // var _pk = string.IsNullOrEmpty(where.primary) ? " id" : where.primary;
            var temp = filter + order + group + _where[4];
            _where1.Add(check);
            _where1.Add(temp);
            return _where;
        }
        #endregion

        #region 自动创建数据库模型
        /// <summary>
        /// 创建Model
        /// </summary>
        /// <param name="name">数据库名</param>
        private void CreateModel()
        {
            if (!this.autoModel)
                return;
            var name = new List<String>();  //类名称集合
            var list = new List<String>();  //类属性集合

            using (var conn = this.getConnection())
            {
                conn.Open();
                var sql = "SELECT d.name FROM sysobjects d where d.xtype='U' and d.name<> 'dtproperties' ";
                var cmd = new SqlCommand() { Connection = conn, CommandText = sql };
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var temp = reader[i].ToString();
                        name.Add(temp);
                    }
                }
            }

            foreach (var item in name)
            {
                //获取文件保存地址
                var baseUrl = AppDomain.CurrentDomain.BaseDirectory.ToString();
                //首字母大写
                var temp = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.ToString());
                //文件保存全路径
                var filepath = baseUrl + "Model/";
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                var model = filepath + temp + ".cs";
                if (!File.Exists(model))
                {
                    using (var conn1 = this.getConnection())
                    {
                        conn1.Open();
                        var sql1 = string.Format("sp_columns {0}", "[" + temp + "]");
                        var cmd1 = new SqlCommand() { Connection = conn1, CommandText = sql1 };
                        var reader1 = cmd1.ExecuteReader();
                        list.Clear();   //清空数组
                        while (reader1.Read())
                        {
                            var name1 = reader1.GetString(3);
                            var type1 = reader1.GetString(5);
                            list.Add(name1 + "-" + type1);
                        }
                    }
                    this.CreateFile(temp, list);    //生成model
                }
            }

        }
        /// <summary>
        /// 获取表字段的类型
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetFiledsType(string tableName, out string pk)
        {
            var _d = new Dictionary<string, string>();
            pk = "";
            tableName = tableName.IndexOf("[") < 0 ? "[" + tableName + "]" : tableName;
            using (var conn1 = this.getConnection())
            {
                conn1.Open();
                var sql1 = string.Format("sp_columns {0}", tableName);
                var cmd1 = new SqlCommand() { Connection = conn1, CommandText = sql1 };
                var reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {
                    var name1 = reader1.GetString(3);
                    var type1 = reader1.GetString(5);
                    _d.Add(name1, type1);
                    if (type1.IndexOf("identity") > 0)
                    {
                        pk = name1;
                    }
                }
            }
            return _d;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetFiledsType(string tableName)
        {
            var _d = new Dictionary<string, string>();
            tableName = tableName.IndexOf("[") < 0 ? "[" + tableName + "]" : tableName;
            using (var conn1 = this.getConnection())
            {
                conn1.Open();
                var sql1 = string.Format("sp_columns {0}", tableName);
                var cmd1 = new SqlCommand() { Connection = conn1, CommandText = sql1 };
                var reader1 = cmd1.ExecuteReader();

                while (reader1.Read())
                {
                    var name1 = reader1.GetString(3);
                    var type1 = reader1.GetString(5);
                    _d.Add(name1, type1);
                }
            }
            return _d;
        }
        /// <summary>
        /// 创建对象模型
        /// </summary>
        /// <param name="className">对象名</param>
        /// <param name="property">对象属性</param>
        /// <returns></returns>
        private bool CreateFile(string className, List<String> property)
        {
            var baseUrl = AppDomain.CurrentDomain.BaseDirectory.ToString();
            var temps = baseUrl.Split('\\');
            var app = temps[temps.Length - 2];
            var flag = true;
            var _fileds = string.Empty;
            try
            {
                var output = string.Empty;
                output = "using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing System.Web;\r\nnamespace " + app + ".Model\r\n{\r\n";
                output += "\tpublic class " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(className) + " \r\n\t{\r\n ";
                foreach (var item in property)
                {
                    if (item.IndexOf(' ') > 0)
                    {
                        var temp = item.Split(' ');
                        var temp1 = temp[0].Split('-');
                        output += "\t\tpublic " + mapType(temp1[1]) + " " + temp1[0] + "{" + " get; set; " + "}\r\n";
                        _fileds += "," + temp1[0];
                    }
                    else
                    {
                        var temp1 = item.Split('-');
                        output += "\t\tpublic " + mapType(temp1[1]) + " " + temp1[0] + "{" + " get ;set;" + "}\r\n";
                        _fileds += "," + temp1[0];
                    }
                }
                output += " \r\n\t}\r\n}";
                var baseUrl1 = baseUrl + "Model/" + className + ".cs";
                File.AppendAllText(baseUrl1, output);
                this.WriteCacheFile(_fileds, className);
            }
            catch
            {
                return false;
            }
            return flag;
        }
        /// <summary>
        /// C#和数据库类型字段映射
        /// </summary>
        /// <param name="type">数据库字段类型</param>
        /// <returns></returns>
        private string mapType(string type)
        {
            var _type = string.Empty;

            if (type.IndexOf("char") >= 0)
            {
                _type = " string ";
            }
            if (type.IndexOf("text") >= 0)
            {
                _type = " string ";
            }
            if (type.IndexOf("bit") >= 0)
            {
                _type = " bool ";
            }
            if (type.IndexOf("datetime") >= 0)
            {
                _type = " DateTime ";
            }
            if (type.IndexOf("int") >= 0)
            {
                _type = " int ";
            }
            if (type.IndexOf("binary") >= 0)
            {
                _type = " byte ";
            }
            if (type.IndexOf("image") >= 0)
            {
                _type = " byte ";
            }
            if (type.IndexOf("decimal") >= 0 || type.IndexOf("numeric") >= 0)
            {
                _type = " double ";
            }
            if (type.IndexOf("money") >= 0)
            {
                _type = " decimal ";
            }
            if (type.IndexOf("float ") >= 0)
            {
                _type = " float  ";
            }
            return _type;
        }
        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <returns></returns>
        public List<string> GetModelFields(string tableName)
        {
            var list = new List<string>();  //类属性
            var _cache = string.Empty;
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            using (var conn = this.getConnection())
            {
                conn.Open();
                var sql = string.Format("sp_columns {0}", tableName);
                var cmd = new SqlCommand() { Connection = conn, CommandText = sql };
                var reader = cmd.ExecuteReader();
                list.Clear();   //清空数组
                while (reader.Read())
                {
                    var name = reader.GetString(3);
                    var type = reader.GetString(5);
                    list.Add(name);
                    _cache += "," + name;
                }
                if (this.cache) //创建缓存文件
                {
                    this.WriteCacheFile(_cache, tableName);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取表字段键值对
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns></returns>
        public Dictionary<string, string> GetFieldsAndType(string tableName)
        {
            var list = new Dictionary<string, string>();  //类属性

            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            using (var conn = this.getConnection())
            {
                conn.Open();
                var sql = string.Format("sp_columns {0}", tableName);
                var cmd = new SqlCommand() { Connection = conn, CommandText = sql };
                var reader = cmd.ExecuteReader();
                list.Clear();   //清空数组
                while (reader.Read())
                {
                    var name = reader.GetString(3);
                    var type = reader.GetString(5);
                    list.Add(name, type);

                }

            }
            return list;
        }
        /// <summary>
        /// 获取表字段没有字段类型
        /// </summary>
        /// <returns></returns>
        public string GetModelFieldsWithoutType(string tableName)
        {
            var list = string.Empty;  //类属性
            tableName = tableName.IndexOf("[") > -1 ? tableName : "[" + tableName + "]";
            using (var conn = this.getConnection())
            {
                conn.Open();
                var sql = string.Format("sp_columns {0}", tableName);
                var cmd = new SqlCommand() { Connection = conn, CommandText = sql };
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var name = reader.GetString(3);
                    var type = reader.GetString(5);
                    list += "," + name;
                }
                if (this.cache) //创建缓存文件
                {
                    this.WriteCacheFile(list, tableName);
                }

            }
            list = list.Substring(1);

            return list;
        }
        #endregion

        #region 缓存文件
        /// <summary>
        /// 写入缓存文件
        /// </summary>
        /// <param name="reader">SqlDataReader数据源</param>
        /// <param name="tableName">文件名称</param>
        /// <param name="path">保存路径</param>
        /// <returns></returns>
        private bool WriteCacheFile(string data, string tableName, string path = "/Cache/")
        {
            var flag = true;
            var list = string.Empty;
            var baseUrl = AppDomain.CurrentDomain.BaseDirectory.ToString();
            try
            {
                path = baseUrl + path;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var d = Convert.ToString(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute);
                list += tableName + "的表字段缓存文件\r\n";
                list += "本文件由系统创建于：" + d + "\r\n";
                list += "保存的是表字段，文件存是为了减少数据查询开销\r\n";

                list += data.Substring(1);
                var fullPath = path + "/cache_" + tableName + "_fields.txt";

                if (!File.Exists(fullPath))
                {
                    File.AppendAllText(fullPath, list, Encoding.UTF8);
                }

            }
            catch
            {
                return false;
            }
            return flag;
        }
        /// <summary>
        /// 是否缓存
        /// </summary>
        /// <returns></returns>
        private bool IsCache()
        {
            var flag = true;
            var _cache = string.Empty;
            try
            {
                _cache = ConfigurationManager.ConnectionStrings["cache"].ConnectionString;
            }
            catch
            {
                _cache = "";

            }
            if (!string.Equals(_cache, "cache"))
            {
                return false;
            }
            return flag;
        }
        /// <summary>
        /// 是否自动创建模型
        /// </summary>
        /// <returns></returns>
        private bool IsAutoModel()
        {
            var flag = true;
            var _cache = string.Empty;
            try
            {
                _cache = ConfigurationManager.ConnectionStrings["aotuModel"].ConnectionString;
            }
            catch
            {
                _cache = "";

            }
            if (!string.Equals(_cache, "aotu"))
            {
                return false;
            }
            return flag;
        }

        #endregion

    }
    #endregion

    #region 查询条件
    public class Where
    {
        /// <summary>
        /// 查询主键，在更新时必填
        /// </summary>
        private string _primary;
        /// <summary>
        /// 查询主键，在更新时必填(默认：id)
        /// </summary>
        public string primary
        {
            get { return _primary; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _primary = "id";
                }
                else
                {
                    _primary = value;
                }
            }
        }
        /// <summary>
        /// 查询字段
        /// </summary>
        private string _fields;
        /// <summary>
        /// 查询字段(默认：*)
        /// </summary>
        public string fields
        {
            get { return _fields; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _fields = "*";
                }
                else
                {
                    _fields = value;
                }
            }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string where { get; set; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 分组条件
        /// </summary>
        public string group { get; set; }
        /// <summary>
        /// 其他条件
        /// </summary>
        public string another { get; set; }

        private string sql;
        /// <summary>
        /// 获取SQL语句
        /// </summary>
        public string Sql
        {
            get { return sql; }
        }
        public Where()
        {
            var _pk = string.IsNullOrEmpty(this._primary) ? " id" : this._primary;
            var _fields = string.IsNullOrEmpty(this.fields) ? "*" : this.fields;
            var _where = string.IsNullOrEmpty(this.where) ? " " : " where " + this.where;
            var _order = string.IsNullOrEmpty(this.order) ? "" : " order by " + this.order;
            var _group = string.IsNullOrEmpty(this.group) ? "" : "group by " + this.group;
            var _another = string.IsNullOrEmpty(this.another) ? "" : this.another;
            var sql = string.Format("select {0} from {1}  {2}  {3} {4} {5}", _pk, _fields, _where, _order, _group, _another);
            this.sql = sql;
        }
    }
    #endregion

    #region 分页类
    public class Pager
    {
        public Pager()
        {
            this.IsShowDetails = true;
            this.IsShowGoto = true;
            this.PageLength = 10;
            this.PageSize = 10;
            this.CurPage = 1;
        }
        /// <summary>
        /// [属性]分页过后的数据。
        /// </summary>
        public IList PageList { get; set; }
        /// <summary>
        /// [属性]跳转地址。
        /// </summary>
        public string Navigetion { get; set; }
        /// <summary>
        /// [属性]每页显示记录数。
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// [属性]所请求的当前页数。
        /// </summary>
        public int CurPage { get; set; }

        /// <summary>
        /// [属性]总页数。
        /// </summary>
        public int PageCount { get; set; }
        /// <summary>
        /// [属性]总记录数。
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// [属性]相对于当前页的上一页
        /// </summary>
        public int PrevPage
        {
            get
            {
                if (CurPage > 1)
                {
                    return CurPage - 1;
                }
                return 1;
            }
        }

        /// <summary>
        /// [属性]相对于当前页的下一页
        /// </summary>
        public int NextPage
        {
            get
            {
                if (CurPage < PageCount)
                {
                    return CurPage + 1;
                }
                return PageCount;
            }
        }
        /// <summary>
        /// [属性]显示基本信息
        /// </summary>
        public bool IsShowDetails { get; set; }
        /// <summary>
        /// [属性]显示跳转选项
        /// </summary>
        public bool IsShowGoto { get; set; }
        /// <summary>
        /// [属性]导航步长
        /// </summary>
        public int PageLength { get; set; }
    }
    #endregion

    #region 分页数据
    public class PagerDataList
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="T">模型</typeparam>
        /// <param name="page">Pager对象</param>
        /// <param name="where">Where对象</param>
        /// <returns></returns>
        public Pager Query<T>(Pager page, Where where)
        {
            var _pk = string.IsNullOrEmpty(where.primary) ? " id" : where.primary;
            var _fields = string.IsNullOrEmpty(where.fields) ? "*" : where.fields;
            var _where = string.IsNullOrEmpty(where.where) ? " " : " where " + where.where + " and ";
            var _order = string.IsNullOrEmpty(where.order) ? "" : " order by " + where.order;
            var _group = string.IsNullOrEmpty(where.group) ? "" : "group by " + where.group;
            var _another = string.IsNullOrEmpty(where.another) ? "" : where.another;
            var _name = "[" + typeof(T).Name + "]";
            var _end = (page.CurPage - 1) * page.PageSize;
            var count = string.Format("select count(*) from {0} {1}", _name, _where);
            var sql = string.Format("select top {0} {1} from {2} where {4}  {8}  not in (select top {3} {8} from {2} )  {5} {6} {7}", page.PageSize, _fields, _name, _end, _where, _group, _another, _order, _pk);
            var db = new Helper();
            using (var conn = db.GetConnection())
            {
                conn.Open();
                var cmd1 = new SqlCommand() { CommandText = count, CommandType = CommandType.Text, Connection = conn };
                var _count = (int)cmd1.ExecuteScalar();
                page.RecordCount = _count;
                var _pagecount = Math.Ceiling((double)_count / page.PageSize);
                page.PageCount = Convert.ToInt32(_pagecount);
                var cmd = new SqlCommand() { CommandText = sql, CommandType = CommandType.Text, Connection = conn };
                var reader = cmd.ExecuteReader();
                page.PageList = DataReader2Model.ReaderToListModel<T>(reader);
                //获取地址
                var uri = HttpContext.Current.Request.Url.AbsolutePath;
                //http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.2.min.js
                //没有Jquery从远程cdn获取
                var jquery = "<script>var oHead = document.getElementsByTagName('head').item(0);var oScript= document.createElement(\"script\"); oScript.type = \"text/javascript\";  oScript.src=\"http://cdn.bootcss.com/jquery/1.11.1/jquery.min.js\";oHead.appendChild(oScript);</script> ";
                var click = "<script>$(function(){$('#goto').change(function () { window.location.href = $(this).val(); });$('a[disabled=\"disabled\"]').click(function (e) { e.preventDefault(); });});</script>";
                /*var click = "<script>if(navigator.userAgent.indexOf(\"IE\") >= 0){";
                click += "oScript.onreadystatechange = function (){";
                click += "if(oScript && (oScript.readyState == \"loaded\" || oScript.readyState == \"complete\")){";
                click += "oScript.onreadystatechange = null;";
                click += "$('#goto').change(function () { window.location.href = $(this).val(); });$('a[disabled=\"disabled\"]').click(function (e) { e.preventDefault(); });";
                click += "}}; ";
                click += "}else{oScript.onload = function () {oScript.onload = null;";
                click += "$('#goto').change(function () { window.location.href = $(this).val(); });$('a[disabled=\"disabled\"]').click(function (e) { e.preventDefault(); });";
                click += "};}</script> ";*/
                page.PageLength = page.PageLength == 0 ? 10 : page.PageLength;
                int start = 0;
                int end = page.PageLength;
                int next = (int)Math.Ceiling(page.CurPage / (double)page.PageLength);
                string details = jquery + click;

                if (page.IsShowDetails)
                {
                    details += "<div class=\"show-page\"><span class=\"page-info\">共有:" + page.RecordCount + "条记录&nbsp;&nbsp;每页:" + page.PageSize + "条记录&nbsp;&nbsp;当前:" + page.CurPage + "/" + page.PageCount + "页&nbsp;&nbsp;</span>";
                }
                else
                {
                    details += "<div class=\"show-page\">";
                }

                var url = details + "<ul class=\"pagelist\">";
                if (page.CurPage > 1)
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=" + 1 + "\">" + "第一页" + "</a></li>";
                }
                else
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=1\" disabled=\"disabled\">第一页</a></li>";
                }
                if (page.CurPage != 1)
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=" + page.PrevPage + "\">" + "上一页" + "</a></li>";
                }
                else
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=1\" disabled=\"disabled\">上一页</a></li>";
                }
                if (page.CurPage > 1 && ((next - 2) * page.PageLength + 1) > 0)
                {
                    url += "<li><a href=\"" + uri + "?p=" + ((next - 2) * page.PageLength + 1) + "\" >" + "前" + page.PageLength + "页" + "</a></li>";
                }
                else
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=" + page.CurPage + "\"  disabled=\"disabled\">" + "前" + page.PageLength + "页" + "</a></li>";
                }

                if (page.CurPage > page.PageLength)
                {
                    start = (next - 1) * page.PageLength + 1;
                    end = next * page.PageLength;
                }
                else
                {
                    start = 1;
                }
                for (var i = start; i <= end; i++)
                {
                    if (i <= page.PageCount)
                    {
                        if (i == page.CurPage)
                        {
                            url += "<li ><a href=\"" + uri + "?p=" + i + "\" class=\"curent\">" + i + "</a></li>";
                        }
                        else
                        {
                            url += "<li ><a href=\"" + uri + "?p=" + i + "\">" + i + "</a></li>";
                        }
                    }
                }
                if (page.CurPage < page.PageCount)
                {
                    url += "<li><a href=\"" + uri + "?p=" + page.NextPage + "\" >" + "下一页" + "</a></li>";
                }
                else
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=" + 1 + "\" disabled=\"disabled\">" + "最后一页" + "</a></li>";
                }
                if (page.CurPage >= 1 && end <= page.PageCount)
                {
                    url += "<li><a href=\"" + uri + "?p=" + (end + 1) + "\" >" + "后" + page.PageLength + "页" + "</a></li>";
                }
                else
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=" + page.CurPage + "\"  disabled=\"disabled\">" + "后" + page.PageLength + "页" + "</a></li>";
                }
                if (page.CurPage < page.PageCount)
                {
                    url += "<li><a href=\"" + uri + "?p=" + page.PageCount + "\">最后一页</a></li>";
                }
                else
                {
                    url += "<li class=\"disable\"><a href=\"" + uri + "?p=" + page.PageCount + "\" disabled=\"disabled\">最后一页</a></li>";
                }
                if (page.IsShowGoto)
                {
                    url += "</ul><div class=\"goto\">跳转:&nbsp;<select id=\"goto\" >";
                    for (var i = 1; i <= page.PageCount; i++)
                    {
                        if (i == page.CurPage)
                        {
                            url += "<option value=\"" + uri + "?p=" + i + "\" selected=\"selected\">" + i + "</option>";
                        }
                        else
                        {
                            url += "<option value=\"" + uri + "?p=" + i + "\">" + i + "</option>";
                        }
                    }

                    url += "</select>&nbsp;页</div>";
                }
                url += "</div>";
                page.Navigetion = url;
            }

            return page;
        }
    }
    #endregion

    #region 合成模型数据
    /// <summary>
    /// DataReader2Model
    /// </summary>
    public static class DataReader2Model
    {
        /// <summary>
        /// DataReader转泛型
        /// </summary>
        /// <typeparam name="T">传入的实体类</typeparam>
        /// <param name="objReader">DataReader对象</param>
        /// <returns></returns>
        public static List<T> ReaderToListModel<T>(this IDataReader objReader)
        {
            using (objReader)
            {
                List<T> list = new List<T>();

                //获取传入的数据类型
                Type modelType = typeof(T);

                //遍历DataReader对象
                while (objReader.Read())
                {
                    //使用与指定参数匹配最高的构造函数，来创建指定类型的实例
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < objReader.FieldCount; i++)
                    {
                        //判断字段值是否为空或不存在的值
                        if (!IsNullOrDBNull(objReader[i]))
                        {
                            //匹配字段名
                            PropertyInfo pi = modelType.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                //绑定实体对象中同名的字段 
                                pi.SetValue(model, CheckType(objReader[i], pi.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<T> ReaderToList_Model<T>(this IDataReader objReader)
        {
            using (objReader)
            {
                List<T> list = new List<T>();

                //获取传入的数据类型
                Type modelType = typeof(T);

                //遍历DataReader对象
                while (objReader.Read())
                {
                    //使用与指定参数匹配最高的构造函数，来创建指定类型的实例
                    T model = Activator.CreateInstance<T>();
                    //PropertyInfo pi=modelType.GetProperty(objReader.GetName(0), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    //pi.SetValue(model,CheckType(objReader[0],pi.PropertyType),null);
                    for (int i = objReader.FieldCount - 1; i >= 0; i--)
                    {
                        //判断字段值是否为空或不存在的值
                        if (!IsNullOrDBNull(objReader[i]))
                        {
                            //匹配字段名
                            PropertyInfo pi = modelType.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                //绑定实体对象中同名的字段  
                                pi.SetValue(model, CheckType(objReader[i], pi.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                return list;
            }
        }
        /// <summary>
        /// 对可空类型进行判断转换(*要不然会报错)
        /// </summary>
        /// <param name="value">DataReader字段的值</param>
        /// <param name="conversionType">该字段的类型</param>
        /// <returns></returns>
        private static object CheckType(object value, Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>
        /// 判断指定对象是否是有效值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsNullOrDBNull(object obj)
        {
            return (obj == null || (obj is DBNull)) ? true : false;
        }


        /// <summary>
        /// DataReader转Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static T ReaderToModel<T>(this IDataReader objReader)
        {
            using (objReader)
            {
                if (objReader.Read())
                {
                    Type modelType = typeof(T);
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < objReader.FieldCount; i++)
                    {
                        if (!IsNullOrDBNull(objReader[i]))
                        {
                            PropertyInfo pi = modelType.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                pi.SetValue(model, CheckType(objReader[i], pi.PropertyType), null);
                            }
                        }
                    }
                    return model;
                }
            }
            return default(T);
        }
        /// <summary>
        /// 将DataReader转成Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReaderToDictionary<T>(this IDataReader objReader)
        {
            var _d = new Dictionary<string, string>();
            using (objReader)
            {
                if (objReader.Read())
                {
                    Type modelType = typeof(T);
                    T model = Activator.CreateInstance<T>();
                    for (int i = 0; i < objReader.FieldCount; i++)
                    {
                        if (!IsNullOrDBNull(objReader[i]))
                        {
                            PropertyInfo pi = modelType.GetProperty(objReader.GetName(i), BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                            if (pi != null)
                            {
                                //pi.SetValue(model, CheckType(objReader[i], pi.PropertyType), null);
                                _d.Add(pi.Name.ToString(), objReader[i].ToString());
                            }
                        }
                    }
                }
            }
            return _d;
        }
        /// <summary>
        /// 将DataReader转成Dictionary
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReaderToDictionary(this IDataReader objReader)
        {
            var _d = new Dictionary<string, string>();
            using (objReader)
            {
                while (objReader.Read())
                {
                    for (var i = 0; i < objReader.FieldCount; i++)
                    {
                        var _value = objReader.GetValue(i).ToString();
                        var _name = objReader.GetName(i).ToString();
                        _d.Add(_name, _value);
                    }
                }
            }
            return _d;
        }
        /// <summary>
        /// ReaderToDictionary
        /// </summary>
        /// <param name="objReader"></param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> ReaderToListDictionary(this IDataReader objReader)
        {
            var t = new List<Dictionary<string, string>>();

            using (objReader)
            {
                while (objReader.Read())
                {
                    var temp = new Dictionary<string, string>();
                    for (var i = 0; i < objReader.FieldCount; i++)
                    {
                        var _name = objReader.GetName(i).ToString();
                        var _value = objReader.GetValue(i).ToString();
                        temp.Add(_name, _value);
                    }
                    t.Add(temp);
                }
            }
            return t;
        }
    }
    #endregion
}