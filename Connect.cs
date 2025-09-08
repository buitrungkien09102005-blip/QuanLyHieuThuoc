using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QuanLyHieuThuoc
{
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
    }
}
