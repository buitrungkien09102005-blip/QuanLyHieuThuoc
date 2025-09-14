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
using Excel = Microsoft.Office.Interop.Excel;


namespace QuanLyHieuThuoc
{
    public partial class ThongKeDonHang : Form
    {
        public ThongKeDonHang()
        {
            InitializeComponent();
        }

        Connect c = new Connect();

        private void ThongKeDonHang_Load(object sender, EventArgs e)
        {
            comboBoxMaHoaDon.Items.Clear();
            c.connect();
            string query = "SELECT MaHoaDon FROM HoaDonBan";
            SqlCommand cmd = new SqlCommand(query, c.conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                comboBoxMaHoaDon.Items.Add(reader[0]);
                i++;
            }
            c.disconnect();
        }

        private void buttonThongKe_Click(object sender, EventArgs e)
        {
            if (comboBoxMaHoaDon.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn mã hóa đơn!");
                return;
            }
            c.connect();

            string query = @"SELECT hd.MaHoaDon, cthd.MaThuoc, h.TenThuoc, hd.NgayBan, 
                            cthd.SoLuong, cthd.DonGia, cthd.GiamGia,
                            (cthd.SoLuong * cthd.DonGia - cthd.GiamGia) AS ThanhTien
                     FROM ChiTietHoaDon cthd
                     JOIN Thuoc h ON cthd.MaThuoc = h.MaThuoc
                     JOIN HoaDonBan hd ON cthd.MaHoaDon = hd.MaHoaDon
                     WHERE cthd.MaHoaDon = @MaHoaDon";

            SqlCommand cmd = new SqlCommand(query, c.conn);
            cmd.Parameters.AddWithValue("@MaHoaDon", comboBoxMaHoaDon.SelectedItem.ToString());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView.DataSource = dt;

            dataGridView.Columns["MaHoaDon"].HeaderText = "Mã hóa đơn";
            dataGridView.Columns["MaThuoc"].HeaderText = "Mã thuốc";
            dataGridView.Columns["TenThuoc"].HeaderText = "Tên thuốc";
            dataGridView.Columns["NgayBan"].HeaderText = "Ngày bán";
            dataGridView.Columns["SoLuong"].HeaderText = "Số lượng";
            dataGridView.Columns["DonGia"].HeaderText = "Đơn giá";
            dataGridView.Columns["GiamGia"].HeaderText = "Giảm giá";
            dataGridView.Columns["ThanhTien"].HeaderText = "Thành tiền";

            c.disconnect();
        }

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxMaHoaDon.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn cần tìm!");
                return;
            }
            c.connect();

            string query = @"SELECT hd.MaHoaDon, cthd.MaThuoc, h.TenThuoc, hd.NgayBan, 
                            cthd.SoLuong, cthd.DonGia, cthd.GiamGia,
                            (cthd.SoLuong * cthd.DonGia - cthd.GiamGia) AS ThanhTien
                     FROM ChiTietHoaDon cthd
                     JOIN Thuoc h ON cthd.MaThuoc = h.MaThuoc
                     JOIN HoaDonBan hd ON cthd.MaHoaDon = hd.MaHoaDon
                     WHERE cthd.MaHoaDon = @MaHoaDon";

            SqlCommand cmd = new SqlCommand(query, c.conn);
            cmd.Parameters.AddWithValue("@MaHoaDon", textBoxMaHoaDon.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView.DataSource = dt;

            c.disconnect();
        }

        private void buttonXuatExel_Click(object sender, EventArgs e)
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

                // Tự co dãn
                dataRange.EntireColumn.AutoFit();

                // Hiển thị Excel
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xuất Excel thất bại: " + ex.Message);
            }

        }

        private void buttonThoát_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
