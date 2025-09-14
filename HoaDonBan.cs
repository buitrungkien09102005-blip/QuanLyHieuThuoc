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
    public partial class HoaDonBan : Form
    {
        public HoaDonBan()
        {
            InitializeComponent();
        }

        Connect c = new Connect();

        private void HoaDonBan_Load(object sender, EventArgs e)
        {
            //ComboBaxNaNV
            comboBoxMaNV.Items.Clear();
            c.connect();
            string queryNV = "SELECT MaNV FROM NhanVien";
            SqlCommand cmd = new SqlCommand(queryNV, c.conn);
            SqlDataReader read = cmd.ExecuteReader();
            int i = 0;
            while (read.Read())
            {
                comboBoxMaNV.Items.Add(read[0]);
                i++;
            }

            //ComboBaxNaKH
            comboBoxMaKH.Items.Clear();
            c.connect();
            string queryKH = "SELECT MaKH FROM KhachHang";
            SqlCommand cmds = new SqlCommand(queryKH, c.conn);
            SqlDataReader reader = cmds.ExecuteReader();
            //int j = 0;
            while (reader.Read())
            {
                comboBoxMaKH.Items.Add(reader[0]);
                i++;
            }
            c.disconnect();
            HienThi();
        }

        void HienThi()
        {
            dataGridView.Rows.Clear();

            string query = @"SELECT MaHoaDon, MaNV, MaKH , TenNV, TenKH, NgayBan, TongTien, GhiChu
                             FROM   HoaDonBan";
            c.connect();
            SqlCommand cmd = new SqlCommand(query, c.conn);
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
                dataGridView.Rows[i].Cells[5].Value = read[5].ToString().Substring(0, 10);
                dataGridView.Rows[i].Cells[6].Value = read[6];
                dataGridView.Rows[i].Cells[7].Value = read[7];
                i++;
            }
            c.disconnect();
        }

        private void ClearFields()
        {
            textBoxMaHoaDon.Clear();
            comboBoxMaNV.Items.Clear();
            comboBoxMaKH.Items.Clear();
            textBoxTenNV.Clear();
            textBoxTenKH.Clear();
            dateTimePickerNgayBan.Value = DateTime.Now;
            textBoxTongTien.Clear();
            textBoxGhiChu.Clear();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            c.connect();
            string query = @"INSERT INTO HoaDonBan
                                (MaHoaDon, MaNV, MaKH , TenNV, TenKH, NgayBan, TongTien, GhiChu)
                        VALUES (@MaHoaDon, @MaNV, @MaKH , @TenNV, @TenKH, @NgayBan, @TongTien, @GhiChu)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

            cmd.Parameters.Add("@MaHoaDon", SqlDbType.NVarChar);
            cmd.Parameters["@MaHoaDon"].Value =textBoxMaHoaDon.Text;
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar);
            cmd.Parameters["@MaNV"].Value = comboBoxMaNV.Text;
            cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar);
            cmd.Parameters["@MaKH"].Value = comboBoxMaKH.Text;
            cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar);
            cmd.Parameters["@TenNV"].Value = textBoxTenNV.Text;
            cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar);
            cmd.Parameters["@TenKH"].Value = textBoxTenKH.Text;
            cmd.Parameters.Add("@NgayBan", SqlDbType.Date);
            cmd.Parameters["@NgayBan"].Value = dateTimePickerNgayBan.Value;
            cmd.Parameters.Add("@TongTien", SqlDbType.Decimal);
            cmd.Parameters["@TongTien"].Value = decimal.Parse(textBoxTongTien.Text);
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            cmd.Parameters["@GhiChu"].Value = textBoxGhiChu.Text;

            ClearFields();
            cmd.ExecuteNonQuery();
            c.disconnect();
            HienThi();
        }

        private void comboBoxMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxTenNV.Clear();
            c.connect();
            string query = "SELECT TenNV FROM NhanVien WHERE MaNV=@MaNV";
            SqlCommand cmd = new SqlCommand(query, c.conn);
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar);
            cmd.Parameters["@MaNV"].Value = comboBoxMaNV.Text;
            SqlDataReader read = cmd.ExecuteReader();
            int i = 0;
            if (read.Read())
            {
                textBoxTenNV.Text = read[0].ToString();
                i++;
            }
            c.disconnect();
        }

        private void comboBoxMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxTenKH.Clear();
            c.connect();
            string query = "SELECT TenKH FROM NhanVien WHERE MaKH = @MaKH";
            SqlCommand cmd = new SqlCommand(query, c.conn);
            cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar);
            cmd.Parameters["@MaKH"].Value = comboBoxMaKH.Text;
            SqlDataReader read = cmd.ExecuteReader();
            int i = 0;
            if (read.Read())
            {
                textBoxTenKH.Text = read[0].ToString();
                i++;
            }
            c.disconnect();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaHoaDon.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            comboBoxMaNV.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
            textBoxTenNV.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
            comboBoxMaKH.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
            textBoxTenKH.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            dateTimePickerNgayBan.Value = Convert.ToDateTime(dataGridView.CurrentRow.Cells[5].Value);
            textBoxTongTien.Text = dataGridView.CurrentRow.Cells[6].Value.ToString();
            textBoxGhiChu.Text = dataGridView.CurrentRow.Cells[7].Value.ToString();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            c.connect();

            string query = @"UPDATE HoaDonBan
                             SET    MaHoaDon = @MaHoaDon, MaNV = @MaNV, MaKH = @MaKH, 
                                    TenNV = @TenNV, TenKH = @TenKH
                                    NgayBan = @NgayBan, TongTien = @TongTien, GhiChu = @GhiChu
                             WHERE (MaHoaDon = @Original_MaHoaDon)";

            SqlCommand cmd = new SqlCommand(query, c.conn);

            cmd.Parameters.Add("@MaHoaDon", SqlDbType.NVarChar);
            cmd.Parameters["@MaHoaDon"].Value = textBoxMaHoaDon.Text;
            cmd.Parameters.Add("@MaNV", SqlDbType.NVarChar);
            cmd.Parameters["@MaNV"].Value = comboBoxMaNV.Text;
            cmd.Parameters.Add("@MaKH", SqlDbType.NVarChar);
            cmd.Parameters["@MaKH"].Value = comboBoxMaKH.Text;
            cmd.Parameters.Add("@TenNV", SqlDbType.NVarChar);
            cmd.Parameters["@TenNV"].Value = textBoxTenNV.Text;
            cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar);
            cmd.Parameters["@TenKH"].Value = textBoxTenKH.Text;
            cmd.Parameters.Add("@NgayBan", SqlDbType.Date);
            cmd.Parameters["@NgayBan"].Value = dateTimePickerNgayBan.Value;
            cmd.Parameters.Add("@TongTien", SqlDbType.Decimal);
            cmd.Parameters["@TongTien"].Value = decimal.Parse(textBoxTongTien.Text);
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            cmd.Parameters["@GhiChu"].Value = textBoxGhiChu.Text;
            cmd.Parameters.Add("@Original_MaHoaDon", SqlDbType.NVarChar);
            cmd.Parameters["@Original_MaHoaDon"].Value = dataGridView.CurrentRow.Cells[0].Value;


            cmd.ExecuteNonQuery();
            c.disconnect();
            HienThi();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Bạn có chắc muốn xa mã hoá đơn " + textBoxMaHoaDon.Text, "Chú ý",
                                               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (D == DialogResult.Yes)
            {

                c.connect();

                string query = @"DELETE FROM HoaDonBan
                                WHERE (MaHoaDon = @Original_MaHoaDon)";
                SqlCommand cmd = new SqlCommand(query, c.conn);

                cmd.Parameters.Add("@Original_MaHoaDon", SqlDbType.NVarChar);
                cmd.Parameters["@Original_MaHoaDon"].Value = dataGridView.CurrentRow.Cells[0].Value;

                cmd.ExecuteNonQuery();
                c.disconnect();
                HienThi();
            }
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            textBoxMaHoaDon.Clear();
            textBoxTenNV.Clear();
            textBoxTenKH.Clear();
            textBoxTongTien.Clear();
            textBoxGhiChu.Clear();
            comboBoxMaNV.SelectedIndex = -1;
            comboBoxMaKH.SelectedIndex = -1;
            dateTimePickerNgayBan.Value = DateTime.Now;
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
