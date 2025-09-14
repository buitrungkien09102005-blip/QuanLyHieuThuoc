using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyHieuThuoc
{
    public partial class ChiTietHoaDon : Form
    {
        public ChiTietHoaDon()
        {
            InitializeComponent();
        }
        Connect c = new Connect();

        private void buttonThem_Click(object sender, EventArgs e)
        {
            c.connect();
            string query = @"INSERT INTO ChiTietHoaDon
                    (MaHoaDon, MaThuoc, SoLuong, DonGia, ThanhTien, TenThuoc, GiamGia)
                        VALUES (@MaHoaDon, @MaThuoc, @SoLuong, @DonGia, (@ThanhTien, @TenThuoc, @GiamGia)";
            SqlCommand cmd = new SqlCommand(query, c.conn);

            decimal donGia = decimal.Parse(textBoxDonGia.Text);
            int soLuong = int.Parse(textBoxSoLuong.Text);
            decimal giamGia = decimal.Parse(textBoxGiamGia.Text);
            decimal thanhTien = soLuong * donGia * (1 - giamGia / 100);

            cmd.Parameters.AddWithValue("@MaHoaDon", textBoxMaHoaDon.Text);
            cmd.Parameters.AddWithValue("@MaThuoc", comboBoxMaThuoc.Text);
            cmd.Parameters.AddWithValue("@SoLuong", soLuong);
            cmd.Parameters.AddWithValue("@DonGia", donGia);
            cmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
            cmd.Parameters.AddWithValue("@TenThuoc", textBoxTenThuoc.Text);
            cmd.Parameters.AddWithValue("@GiamGia", giamGia);

            ClearFields();
            cmd.ExecuteNonQuery();
            c.disconnect();
            HienThi();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ChiTietHoaDon_Load_1(object sender, EventArgs e)
        {
            comboBoxMaThuoc.Items.Clear();
            c.connect();
            string queryThuoc = "SELECT MaThuoc, TenThuoc FROM Thuoc";
            SqlCommand cmd = new SqlCommand(queryThuoc, c.conn);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                comboBoxMaThuoc.Items.Add(rd["MaThuoc"].ToString());
            }
            c.disconnect();
            HienThi();
        }

        private void comboBoxMaThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxTenThuoc.Clear();
            c.connect();
            string maThuoc = comboBoxMaThuoc.Text;
            string query = "SELECT TenThuoc, GiaBan FROM Thuoc WHERE MaThuoc=@MaThuoc";
            SqlCommand cmd = new SqlCommand(query, c.conn);
            cmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                textBoxTenThuoc.Text = rd["TenThuoc"].ToString();

                textBoxDonGia.Text = Convert.ToDecimal(rd["GiaBan"]).ToString("0.00");
            }
            c.disconnect();
        }

        void HienThi()
        {
            dataGridView.Rows.Clear();

            string query = @"SELECT MaHoaDon, MaThuoc, SoLuong, DonGia, ThanhTien, TenThuoc, GiamGia
                               FROM ChiTietHoaDon";
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
                dataGridView.Rows[i].Cells[6].Value = read[6];
                i++;
            }
            c.disconnect();
        }

        private void ClearFields()
        {
            textBoxMaHoaDon.Clear();
            comboBoxMaThuoc.Items.Clear();
            textBoxSoLuong.Clear();
            textBoxDonGia.Clear();
            textBoxThanhTien.Clear();
            textBoxGiamGia.Clear();
            textBoxTenThuoc.Clear();
        }

        private void dataGridView_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBoxMaHoaDon.Text = dataGridView.CurrentRow.Cells[0].Value.ToString();
            comboBoxMaThuoc.Text = dataGridView.CurrentRow.Cells[1].Value.ToString();
            textBoxSoLuong.Text = dataGridView.CurrentRow.Cells[2].Value.ToString();
            textBoxDonGia.Text = dataGridView.CurrentRow.Cells[3].Value.ToString();
            textBoxThanhTien.Text = dataGridView.CurrentRow.Cells[4].Value.ToString();
            textBoxTenThuoc.Text = dataGridView.CurrentRow.Cells[5].Value.ToString();
            textBoxGiamGia.Text = dataGridView.CurrentRow.Cells[6].Value.ToString();
        }

        private void buttonSua_Click_1(object sender, EventArgs e)
        {
            c.connect();

            string query = @"UPDATE ChiTietHoaDon
                             SET    MaHoaDon = @MaHoaDon, MaThuoc = @MaThuoc, SoLuong = @SoLuong, 
                                    DonGia = @DonGia, ThanhTien = @ThanhTien, GiamGia = @GiamGia, TenThuoc = @TenThuoc
                            WHERE (MaHoaDon = @Original_MaHoaDon) AND (MaThuoc = @Original_MaThuoc)";

            SqlCommand cmd = new SqlCommand(query, c.conn);

            decimal donGia = decimal.Parse(textBoxDonGia.Text);
            int soLuong = int.Parse(textBoxSoLuong.Text);
            decimal giamGia = decimal.Parse(textBoxGiamGia.Text);
            decimal thanhTien = soLuong * donGia * (1 - giamGia / 100);

            cmd.Parameters.AddWithValue("@MaHoaDon", textBoxMaHoaDon.Text);
            cmd.Parameters.AddWithValue("@MaThuoc", comboBoxMaThuoc.Text);
            cmd.Parameters.AddWithValue("@SoLuong", soLuong);
            cmd.Parameters.AddWithValue("@DonGia", donGia);
            cmd.Parameters.AddWithValue("@ThanhTien", thanhTien);
            cmd.Parameters.AddWithValue("@TenThuoc", textBoxTenThuoc.Text);
            cmd.Parameters.AddWithValue("@(GiamGia", giamGia);
            cmd.Parameters.Add("@Original_MaHoaDon", SqlDbType.NVarChar);
            cmd.Parameters["@Original_MaHoaDon"].Value = dataGridView.CurrentRow.Cells[0].Value;


            cmd.ExecuteNonQuery();
            c.disconnect();
            HienThi();
        }

        private void buttonXoa_Click_1(object sender, EventArgs e)
        {
            DialogResult D = MessageBox.Show("Bạn có chắc muốn xa mã hoá đơn " + textBoxMaHoaDon.Text, "Chú ý",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (D == DialogResult.Yes)
            {

                c.connect();

                string query = @"DELETE FROM ChiTietHoaDon
                    WHERE (MaHoaDon = @Original_MaHoaDon) AND (MaThuoc = (Original_MaThuoc)";
                SqlCommand cmd = new SqlCommand(query, c.conn);


                cmd.Parameters.Add("@Original_MaHoaDon", SqlDbType.NVarChar);
                cmd.Parameters["@Original_MaHoaDon"].Value = dataGridView.CurrentRow.Cells[0].Value;

                cmd.ExecuteNonQuery();
                c.disconnect();
                HienThi();
            }
        }

        private void buttonThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonXuatExel_Click_1(object sender, EventArgs e)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);

                Excel._Worksheet worksheet = (Excel._Worksheet)excelApp.ActiveSheet;
                worksheet.Name = "ChiTietHoaDon";

                // Tiêu đề
                string tieuDe = "BÁO CÁO THỐNG KÊ HÓA ĐƠN THEO ĐƠN HÀNG";
                worksheet.Cells[1, 1] = tieuDe;

                Excel.Range titleRange = worksheet.Range[
                    worksheet.Cells[1, 1],
                    worksheet.Cells[1, dataGridView.Columns.Count]
                ];
                titleRange.Merge();
                titleRange.Font.Size = 16;
                titleRange.Font.Bold = true;
                titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // Tên cột
                for (int i = 1; i <= dataGridView.Columns.Count; i++)
                {
                    worksheet.Cells[3, i] = dataGridView.Columns[i - 1].HeaderText;
                }

                // Dữ liệu
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        if (dataGridView.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 4, j + 1] =
                                dataGridView.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                // Kẻ bảng
                Excel.Range dataRange = worksheet.Range[
                    worksheet.Cells[3, 1],
                    worksheet.Cells[dataGridView.Rows.Count + 3, dataGridView.Columns.Count]
                ];
                dataRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                dataRange.Borders.Color = System.Drawing.Color.Black.ToArgb();

                // Tự co giãn
                dataRange.EntireColumn.AutoFit();

                // Hiển thị Excel
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xuất Excel thất bại: " + ex.Message);
            }

        }
    }
}
        

