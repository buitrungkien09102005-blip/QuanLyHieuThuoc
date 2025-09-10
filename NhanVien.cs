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
        Connect c = new Connect();

        private void NhanVien_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        void HienThi()
        {
            dataGridView.Rows.Clear();
            //SqlConnection conn = new SqlConnection(conStr);
            string query = @"SELECT MaNV, TenNV, NgaySinh, GioiTinh, SoDienThoai, DiaChi, GhiChu,
                                FORMAT(NgaySinh, 'dd/MM/yyyy') AS NgaySinh
                             FROM   NhanVien";
            c.connect();
            SqlCommand cmd = new SqlCommand(query, c.conn);
            SqlDataReader read = cmd.ExecuteReader();
            int i = 0;
            while (read.Read())
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells[0].Value = read[0];
                dataGridView.Rows[i].Cells[1].Value = read[1];
                dataGridView.Rows[i].Cells[2].Value = read[2].ToString().Substring(0, 10);
                dataGridView.Rows[i].Cells[3].Value = read[3];
                dataGridView.Rows[i].Cells[4].Value = read[4];
                dataGridView.Rows[i].Cells[5].Value = read[5];
                dataGridView.Rows[i].Cells[6].Value = read[6];
                i++;
            }
            c.disconnect();
        }

        private void ClearFields()
        {
            textBoxMaNV.Clear();
            textBoxTenNV.Clear();
            textBoxDienThoai.Clear();
            textBoxDiaChi.Clear();
            textBoxGhiChu.Clear();
            textBoxGioiTinh.Clear();
            dateTimePickerNgaySinh.Value = DateTime.Now;

            //radioNam.Checked = false;
            //radioNu.Checked = false;
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            //SqlConnection conn = new SqlConnection(conStr);
            c.connect();

            string checkQuery = "SELECT COUNT(*) FROM NhanVien WHERE MaNV = @MaNV";
            SqlCommand checkCmd = new SqlCommand(checkQuery, c.conn);
            checkCmd.Parameters.AddWithValue("@MaNV", textBoxMaNV.Text.Trim());

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("Mã nhân viên đã tồn tại, vui lòng nhập mã khác!", "Thông báo",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng, không thêm nữa
            }

            string query = @"INSERT INTO NhanVien
                                (MaNV, TenNV, NgaySinh, GioiTinh, SoDienThoai, DiaChi, GhiChu)
                            VALUES (@MaNV,@TenNV,@NgaySinh,@GioiTinh,@SoDienThoai,@DiaChi,@GhiChu)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

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
            ClearFields();

            cmd.ExecuteNonQuery();
            c.disconnect();
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
            //SqlConnection conn = new SqlConnection(conStr);
            c.connect();
            string query = @"UPDATE NhanVien
                             SET    MaNV = @MaNV, TenNV = @TenNV, NgaySinh = @NgaySinh, 
                                    GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, 
                                    DiaChi = @DiaChi, GhiChu = @GhiChu
                             WHERE (MaNV = @Original_MaNV)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

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
            c.disconnect();
            HienThi();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Bạn có chắc muốn xoá nhân viên "+textBoxTenNV.Text, "Chú ý", 
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                //SqlConnection conn = new SqlConnection(conStr);
                c.connect();
                string query = @"DELETE FROM NhanVien
                            WHERE (MaNV = @Original_MaNV)";
                SqlCommand cmd = new SqlCommand(query, c.conn);

                cmd.Parameters.Add("@Original_MaNV", SqlDbType.NVarChar);
                cmd.Parameters["@Original_MaNV"].Value = dataGridView.CurrentRow.Cells[0].Value;

                cmd.ExecuteNonQuery();
                c.disconnect();
                HienThi();
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
