using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace LXY.MeteorProject.DAL
{
    public static class SqlHelper
    {
        private static readonly string connStr = "server=192.168.13.150;database=handev_MSCRM;uid=sa;pwd=P@ssw0rd;Min Pool Size = 3";
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {

                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }

                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static DataTable ExecuteTable(string sql, CommandType type, params SqlParameter[] ps)
        {
            //DataTable dt = new DataTable();
            //using (SqlDataAdapter sda = new SqlDataAdapter(sql, connStr))
            //{
            //    if (ps != null)
            //    {
            //        sda.SelectCommand.Parameters.AddRange(ps);
            //    }
            //    sda.Fill(dt);
            //    return dt;
            //}
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    sda.SelectCommand.CommandType = type;
                    if (ps != null)
                    {
                        sda.SelectCommand.Parameters.AddRange(ps);
                    }
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] ps)
        {
            SqlConnection conn = new SqlConnection(connStr);
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {

                if (ps != null)
                {
                    cmd.Parameters.AddRange(ps);
                }
                try
                {
                    conn.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                }
                catch (Exception e)
                {
                    conn.Close();
                    conn.Dispose();
                    throw e;
                }
            }
        }

    }
}
