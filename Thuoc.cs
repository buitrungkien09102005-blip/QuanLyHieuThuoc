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
    public partial class Thuoc : Form
    {
        public Thuoc()
        {
            InitializeComponent();
        }
        // Kết nối csdl
        Connect c = new Connect();

        void HienThi()
        {
            dataGridView.Rows.Clear();
            string query = @"SELECT MaThuoc, TenThuoc, DonViTinh, SoLuong, GiaBan, GhiChu
                             FROM   Thuoc";
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
                dataGridView.Rows[i].Cells[5].Value = read[5];
                i++;
            }
            c.disconnect();
        }

        private void ClearFields()
        {
            textBoxMaThuoc.Clear();
            textBoxTenThuoc.Clear();
            textBoxDonViTinh.Clear();
            textBoxSoLuong.Clear();
            textBoxGiaBan.Clear();
            textBoxGhiChu.Clear();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            c.connect();

            string checkQuery = "SELECT COUNT(*) FROM THuoc WHERE MaThuoc = @MaThuoc";
            SqlCommand checkCmd = new SqlCommand(checkQuery, c.conn);
            checkCmd.Parameters.AddWithValue("@MaThuoc", textBoxMaThuoc.Text.Trim());

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {
                MessageBox.Show("Mã thuốc đã tồn tại, vui lòng nhập mã khác!", "Thông báo",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng, không thêm nữa
            }

            string query = @"INSERT INTO Thuoc
                                (MaThuoc, TenThuoc, DonViTinh, SoLuong, GiaBan, GhiChu)
                            VALUES (@MaThuoc,@TenThuoc,@DonViTinh,@SoLuong,@GiaBan,@GhiChu)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

            cmd.Parameters.Add("@MaThuoc", SqlDbType.NVarChar);
            cmd.Parameters["@MaThuoc"].Value = textBoxMaThuoc.Text;
            cmd.Parameters.Add("@TenThuoc", SqlDbType.NVarChar);
            cmd.Parameters["@TenThuoc"].Value = textBoxTenThuoc.Text;
            cmd.Parameters.Add("@DonViTinh", SqlDbType.NVarChar);
            cmd.Parameters["@DonViTinh"].Value = textBoxDonViTinh.Text;
            cmd.Parameters.Add("@SoLuong", SqlDbType.NVarChar);
            cmd.Parameters["@SoLuong"].Value = textBoxSoLuong.Text;
            cmd.Parameters.Add("@GiaBan", SqlDbType.NVarChar);
            cmd.Parameters["@GiaBan"].Value = Convert.ToDecimal(textBoxGiaBan.Text);
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            cmd.Parameters["@GhiChu"].Value = textBoxGhiChu.Text;
            ClearFields();

            cmd.ExecuteNonQuery();
            c.disconnect();
            HienThi();
        }

        private void Thuoc_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaThuoc.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            textBoxTenThuoc.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
            textBoxDonViTinh.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
            textBoxSoLuong.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
            textBoxGiaBan.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            textBoxGhiChu.Text = dataGridView.CurrentRow.Cells[5].Value.ToString();
        }

        private void buttonSua_Click(object sender, EventArgs e)
        {
            c.connect();
            string query = @"UPDATE Thuoc
                            SET     MaThuoc = @MaThuoc, TenThuoc = @TenThuoc, DonViTinh = @DonViTinh, 
                                    SoLuong = @SoLuong, GiaBan = @GiaBan, GhiChu = @GhiChu
                            WHERE (MaThuoc = @Original_MaThuoc)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

            cmd.Parameters.Add("@MaThuoc", SqlDbType.NVarChar);
            cmd.Parameters["@MaThuoc"].Value = textBoxMaThuoc.Text;
            cmd.Parameters.Add("@TenThuoc", SqlDbType.NVarChar);
            cmd.Parameters["@TenThuoc"].Value = textBoxTenThuoc.Text;
            cmd.Parameters.Add("@DonViTinh", SqlDbType.NVarChar);
            cmd.Parameters["@DonViTinh"].Value = textBoxDonViTinh.Text;
            cmd.Parameters.Add("@SoLuong", SqlDbType.NVarChar);
            cmd.Parameters["@SoLuong"].Value = textBoxSoLuong.Text;
            cmd.Parameters.Add("@GiaBan", SqlDbType.NVarChar);
            cmd.Parameters["@GiaBan"].Value = Convert.ToDecimal(textBoxGiaBan.Text);
            cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            cmd.Parameters["@GhiChu"].Value = textBoxGhiChu.Text;
            cmd.Parameters.Add("@Original_MaThuoc", SqlDbType.NVarChar);
            cmd.Parameters["@Original_MaThuoc"].Value = dataGridView.CurrentRow.Cells[0].Value;

            cmd.ExecuteNonQuery();
            c.disconnect();
            HienThi();
        }

        private void buttonXoa_Click(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Bạn có chắc muốn xoá thuốc " + textBoxTenThuoc.Text, "Chú ý",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (D == DialogResult.Yes)
            {
                //SqlConnection conn = new SqlConnection(conStr);
                c.connect();
                string query = @"DELETE FROM Thuoc
                            WHERE (MaThuoc = @Original_MaThuoc)";
                SqlCommand cmd = new SqlCommand(query, c.conn);

                cmd.Parameters.Add("@Original_MaThuoc", SqlDbType.NVarChar);
                cmd.Parameters["@Original_MaThuoc"].Value = dataGridView.CurrentRow.Cells[0].Value;

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
