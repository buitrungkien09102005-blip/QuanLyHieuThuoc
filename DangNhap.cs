using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;

namespace QuanLyHieuThuoc
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        // Kết nối đến csdl
        string conStr = @" Data Source=Eagle\SQLEXPRESS01;
                           Initial Catalog=LTMT2-K15-Nhom11;
                           Integrated Security=True";
        //string sql = @"";
        //SqlConnection KetNoi;
        //SqlCommand ThucHien;
        //SqlDataReader Doc;

        private void button_dangnhap_Click(object sender, EventArgs e)
        {
            string username = txt_taikhoan.Text.Trim();
            string password = txt_matkhau.Text;


            SqlConnection conn = new SqlConnection(conStr);
            //using (SqlConnection conn = Connect.GetConnection())
            //{
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan=@username AND MatKhau=@password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        //MessageBox.Show("Đăng nhập thành công!");
                        FormMDI mainForm = new FormMDI(); // form chính sau khi đăng nhập
                        mainForm.Show();
                        this.Hide();
                    }
                    else if (username == "" || password == "")
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //return;
                    }

                    else
                    {
                            MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            //}
            /*
            using (SqlConnection KetNoi = new SqlConnection(conn))
            {
 
                KetNoi.Open();
            string taikhoan = txt_taikhoan.Text;
            string matkhau = txt_matkhau.Text;
            sql = @"SELECT TaiKhoan, MatKhau
                    FROM   TaiKhoan
                    WHERE TaiKhoan=@taikhoan AND MatKhau=@matkhau";
                       
            ThucHien = new SqlCommand(sql, KetNoi);
            ThucHien.Parameters.AddWithValue(@taikhoan, taikhoan);
            ThucHien.Parameters.AddWithValue(matkhau, matkhau);
            Doc = ThucHien.ExecuteReader();

            if (string.IsNullOrEmpty(taikhoan) || string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(Doc.HasRows)
            {
                MessageBox.Show("Đăng nhập thành công");
                this.Hide();
                FormMDI F = new FormMDI();
                F.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu sai!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
                
            //ThucHien.ExecuteNonQuery();
            
            KetNoi.Close();

            }*/
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            // Ẩn mật khẩu mặc định
            txt_matkhau.UseSystemPasswordChar = true;
        }

        private void button_dangky_Click(object sender, EventArgs e)
        {
            //DangKy formDK = new DangKy();
            //formDK.ShowDialog();
            string username = txt_taikhoan.Text.Trim();
            string password = txt_matkhau.Text;
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = @"INSERT INTO TaiKhoan
                                (TaiKhoan, MatKhau)
                            VALUES (@TaiKhoan,@MatKhau)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@TaiKhoan", SqlDbType.NVarChar);
            cmd.Parameters["@TaiKhoan"].Value = txt_taikhoan.Text;
            cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar);
            cmd.Parameters["@MatKhau"].Value = txt_matkhau.Text;
            MessageBox.Show("Đăng kí thành công!");
            cmd.ExecuteNonQuery();
            conn.Close();
            //HienThi();
            /*try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đăng ký thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }*/
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Ẩn hiện mật khẩu
            txt_matkhau.UseSystemPasswordChar = !checkBoxMatKhau.Checked; 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ Admin để lấy lại mật khẩu!");
            /*
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string sql = "UPDATE TaiKhoan SET mat_khau=@newpass " +
                         "WHERE ten_dang_nhap=@user AND mat_khau=@oldpass";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@user", txt_taikhoan.Text.Trim());
            cmd.Parameters.AddWithValue("@oldpass", txt_matkhau.Text.Trim());
            cmd.Parameters.AddWithValue("@newpass", txt_matkhau.Text.Trim());

            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                MessageBox.Show("Đổi mật khẩu thành công!");
            else
                MessageBox.Show("Sai mật khẩu cũ!");
            */
        }

        private void button_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
