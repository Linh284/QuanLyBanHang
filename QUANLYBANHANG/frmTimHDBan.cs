using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QUANLYBANHANG.Class;

namespace QUANLYBANHANG
{
    public partial class frmTimHDBan : Form
    {
        DataTable tblHDB;
        public frmTimHDBan()
        {
            InitializeComponent();
        }

        private void frmTimHDBan_Load(object sender, EventArgs e)
        {
            ResetValues();
            dtgvTKHoaDon.DataSource = null;
        }

        private void ResetValues()
        {
            foreach(Control Ctl in this.Controls)
            {
                if (Ctl is TextBox)
                    Ctl.Text = "";
                txtMaHoaDon.Focus();
            }
        }

        private void Load_HD()
        {
            dtgvTKHoaDon.Columns[0].HeaderText = "Mã HĐB";
            dtgvTKHoaDon.Columns[0].HeaderText = "Mã nhân viên";
            dtgvTKHoaDon.Columns[0].HeaderText = "Ngày bán";
            dtgvTKHoaDon.Columns[0].HeaderText = "Mã khách";
            dtgvTKHoaDon.Columns[0].HeaderText = "Tổng tiền";
            dtgvTKHoaDon.Columns[0].Width = 250;
            dtgvTKHoaDon.Columns[1].Width = 200;
            dtgvTKHoaDon.Columns[2].Width = 200;
            dtgvTKHoaDon.Columns[3].Width = 200;
            dtgvTKHoaDon.Columns[4].Width = 200;
            dtgvTKHoaDon.AllowUserToAddRows = false;
            dtgvTKHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dtgvTKHoaDon.DataSource = null;
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (Convert.ToInt32(e.KeyChar) == 8)) { 
                e.Handled = false;
            } else e.Handled = true;
        }

        private void dtgvTKHoaDon_DoubleClick(object sender, EventArgs e)
        {
            string mahd;
            if (MessageBox.Show("Bạn có muốn hiển thị thông tin chi tiết?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                mahd = dtgvTKHoaDon.CurrentRow.Cells["MaHDBan"].Value.ToString();
                frmTimHDBan frm = new frmTimHDBan();
                frm.txtMaHoaDon.Text = mahd;
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHoaDon.Text == "") && (txtThang.Text == "") && (txtNam.Text == "") && (txtMaKhachHang.Text == "") && (txtTongTien.Text == "") && (txtMaKhachHang.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!", "Yêu cầu .. ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select * from tblHDBan where 1=1";
            if (txtMaHoaDon.Text != "")
            {
                sql = sql + "and MaHDBan like N'%" + txtMaHoaDon.Text + "%'";
            }
            if (txtThang.Text != "")
            {
                sql = sql + "and Month(NgayBan) = " + txtThang.Text;
            }
            if (txtNam.Text != "")
            {
                sql = sql + "and Year(NgayBan) = " + txtNam.Text;
            }
            if (txtMaNhanVien.Text != "")
            {
                sql = sql + "and MaNhanVien like N'%" + txtMaNhanVien.Text + "'";
            }
            if (txtMaKhachHang.Text != "")
            {
                sql = sql + "and MaKhach like N'%" + txtMaKhachHang.Text + "'";
            }
            if (txtTongTien.Text != "")
            {
                sql = sql + "and TongTien <=" + txtTongTien.Text;
            }
            tblHDB = Functions.GetDataTable(sql);
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông baó", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có " + tblHDB.Rows.Count + " bản ghi thỏa mãn điều kiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dtgvTKHoaDon.DataSource = tblHDB;
            Load_HD();
        }

        private void txtThang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtThang, "Nhap lai thang");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtThang, "");
            }
        }

        private void txtNam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtNam, "Nhap lai nam");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtNam, "");
            }
        }

        private void txtMaNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtMaNhanVien, "");
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(txtMaNhanVien, "Nhập lại");
            }
        }

        private void txtMaKhachHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtMaKhachHang, "");
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(txtMaKhachHang, "Nhập lại");
            }
        }
    }
}
