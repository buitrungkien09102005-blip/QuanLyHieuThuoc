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
        
        Connect c = new Connect();

        private void button_dangnhap_Click(object sender, EventArgs e)
        {
            string username = txt_taikhoan.Text;
            string password = txt_matkhau.Text;

            try
            {
                c.connect();

                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan=@username AND MatKhau=@password ";
                SqlCommand cmd = new SqlCommand(query, c.conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int result = (int)cmd.ExecuteScalar();

                if ( result > 0)
                {
                    this.Hide();
                    FormMDI mainForm = new FormMDI(); // form chính sau khi đăng nhập
                    mainForm.ShowDialog();
                    mainForm = null;
                    txt_matkhau.Text = "";
                    this.Show();
                }
                else if (username == "" || password == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_matkhau.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            // Ẩn mật khẩu mặc định
            txt_matkhau.UseSystemPasswordChar = true;
        }

        private void button_dangky_Click(object sender, EventArgs e)
        {
            string username = txt_taikhoan.Text.Trim();
            string password = txt_matkhau.Text;
            c.connect();
            string query = @"INSERT INTO TaiKhoan
                                (TaiKhoan, MatKhau)
                            VALUES (@TaiKhoan,@MatKhau)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

            cmd.Parameters.Add("@TaiKhoan", SqlDbType.NVarChar);
            cmd.Parameters["@TaiKhoan"].Value = txt_taikhoan.Text;
            cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar);
            cmd.Parameters["@MatKhau"].Value = txt_matkhau.Text;
            MessageBox.Show("Đăng kí thành công!");
            cmd.ExecuteNonQuery();
            c.disconnect();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Ẩn hiện mật khẩu
            txt_matkhau.UseSystemPasswordChar = !checkBoxMatKhau.Checked; 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ Admin để lấy lại mật khẩu!");
        }

        private void button_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
