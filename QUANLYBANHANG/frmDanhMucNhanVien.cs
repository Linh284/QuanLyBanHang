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
    public partial class frmDMNVien : Form
    {
        DataTable tblNV;
        public frmDMNVien()
        {
            InitializeComponent();
        }

        private void frmDMNVien_Load(object sender, EventArgs e)
        {
            txtMaNhanVien.Enabled = false;
            btnLuu.Enabled = btnBoQua.Enabled = false;
            Load_Bang_Nhan_Vien();
        }

        private void Load_Bang_Nhan_Vien()
        {
            string sql = "select MaNhanVien,TenNhanVien,GioiTinh,DiaChi,DienThoai,NgaySinh from tblNhanVien";
            tblNV = Functions.GetDataTable(sql);
            dtgvNhanVien.DataSource = tblNV;

            dtgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dtgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dtgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dtgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dtgvNhanVien.Columns[4].HeaderText = "Điện thoại";
            dtgvNhanVien.Columns[5].HeaderText = "Ngày sinh";
            dtgvNhanVien.AllowUserToAddRows = false;
            dtgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dtgvNhanVien_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if(tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNhanVien.Text = dtgvNhanVien.CurrentRow.Cells["MaNhanVien"].Value.ToString();
            txtTenNhanVien.Text = dtgvNhanVien.CurrentRow.Cells["TenNhanVien"].Value.ToString();
            if (dtgvNhanVien.CurrentRow.Cells["GioiTinh"].Value.ToString() == "Nam")
            {
                chGioiTinh.Checked = true;
            }
            else chGioiTinh.Checked = false;
            txtDiaChi.Text = dtgvNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            mtbDienThoai.Text = dtgvNhanVien.CurrentRow.Cells["DienThoai"].Value.ToString();
            dtpNgaySinh.Value = (DateTime) dtgvNhanVien.CurrentRow.Cells["NgaySinh"].Value;
            btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = btnThem.Enabled = false;
            btnBoQua.Enabled = btnLuu.Enabled = true;
            ResetValues();
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Focus();
        }

        private void ResetValues()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            chGioiTinh.Checked = false;
            txtDiaChi.Text = "";
            mtbDienThoai.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if(txtMaNhanVien.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if(txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNhanVien.Focus();
                return;
            }
            if(txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if(mtbDienThoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }
            if (chGioiTinh.Checked == true)
            {
                gt = "Nam";
            }
            else gt = "Nữ";
            sql = "select MaNhanVien from tblNhanVien where MaNhanVien = N'" + txtMaNhanVien.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                txtMaNhanVien.Text = "";
                return;
            }
            sql = "INSERT INTO tblNhanVien (MaNhanVien,TenNhanVien,GioiTinh,DiaChi,DienThoai,NgaySinh) values (N'" + txtMaNhanVien.Text + "',N'" + txtTenNhanVien.Text + "',N'" + gt + "',N'" + txtDiaChi.Text + "', '" + mtbDienThoai.Text + "','" + dtpNgaySinh.Value + "')";
            Functions.RunSQL(sql);
            Load_Bang_Nhan_Vien();
            ResetValues();
            btnXoa.Enabled = btnThem.Enabled = btnSua.Enabled = true;
            btnBoQua.Enabled = btnLuu.Enabled = false;
            txtMaNhanVien.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if(txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoai.Focus();
                return;
            }
            if (chGioiTinh.Checked == true)
            {
                gt = "Nam";
            }
            else gt = "Nữ";
            sql = "update tblNhanVien set TenNhanVien = N'" + txtTenNhanVien.Text +
                                          "',DiaChi = N'" + txtDiaChi.Text +
                                          "',DienThoai = N'" + mtbDienThoai.Text +
                                          "',GioiTinh = N'" + gt +
                                          "',NgaySinh = N'" + dtpNgaySinh.Value +
                                          "' where MaNhanVien = N'" + txtMaNhanVien.Text + "'";
            Functions.RunSQL(sql);
            Load_Bang_Nhan_Vien();
            ResetValues();
            btnBoQua.Enabled = false;
         }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return;
            }
            if(MessageBox.Show("Bạn có muốn xóa không?","Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "delete tblNhanVien where MaNhanVien = N'" + txtMaNhanVien.Text + "'";
                Functions.RunSqlDel(sql);
                Load_Bang_Nhan_Vien();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = btnLuu.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = true;
            txtMaNhanVien.Enabled = false;
        }

        private void txtMaNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtTenNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void mtbDienThoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void dtpNgaySinh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void txtTenNhanVien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenNhanVien, "");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenNhanVien, "Nhập lại");
            }
        }

        private void txtDiaChi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtDiaChi, "");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtDiaChi, "Nhập lại");
            }
        }

        private void txtDiaChi_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
