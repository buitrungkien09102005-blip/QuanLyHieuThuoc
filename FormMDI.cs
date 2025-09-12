using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace QuanLyHieuThuoc
{
    public partial class FormMDI : Form
    {
        public FormMDI()
        {
            InitializeComponent();
            menuStrip1.BringToFront();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangNhap dangNhap = new DangNhap();
            dangNhap.MdiParent = this;
            dangNhap.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NhanVien nhanVien = new NhanVien();
            nhanVien.MdiParent = this;
            nhanVien.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KhachHang khachHang = new KhachHang();
            khachHang.MdiParent = this;
            khachHang.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DangNhap dangNhap = new DangNhap();
            dangNhap.Show();
            this.Hide();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMDI_Load(object sender, EventArgs e)
        {
            
        }

        private void danhMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void danhMụcToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Form form = new Form();
            switch (e.ClickedItem.Name)
            {
                case "MenuNhanVien":
                    NhanVien nhanVien = new NhanVien();
                    form = nhanVien;
                    break;

                case "MenuKhachHang":
                    KhachHang khachHang = new KhachHang();
                    form = khachHang;
                    break;

                case "MenuThuoc":
                    Thuoc thuoc = new Thuoc();
                    form = thuoc;
                    break;
            }  
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
            form.BringToFront();
        }

        private void thuốcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thuoc thuoc = new Thuoc();
            thuoc.MdiParent = this;
            thuoc.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void thốngKêTheoNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKeNhanVien thongKe = new ThongKeNhanVien();
            thongKe.MdiParent = this;
            thongKe.Show();
        }

        private void thốngKêTheoNhânViênToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ThốngKêToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Form form = new Form();
            switch (e.ClickedItem.Name)
            {
                case "MenuThongKeNhanVien":
                    ThongKeNhanVien thongKe = new ThongKeNhanVien();
                    form = thongKe;
                    break;

            }
            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
            form.BringToFront();
        }
    }
}
