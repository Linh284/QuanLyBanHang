using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QUANLYBANHANG
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            frmDanhMucChatLieu frmCl = new frmDanhMucChatLieu();
            frmCl.MdiParent = this;
            frmCl.Show();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            frmDMNVien frmNV = new frmDMNVien();
            frmNV.MdiParent = this;
            frmNV.Show();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmDanhMucKhachHang frmKH = new frmDanhMucKhachHang();
            frmKH.MdiParent = this;
            frmKH.Show();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            frmDanhMucHangHoa frmHH = new frmDanhMucHangHoa();
            frmHH.MdiParent = this;
            frmHH.Show();
        }

        private void hóaĐơnBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonBanHang frmHDB = new frmHoaDonBanHang();
            frmHDB.MdiParent = this;
            frmHDB.Show();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimHDBan frmTHDB= new frmTimHDBan();
            frmTHDB.MdiParent = this;
            frmTHDB.Show();
        }

        private void hàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimHang frmTimHang = new frmTimHang();
            frmTimHang.MdiParent = this;
            frmTimHang.Show();
        }

        private void doanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void hàngTồnToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void thànhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThanhVien frmTV = new frmThanhVien();
            frmTV.MdiParent = this;
            frmTV.Show();
        }
    }
}
