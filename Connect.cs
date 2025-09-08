using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyHieuThuoc
{
    class Connect
    {
        public SqlConnection conn;
        public void connect()
        {
            string conStr = @"Data Source=Eagle\SQLEXPRESS01;
                         Initial Catalog=LTMT2-K15-Nhom11;
                         Integrated Security=True";
            try
            {
                conn = new SqlConnection(conStr);
                conn.Open();
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi: " + ex.Message);            }
        }
        public void disconnect()
        {
            conn.Close();
            conn.Dispose();
            conn = null;
        }

        public Boolean exeSQL(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    /*
    internal class Connect
    {
        // Chuỗi kết nối tới SQL Server
        private static string connectionString =
            @"Data Source=Eagle\SQLEXPRESS01;
                         Initial Catalog=LTMT2-K15-Nhom11;
                         Integrated Security=True";

        // Hàm trả về đối tượng SqlConnection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }*/
}
