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
using QUANLYBANHANG.Class; // sử dụng class Functions.cs

namespace QUANLYBANHANG
{
    public partial class frmDanhMucChatLieu : Form
    {
        #region Biến
        DataTable tblCL; // chứa dữ liệu bảng chất liệu

        #endregion
        public frmDanhMucChatLieu()
        {
            InitializeComponent();
        }

        private void frmDanhMucChatLieu_Load(object sender, EventArgs e)
        {
            txtMaChatLieu.Enabled = false;
            btnLuu.Enabled = btnBoQua.Enabled = false ;
            Load_Bang_Chat_Lieu();
        }

        private void Load_Bang_Chat_Lieu()
        {
            string sql = "select MaChatlieu,TenChatLieu from tblChatLieu";
            tblCL = Class.Functions.GetDataTable(sql);
            dtgvChatLieu.DataSource = tblCL;
            dtgvChatLieu.Columns[0].HeaderText = "Mã chất liệu";
            dtgvChatLieu.Columns[1].HeaderText = "Tên chất liệu";

            dtgvChatLieu.AllowUserToAddRows = false; // khong cho nguoi dung them
            dtgvChatLieu.EditMode = DataGridViewEditMode.EditProgrammatically; 
            // khong cho sua du lieu truc tiep
        }

        private void dtgvChatLieu_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaChatLieu.Focus();
                return;
            }
            if(tblCL.Rows.Count == 0){ // neu khong co du lieu
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaChatLieu.Text = dtgvChatLieu.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            txtTenChatLieu.Text = dtgvChatLieu.CurrentRow.Cells["TenChatLieu"].Value.ToString();
            btnSua.Enabled = btnXoa.Enabled = btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = btnThem.Enabled = false;
            btnBoQua.Enabled = btnLuu.Enabled = true;
            ResetValue();
            txtMaChatLieu.Enabled = true;
            txtMaChatLieu.Focus();
        }

        private void ResetValue()
        {
            txtMaChatLieu.Text = "";
            txtTenChatLieu.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if(txtMaChatLieu.Text.Trim().Length == 0) // neu chua nhap ma chat lieu
            {
                MessageBox.Show("Bạn phải nhập mã chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaChatLieu.Focus();
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0) // neu chua nhap ten chat lieu
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenChatLieu.Focus();
                return;
            }

            sql = "select MaChatLieu from tblChatLieu where MaChatLieu = N'" + txtMaChatLieu.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql)) {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaChatLieu.Focus();
                return;
            }

            sql = "insert into tblChatLieu values (N'" + txtMaChatLieu.Text + "', N'" + txtTenChatLieu.Text + "')";
            Class.Functions.RunSQL(sql);
            Load_Bang_Chat_Lieu();
            ResetValue();
            btnXoa.Enabled = btnThem.Enabled = btnSua.Enabled = true;
            btnBoQua.Enabled = btnLuu.Enabled = false;
            txtMaChatLieu.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtMaChatLieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(txtTenChatLieu.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "update tblChatLieu set TenChatLieu = N'" + txtTenChatLieu.Text.ToString() + "' where MaChatLieu = N'" + txtMaChatLieu.Text + "'";
            Class.Functions.RunSQL(sql);
            Load_Bang_Chat_Lieu();
            ResetValue();

            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if(tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaChatLieu.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(MessageBox.Show("Bạn có muốn xóa không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "delete tblChatLieu where MaChatLieu = N'" + txtMaChatLieu.Text + "'";
                Class.Functions.RunSqlDel(sql);
                Load_Bang_Chat_Lieu();
                ResetValue();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoQua.Enabled = btnLuu.Enabled = false;
            btnThem.Enabled = btnXoa.Enabled = btnSua.Enabled = true;
            txtMaChatLieu.Enabled = false;
        }

        private void txtMaChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtTenChatLieu_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaChatLieu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtMaChatLieu, "");
            }
            else
            {
                e.Handled = true;
                errorProvider1.SetError(txtMaChatLieu, "Nhập lại");
            }
        }

        private void txtTenChatLieu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) || Char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenChatLieu, "");
            }
            else
            {
                e.Handled = false;
                errorProvider1.SetError(txtTenChatLieu, "Nhập lại");
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
