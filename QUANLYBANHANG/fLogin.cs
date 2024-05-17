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
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTaiKhoan.Text;
            string matKhau = txtMatKhau.Text;
            if (Login(taiKhoan,matKhau))
            {
                fLoaiTK.LoaiTaiKhoan = TakeResult(taiKhoan,matKhau);
                frmMain f = new frmMain();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
            }
        }
        int TakeResult(string  userName,string passWord)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable result = Class.Functions.ExcuteQuery(query,new object[] {userName,passWord});
            if(result.Rows.Count > 0)
            {
                return Convert.ToInt32(result.Rows[0]["Type"]);
            }
            else
            {
                return -1;
            }
        }
        bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable result = Class.Functions.ExcuteQuery(query,new object[] {userName,passWord});
            return result.Rows.Count > 0;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect(); // mo ket noi du lieu
        }
    }
}
