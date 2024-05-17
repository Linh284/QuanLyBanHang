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
    public partial class frmTimHang : Form
    {
        DataTable tblHang;
        public frmTimHang()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaHang.Text == "" && txtTenHang.Text == "")
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!", "Yêu cầu .. ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "select * from tblHang where 1=1";
            if (txtMaHang.Text != "")
            {
                sql += sql + "and MaHang like N'%" + txtMaHang.Text + "%'";
            }
            if (txtTenHang.Text != "")
            {
                sql += sql + "and TenHang like N'%" + txtTenHang.Text +"%'";
            }
            if (cbbMaChatLieu.Text != "")
            {
                sql += "and MaChatLieu like N'%" + cbbMaChatLieu.SelectedValue + "%'";
            }
            tblHang = Functions.GetDataTable(sql);
            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào thỏa mãn", "Thông baó", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có " + tblHang.Rows.Count + " bản ghi thỏa mãn điều kiện.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dtgvHang.DataSource = tblHang;
            Load_Hang();
        }
        public void Load_Hang()
        {
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
        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
            {
                if (Ctl is TextBox)
                    Ctl.Text = "";
                txtMaHang.Focus();
            }
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dtgvHang.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTimHang_Load(object sender, EventArgs e)
        {
            string sql = "select * from tblChatLieu";
            Functions.FillCombo(sql, cbbMaChatLieu, "MaChatLieu", "TenChatLieu");
            cbbMaChatLieu.SelectedIndex = -1;
        }

        private void dtgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaChatLieu, sql;
            if (tblHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaHang.Text = dtgvHang.CurrentRow.Cells["MaHang"].Value.ToString();
            txtTenHang.Text = dtgvHang.CurrentRow.Cells["TenHang"].Value.ToString();
            MaChatLieu = dtgvHang.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            sql = "select TenChatLieu from tblChatLieu where MaChatLieu = N'" + MaChatLieu + "'";
            cbbMaChatLieu.Text = Functions.GetFieldValues(sql);
            sql = "select Anh from tblHang where MaHang = N'" + txtMaHang.Text + "'";
            txtAnh.Text = Functions.GetFieldValues(sql);
            pic.Image = Image.FromFile(txtAnh.Text);
            sql = "select GhiChu from tblHang where MaHang = N'" + txtMaHang.Text + "'";
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
                e.Handled = true;
                errorProvider1.SetError(txtTenHang, "Nhập lại");
            }
        }
    }
}
