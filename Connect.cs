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
            /*string conStr = @"Data Source=DESKTOP-SDK527M;
                         Initial Catalog=LTMT-K15-Lê Trung Hiếu-CD230977- Xây dựng quản lí tiệm thuốc;
                         Integrated Security=True";*/

            string conStr = @"Data Source=DESKTOP-SDK527M;
                    Initial Catalog=""LTMT-K15-Lê Trung Hiếu-CD230977- Xây dựng quản lí tiệm thuốc"";
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

    
}
