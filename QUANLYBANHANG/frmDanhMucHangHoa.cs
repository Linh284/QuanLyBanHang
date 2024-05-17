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
    public partial class frmDanhMucHangHoa : Form
    {
        DataTable tblHH;
        public frmDanhMucHangHoa()
        {
            InitializeComponent();
        }
        void phanquyen()
        {
            if (fLoaiTK.LoaiTaiKhoan != 1)
            {
                btnThem.Enabled = false;
            }
        }
        private void frmDanhMucHangHoa_Load(object sender, EventArgs e)
        {
            string sql = "select * from tblChatLieu";
            txtMaHang.Enabled = btnLuu.Enabled = btnBoQua.Enabled = false;
            Load_Bang_HangHoa();
            Functions.FillCombo(sql, cbbMaChatLieu, "MaChatLieu", "TenChatLieu");
            cbbMaChatLieu.SelectedIndex = -1;
            ResetValues();
        }

        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cbbMaChatLieu.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtAnh.Text = "";
            pic.Image = null;
            txtGhiChu.Text = "";
        }

        private void Load_Bang_HangHoa()
        {
            phanquyen();
            string sql = "select * from tblHang";
            tblHH = Functions.GetDataTable(sql);
            dtgvHang.DataSource = tblHH;
            dtgvHang.Columns[0].HeaderText = "Mã hàng";
            dtgvHang.Columns[1].HeaderText = "Tên hàng";
            dtgvHang.Columns[2].HeaderText = "Chất liệu";
            dtgvHang.Columns[3].HeaderText = "Số lượng";
            dtgvHang.Columns[4].HeaderText = "Đơn giá nhập";
            dtgvHang.Columns[5].HeaderText = "Đơn giá bán";
            dtgvHang.Columns[6].HeaderText = "Ảnh";
            dtgvHang.Columns[7].HeaderText = "Ghi chú";

            dtgvHang.Columns[0].Width = 80;
            dtgvHang.Columns[1].Width = 200;
            dtgvHang.Columns[2].Width = 80;
            dtgvHang.Columns[3].Width = 80;
            dtgvHang.Columns[4].Width = 100;
            dtgvHang.Columns[5].Width = 100;
            dtgvHang.Columns[6].Width = 200;
            dtgvHang.Columns[7].Width = 300;

            dtgvHang.AllowUserToAddRows = false;
            dtgvHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if(txtMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if(txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if(cbbMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh họa cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMaChatLieu.Focus();
                return;
            }
            if(txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh họa cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpen.Focus();
                return;
            }
            sql = "select MaHang from tblHang where MaHang = N'" + txtMaHang.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng đã tồn tại, bạn phải chọn mã hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            sql = "INSERT INTO tblHang (MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap,DonGiaBan,Anh,GhiChu)" +
                " values (N'" + txtMaHang.Text + "',N'" + txtTenHang.Text + "',N'" + cbbMaChatLieu.SelectedValue.ToString() + "'," + txtSoLuong.Text +
                "," + txtDonGiaNhap.Text + "," + txtDonGiaBan.Text + ",'" + txtAnh.Text + "',N'" + txtGhiChu.Text + "')";
            Functions.RunSQL(sql);
            Load_Bang_HangHoa();
            // ResetValues();
            btnXoa.Enabled = btnThem.Enabled = btnSua.Enabled = true;
            btnBoQua.Enabled = btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if(txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return;
            }
            if(cbbMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMaChatLieu.Focus();
                return;
            }
            if(txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải ảnh minh họa cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAnh.Focus();
                return;
            }
            sql = "update tblHang set TenHang = N'" + txtTenHang.Text +
                                "',MaChatLieu = N'" + cbbMaChatLieu.SelectedValue.ToString() +
                                "',SoLuong = " + txtSoLuong.Text + ",Anh ='" + txtAnh.Text +
                                "', GhiChu = N'" + txtGhiChu.Text +
                                "' where MaHang = N'" + txtMaHang.Text + "'";
            Functions.RunSQL(sql);
            Load_Bang_HangHoa();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblHH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show("Bạn có muốn xóa bản ghi này ?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "delete tblHang where MaHang = N'" + txtMaHang.Text + "'";
                Functions.RunSQL(sql);
                Load_Bang_HangHoa();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            if(fLoaiTK.LoaiTaiKhoan != 1)
                btnThem.Enabled = false;
            else btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaHang.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            file.FilterIndex = 2;
            file.Title = "Chọn ảnh minh họa cho sản phẩm";
            if(file.ShowDialog() == DialogResult.OK)
            {
                pic.Image = Image.FromFile(file.FileName);
                txtAnh.Text = file.FileName;
            }
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql = "select MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap,DonGiaBan,Anh,GhiChu from tblHang";
            tblHH = Functions.GetDataTable(sql);
            dtgvHang.DataSource = tblHH;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaChatLieu, sql;
            if (btnThem.Enabled == false)
            {
                if (fLoaiTK.LoaiTaiKhoan != 1)
                {
                }
                else
                {
                    MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaHang.Focus();
                    return;
                }
            }
            if (tblHH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHang.Text = dtgvHang.CurrentRow.Cells["MaHang"].Value.ToString();
            txtTenHang.Text = dtgvHang.CurrentRow.Cells["TenHang"].Value.ToString();
            MaChatLieu = dtgvHang.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            sql = "select TenChatLieu from tblChatLieu where MaChatLieu = N'" + MaChatLieu + "'";
            cbbMaChatLieu.Text = Functions.GetFieldValues(sql);
            txtSoLuong.Text = dtgvHang.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGiaBan.Text = dtgvHang.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            txtDonGiaNhap.Text = dtgvHang.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            sql = "select Anh from tblHang where MaHang = N'" + txtMaHang.Text + "'";
            txtAnh.Text = Functions.GetFieldValues(sql);
            pic.Image = Image.FromFile(txtAnh.Text);
            sql = "select GhiChu from tblHang where MaHang = N'" + txtMaHang.Text + "'";
            txtGhiChu.Text = Functions.GetFieldValues(sql);
            btnSua.Enabled = btnXoa.Enabled = btnBoQua.Enabled = true;
        }

        private void txtMaHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtMaHang, "");
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(txtMaHang, "Nhập lại");
            }
        }

        private void txtTenHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenHang, "");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenHang, "Nhập lại");
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtSoLuong, "Nhap lai so luong");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtSoLuong, "");
            }
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtDonGiaNhap, "Nhap lai don gia nhap");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtDonGiaNhap, "");
            }
        }

        private void txtDonGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                errorProvider1.SetError(txtDonGiaBan, "Nhap lai don gia ban");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtDonGiaBan, "");
            }
        }
    }
}
