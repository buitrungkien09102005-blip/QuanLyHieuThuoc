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
    public partial class KhachHang : Form
    {
        public KhachHang()
        {
            InitializeComponent();
        }

        Connect c = new Connect();

        void HienThi()
        {
            dataGridView.Rows.Clear();
            string query = @"SELECT MaKH, TenKH, NgaySinh, GioiTinh, SoDienThoai, DiaChi, GhiChu
                            FROM   KhachHang";
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
            textBoxMaKH.Clear();
            textBoxTenKH.Clear();
            textBoxDienThoai.Clear();
            textBoxDiaChi.Clear();
            textBoxGhiChu.Clear();
            textBoxGioiTinh.Clear();
            dateTimePickerNgaySinh.Value = DateTime.Now;
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            c.connect();

            string checkQuery = "SELECT COUNT(*) FROM KhachHang WHERE MaKH = @MaKH";
            SqlCommand checkCmd = new SqlCommand(checkQuery, c.conn);
            checkCmd.Parameters.AddWithValue("@MaKH", textBoxMaKH.Text.Trim());

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("Mã khách hàng đã tồn tại, vui lòng nhập mã khác!", "Thông báo", 
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng, không thêm nữa
            }

            string query = @"INSERT INTO KhachHang
                                (MaKH, TenKH, NgaySinh, GioiTinh, SoDienThoai, DiaChi, GhiChu)
                            VALUES (@MaKH,@TenKH,@NgaySinh,@GioiTinh,@SoDienThoai,@DiaChi,@GhiChu)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

            cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar);
            cmd.Parameters["@MaKH"].Value = textBoxMaKH.Text;
            cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar);
            cmd.Parameters["@TenKH"].Value = textBoxTenKH.Text;
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

        private void KhachHang_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            c.connect();
            string query = @"UPDATE KhachHang
                            SET     MaKH = @MaKH, TenKH = @TenKH, NgaySinh = @NgaySinh, 
                                    GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, 
                                    DiaChi = @DiaChi, GhiChu = @GhiChu
                            WHERE (MaKH = @Original_MaKH)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

            cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar);
            cmd.Parameters["@MaKH"].Value = textBoxMaKH.Text;
            cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar);
            cmd.Parameters["@TenKH"].Value = textBoxTenKH.Text;
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
            cmd.Parameters.Add("@Original_MaKH", SqlDbType.NVarChar);
            cmd.Parameters["@Original_MaKH"].Value = dataGridView.CurrentRow.Cells[0].Value;

            cmd.ExecuteNonQuery();
            c.disconnect();
            HienThi();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaKH.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            textBoxTenKH.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
            dateTimePickerNgaySinh.Value = Convert.ToDateTime(dataGridView.CurrentRow.Cells[2].Value);
            textBoxGioiTinh.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
            textBoxDienThoai.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            textBoxDiaChi.Text = dataGridView.CurrentRow.Cells[5].Value.ToString();
            textBoxGhiChu.Text = dataGridView.CurrentRow.Cells[6].Value.ToString();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Bạn có chắc muốn xoá khách hàng " + textBoxTenKH.Text, "Chú ý",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                c.connect();
                string query = @"DELETE FROM KhachHang
                                WHERE (MaKH = @Original_MaKH)";
                SqlCommand cmd = new SqlCommand(query, c.conn);

                cmd.Parameters.Add("@Original_MaKH", SqlDbType.NVarChar);
                cmd.Parameters["@Original_MaKH"].Value = dataGridView.CurrentRow.Cells[0].Value;

                cmd.ExecuteNonQuery();
                c.disconnect();
                HienThi();
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Bạn có chắc muốn thoát ", "Chú ý",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                c.connect();
                this.Close();
                c.disconnect();
            }    
        }
    }
}
