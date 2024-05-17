using QUANLYBANHANG.Class;
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
    public partial class frmDanhMucKhachHang : Form
    {
        DataTable tblKH;
        public frmDanhMucKhachHang()
        {
            InitializeComponent();
        }

        private void frmDanhMucKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKhach.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            Load_Bang_KhachHang();
        }

        private void Load_Bang_KhachHang()
        {
            string sql = "select * from tblKhach";
            tblKH = Functions.GetDataTable(sql);
            dtgvKhachHang.DataSource = tblKH;
            dtgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dtgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dtgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dtgvKhachHang.Columns[3].HeaderText = "Điện thoại";
            dtgvKhachHang.AllowUserToAddRows = false;
            dtgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dtgvKhachHang_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKhach.Text = dtgvKhachHang.CurrentRow.Cells["MaKhach"].Value.ToString();
            txtTenKhach.Text = dtgvKhachHang.CurrentRow.Cells["TenKhach"].Value.ToString();
            txtDiaChi.Text = dtgvKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
            mtbDienThoai.Text = dtgvKhachHang.CurrentRow.Cells["DienThoai"].Value.ToString();
            btnSua.Enabled = btnXoa.Enabled = btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = false;
            btnBoQua.Enabled = btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMaKhach.Enabled = true;
            txtMaKhach.Focus();
        }

        private void ResetValue()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtDiaChi.Text = "";
            mtbDienThoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if(txtMaKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhach.Focus();
                return;
            }
            if (txtTenKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhach.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }
            // check 
            sql = "select MaKhach from tblKhach where MaKhach = N'" + txtMaKhach.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhach.Focus();
                return;
            }
            // chèn thêm
            sql = "insert into tblKhach values (N'" + txtMaKhach.Text +
                                            "',N'" + txtTenKhach.Text +
                                            "',N'" + txtDiaChi.Text +
                                            "','" + mtbDienThoai.Text + "')";
            Functions.RunSQL(sql);
            Load_Bang_KhachHang();
            ResetValue();

            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = true;
            btnBoQua.Enabled = btnLuu.Enabled = false;
            txtMaKhach.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaKhach.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show("Bạn có chắc muốn xóa?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "delete tblKhach where MaKhach = N'" + txtMaKhach.Text + "'";
                Functions.RunSqlDel(sql);
                Load_Bang_KhachHang();
                ResetValue();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaKhach.Enabled = false;
        }

        private void txtMaKhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaKhach.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtTenKhach.Text.Trim().Length  == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhach.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (mtbDienThoai.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
                return;
            }
            sql = "update tblKhach set TenKhach = N'" + txtTenKhach.Text +
                                       "',DiaChi = N'" + txtDiaChi.Text +
                                       "',DienThoai = '" + mtbDienThoai.Text +
                    "' where MaKhach = N'" + txtMaKhach.Text + "'";
            Functions.RunSQL(sql);
            Load_Bang_KhachHang();
            ResetValue();
            btnBoQua.Enabled = false;
        }

        private void txtMaKhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtMaKhach, "");
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(txtMaKhach, "Nhập lại");
            }
        }

        private void txtTenKhach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenKhach, "");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenKhach, "Nhập lại");
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
    }
}
