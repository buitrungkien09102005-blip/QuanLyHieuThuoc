using System;
using System.Collections;
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
    public partial class ThongKeNhanVien : Form
    {
        public ThongKeNhanVien()
        {
            InitializeComponent();
        }

        Connect c = new Connect();

        private void LoadNhanVien()
        {
            c.connect();
            string query = "SELECT MaNV, TenNV FROM NhanVien";
            SqlDataAdapter da = new SqlDataAdapter(query, c.conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBoxMaNV.DataSource = dt;
            comboBoxMaNV.DisplayMember = "MaNV";
            comboBoxMaNV.ValueMember = "MaNV";

            textBoxTenNV.DataBindings.Clear();
            textBoxTenNV.DataBindings.Add("Text", dt, "TenNV");

        }

        private void ThongKeNhanVien_Load(object sender, EventArgs e)
        {
            /*comboBoxMaNV.Items.Clear();
            //SqlConnection conn = new SqlConnection(conStr);
            string query = @"SELECT MaNV
                            FROM   NhanVien";
            c.connect();
            SqlCommand cmd = new SqlCommand(query, c.conn);
            SqlDataReader read = cmd.ExecuteReader();
            int i = 0;
            while (read.Read())
            {
                comboBoxMaNV.Items.Add(read[0]);
                i++;
            }
            c.disconnect();*/

            LoadNhanVien();
            
        }

        private void buttonThongKe_Click(object sender, EventArgs e)
        {
            c.connect();
            string maNV = comboBoxMaNV.SelectedValue.ToString();
            DateTime tuNgay = dateTimePickerTuNgay.Value;
            DateTime denNgay = dateTimePickerDenNgay.Value;

            /*string query = @"SELECT MaHoaDon, NgayBan, MaKH, MaNV, TongTien, GhiChu
                     FROM HoaDonBan
                     WHERE MaNV = @maNV AND NgayBan BETWEEN @tuNgay AND @denNgay";*/

            string query = @"SELECT hd.MaNV, nv.TenNV, hd.NgayBan, hd.TongTien
                               FROM HoaDonBan hd
                               JOIN NhanVien nv ON hd.MaNV = nv.MaNV
                               WHERE hd.MaNV = @MaNV 
                                     AND hd.NgayBan BETWEEN @TuNgay AND @DenNgay";

            SqlCommand cmd = new SqlCommand(query, c.conn);
            cmd.Parameters.AddWithValue("@maNV", maNV);
            cmd.Parameters.AddWithValue("@tuNgay", tuNgay);
            cmd.Parameters.AddWithValue("@denNgay", denNgay);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView.DataSource = dt;

            // Tính tổng tiền
            decimal tong = 0;
            foreach (DataRow row in dt.Rows)
            {
                tong += Convert.ToDecimal(row["TongTien"]);
            }
            textBoxTongTien.Text = "Tổng doanh thu: " + tong.ToString("N0") + " VNĐ";
        }

        private void buttonLamMoi_Click(object sender, EventArgs e)
        {
            comboBoxMaNV.SelectedIndex = -1;
            textBoxTenNV.Clear();
            dateTimePickerTuNgay.Value = DateTime.Now;
            dateTimePickerDenNgay.Value = DateTime.Now;
            dataGridView.DataSource = null;
            textBoxTongTien.Clear();
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
