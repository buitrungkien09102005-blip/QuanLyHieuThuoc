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

namespace QuanLyHieuThuoc
{
    public partial class NhanVien : Form
    {
        public NhanVien()
        {
            InitializeComponent();
        }

        // Kết nối đến csdl
        string conStr = @" Data Source=Eagle\SQLEXPRESS01;
                           Initial Catalog=LTMT2-K15-Nhom11;
                           Integrated Security=True";
        //string query = @"";
        //SqlConnection KetNoi;
        //SqlCommand ThucHien;
        //SqlDataReader Doc;

        private void NhanVien_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            HienThi();
        }

        void HienThi()
        {
            dataGridView.Rows.Clear();
            SqlConnection conn = new SqlConnection(conStr);
            string query = @"SELECT MaNV, TenNV, NgaySinh, GioiTinh, SoDienThoai, DiaChi, GhiChu
                             FROM   NhanVien";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader read = cmd.ExecuteReader();
            int i = 0;
            while (read.Read())
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells[0].Value = read[0];
                dataGridView.Rows[i].Cells[1].Value = read[1];
                dataGridView.Rows[i].Cells[2].Value = read[2];
                dataGridView.Rows[i].Cells[3].Value = read[3];
                dataGridView.Rows[i].Cells[4].Value = read[4];
                dataGridView.Rows[i].Cells[5].Value = read[5];
                dataGridView.Rows[i].Cells[6].Value = read[6];
                i++;
            }
            conn.Close();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = @"INSERT INTO NhanVien
                                (MaNV, TenNV, NgaySinh, GioiTinh, SoDienThoai, DiaChi, GhiChu)
                            VALUES (@MaNV,@TenNV,@NgaySinh,@GioiTinh,@SoDienThoai,@DiaChi,@GhiChu)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar);
            cmd.Parameters["@MaNV"].Value = textBoxMaNV.Text;
            cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar);
            cmd.Parameters["@TenNV"].Value = textBoxTenNV.Text;
            cmd.Parameters.Add("@NgaySinh", SqlDbType.Date);
            cmd.Parameters["@NgaySinh"].Value = dateTimePickerNgaySinh.Value;
            cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar);
            cmd.Parameters["@GioiTinh"].Value = textBoxGioiTinh.Text;
            cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar);
            cmd.Parameters["@SoDienThoai"].Value = textBoxDienThoai.Text;
            cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
            cmd.Parameters["@DiaChi"].Value = textBoxDiaChi.Text;
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            cmd.Parameters["@GhiChu"].Value = textBoxGhiChu.Text;
            
            cmd.ExecuteNonQuery();
            conn.Close();
            HienThi();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaNV.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            textBoxTenNV.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
            dateTimePickerNgaySinh.Value = Convert.ToDateTime(dataGridView.CurrentRow.Cells[2].Value);
            textBoxGioiTinh.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
            textBoxDienThoai.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            textBoxDiaChi.Text = dataGridView.CurrentRow.Cells[5].Value.ToString();
            textBoxGhiChu.Text = dataGridView.CurrentRow.Cells[6].Value.ToString();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = @"UPDATE NhanVien
                             SET    MaNV = @MaNV, TenNV = @TenNV, NgaySinh = @NgaySinh, 
                                    GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, 
                                    DiaChi = @DiaChi, GhiChu = @GhiChu
                             WHERE (MaNV = @Original_MaNV)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar);
            cmd.Parameters["@MaNV"].Value = textBoxMaNV.Text;
            cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar);
            cmd.Parameters["@TenNV"].Value = textBoxTenNV.Text;
            cmd.Parameters.Add("@NgaySinh", SqlDbType.Date);
            cmd.Parameters["@NgaySinh"].Value = dateTimePickerNgaySinh.Value;
            cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar);
            cmd.Parameters["@GioiTinh"].Value = textBoxGioiTinh.Text;
            cmd.Parameters.Add("@SoDienThoai", SqlDbType.NVarChar);
            cmd.Parameters["@SoDienThoai"].Value = textBoxDienThoai.Text;
            cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
            cmd.Parameters["@DiaChi"].Value = textBoxDiaChi.Text;
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            cmd.Parameters["@GhiChu"].Value = textBoxGhiChu.Text;
            cmd.Parameters.Add("@Original_MaNV", SqlDbType.NVarChar);
            cmd.Parameters["@Original_MaNV"].Value = dataGridView.CurrentRow.Cells[0].Value;

            cmd.ExecuteNonQuery();
            conn.Close();
            HienThi();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(conStr);
            conn.Open();
            string query = @"DELETE FROM NhanVien
                            WHERE (MaNV = @Original_MaNV)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.Add("@Original_MaNV", SqlDbType.NVarChar);
            cmd.Parameters["@Original_MaNV"].Value = dataGridView.CurrentRow.Cells[0].Value;

            cmd.ExecuteNonQuery();
            conn.Close();
            HienThi();
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
