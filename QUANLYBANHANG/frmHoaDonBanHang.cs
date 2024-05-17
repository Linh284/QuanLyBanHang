using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExecl = Microsoft.Office.Interop.Excel;
using QUANLYBANHANG.Class;

namespace QUANLYBANHANG
{
    public partial class frmHoaDonBanHang : Form
    {
        DataTable tblCTHDB;
        public frmHoaDonBanHang()
        {
            InitializeComponent();
        }

        private void frmHoaDonBanHang_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = btnIn.Enabled = false;
            btnXoa.Enabled = false;
            txtMaHoaDon.ReadOnly = true;
            txtTenNhanVien.ReadOnly = true;
            txtTenKhachHang.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            mtbDienThoai.ReadOnly = true;
            txtTenHang.Enabled = true;
            txtDonGia.Enabled = true;
            txtTongTien.Enabled = true;
            txtThanhTien.Enabled = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";
            Functions.FillCombo("select MaKhach,TenKhach from tblKhach", cbbMaKhachHang, "MaKhach", "MaKhach");
            cbbMaKhachHang.SelectedIndex = -1;
            Functions.FillCombo("select MaNhanVien,TenNhanVien from tblNhanVien", cbbMaNhanVien, "MaNhanVien", "MaNhanVien");
            cbbMaNhanVien.SelectedIndex = -1;
            Functions.FillCombo("select MaHang,TenHang from tblHang", cbbMatHang, "MaHang", "MaHang");
            cbbMatHang.SelectedIndex = -1;
            // HIển thị thông tin của 1 hóa đơn được gọi từ form tìm kiếm
            if (txtMaHoaDon.Text != "")
            {
                Load_ThongTin_Hoa_Don();
                btnIn.Enabled = true;
            }
            Load_dtgv();
        }

        private void Load_ThongTin_Hoa_Don()
        {
            string str = "select NgayBan from tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'";
            dtNgayban.Value = DateTime.Parse(Functions.GetFieldValues(str));
            str = "select MaNhanVien from tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'";
            cbbMaNhanVien.SelectedValue = Functions.GetFieldValues(str);
            str = "select MaKhach from tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'";
            cbbMaKhachHang.Text = Functions.GetFieldValues(str);
            str = "select TongTien from tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'";
            txtTongTien.Text = Functions.GetFieldValues(str);
            lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(double.Parse(txtTongTien.Text));
        }

        private void Load_dtgv()
        {
            string sql = "select hdb.MaHang,h.TenHang,hdb.SoLuong,h.DonGiaBan, hdb.GiamGia,hdb.ThanhTien from tblChiTietHDBan as hdb,tblHang as h where hdb.MaHDBan = N'" + txtMaHoaDon.Text + "' AND hdb.MaHang = h.MaHang";
            tblCTHDB = Functions.GetDataTable(sql);
            dtgv.DataSource = tblCTHDB;
            dtgv.Columns[0].HeaderText = "Mã hàng";
            dtgv.Columns[1].HeaderText = "Tên hàng";
            dtgv.Columns[2].HeaderText = "Số lượng";
            dtgv.Columns[3].HeaderText = "Đơn giá";
            dtgv.Columns[4].HeaderText = "Giảm giá %";
            dtgv.Columns[5].HeaderText = "Thành tiền";

            dtgv.Columns[0].Width = 150;
            dtgv.Columns[1].Width = 200;
            dtgv.Columns[2].Width = 120;
            dtgv.Columns[3].Width = 200;
            dtgv.Columns[4].Width = 190;
            dtgv.Columns[5].Width = 180;

            dtgv.AllowUserToAddRows = false;
            dtgv.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void ResetValueHang()
        {
            cbbMatHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "";
            txtThanhTien.Text = "";
        }

        private void dtgv_DoubleClick(object sender, EventArgs e)
        {
            string MaHangXoa, sql;
            Double ThanhTienXoa, SoLuongXoa, sl, slcon, tong, tongmoi;
            if (tblCTHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                MaHangXoa = dtgv.CurrentRow.Cells["MaHang"].Value.ToString();
                SoLuongXoa = Convert.ToDouble(dtgv.CurrentRow.Cells["SoLuong"].Value.ToString());
                ThanhTienXoa = Convert.ToDouble(dtgv.CurrentRow.Cells["ThanhTien"].Value.ToString());
                sql = "delete tblChiTietHDBan where MaHDBan =N'" + txtMaHoaDon.Text + "' and MaHang = N'" + MaHangXoa + "'";
                Functions.RunSQL(sql);
                // cap nhat lai so luong mat hang
                sl = Convert.ToDouble(Functions.GetFieldValues("select SoLuong from tblHang where MaHang = N'" + MaHangXoa + "'"));
                slcon = sl + SoLuongXoa;
                sql = "update tblHang set SoLuong = " + slcon + " where MaHang = N'" + MaHangXoa + "'";
                Functions.RunSQL(sql);
                // cap nhat lai tong tien cho hoa don ban
                tong = Convert.ToDouble(Functions.GetFieldValues("select TongTien from tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'"));
                tongmoi = tong - ThanhTienXoa;
                sql = "update tblHDBan set TongTien = " + tongmoi + "where MaHDBan = N'" + txtMaHoaDon.Text + "'";
                Functions.RunSQL(sql);
                txtTongTien.Text = tongmoi.ToString();
                lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(double.Parse(tongmoi.ToString()));
                Load_dtgv();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "select MaHang,SoLuong from tblChiTietHDBan where  MaHDBan =N'" + txtMaHoaDon.Text + "'";
                DataTable tblHang = Functions.GetDataTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // cap nhat lai so luong cho cac mat hang
                    sl = Convert.ToDouble(Functions.GetFieldValues("select SoLuong from tblHang where MaHang = N'" + tblHang.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    sql = "update tblHang set SoLuong = " + slcon + "where Mahang = N'" + tblHang.Rows[hang][0].ToString() + "'";
                    Functions.RunSQL(sql);
                }
                // xoa chi tiet hoa don
                sql = "delete tblChiTietHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'";
                Functions.RunSqlDel(sql);

                // xoa hoa don
                sql = "delete tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'";
                Functions.RunSqlDel(sql);
                ResetValues();
                Load_dtgv();
                btnXoa.Enabled = false;
                btnIn.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = btnIn.Enabled = btnThem.Enabled = false;
            btnLuu.Enabled = true;
            ResetValues();
            txtMaHoaDon.Text = Functions.CreateKey("HDB");
            Load_dtgv();
        }

        private void ResetValues()
        {
            txtMaHoaDon.Text = "";
            dtNgayban.Value = DateTime.Now;
            cbbMaNhanVien.Text = "";
            cbbMaKhachHang.Text = "";
            txtTongTien.Text = "0";
            lblBangChu.Text = "Bằng chữ: ";
            cbbMatHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, slcon, tong, tongmoi;
            sql = "select MaHDBan from tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                // mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // mã hóa đơn bán được sinh tự động do đó không có trường hợp trùng khóa
                if (cbbMaNhanVien.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbMaNhanVien.Focus();
                    return;
                }
                if (cbbMaKhachHang.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbMaKhachHang.Focus();
                    return;
                }
                sql = "insert into tblHDBan (MaHDBan,NgayBan,MaNhanVien,MaKhach,TongTien) " +
                                            "values (N'" + txtMaHoaDon.Text +
                                             "','" + dtNgayban.Value + "',N'" +
                                             cbbMaNhanVien.SelectedValue + "',N'" +
                                             cbbMaKhachHang.SelectedValue + "'," +
                                             txtTongTien.Text + ")";
                Functions.RunSQL(sql);
            }
            // luu thong tin cac mat hang
            if (cbbMatHang.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMatHang.Focus();
                return;
            }
            if ((txtSoLuong.Text.Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if(txtGiamGia.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "select MaHang from tblChiTietHDBan where MaHang = N'" + cbbMatHang.SelectedValue + "' and MaHDBan = N'" + txtMaHoaDon.Text + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValueHang();
                cbbMatHang.Focus();
                return;
            }
            // kiem tra so luong hang trong kho du cung cap hay khong ?
            sl = Convert.ToDouble(Functions.GetFieldValues("select SoLuong from tblHang where MaHang = N'" + cbbMatHang.SelectedValue + "'"));
            if(Convert.ToDouble(txtSoLuong.Text) > sl) 
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = " insert into tblChiTietHDBan (MaHDBan,MaHang,SoLuong,DonGia,GiamGia,ThanhTien) " +
                                        "values (N'" + txtMaHoaDon.Text + "',N'" + cbbMatHang.SelectedValue +
                                                "'," + txtSoLuong.Text + "," + txtDonGia.Text + "," + txtGiamGia.Text +
                                                "," + txtThanhTien.Text + ")";
            Functions.RunSQL(sql);
            Load_dtgv();
            // cap nhat so luong mat hang vao tblHang
            slcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "update tblHang set SoLuong = " + slcon + "where MaHang = N'" + cbbMatHang.SelectedValue + "'";
            Functions.RunSQL(sql);
            // cap nhat lai tong tien cho hoa don ban
            tong = Convert.ToDouble(Functions.GetFieldValues("select TongTien from tblHDBan where MaHDBan = N'" + txtMaHoaDon.Text + "'"));
            tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "update tblHDBan set TongTien = " + tongmoi + "where MaHDBan = N'" + txtMaHoaDon.Text + "'";
            Functions.RunSQL(sql);
            txtTongTien.Text = tongmoi.ToString();
            lblBangChu.Text = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(double.Parse(tongmoi.ToString()));
            ResetValueHang();
            btnXoa.Enabled = btnThem.Enabled = btnIn.Enabled = true;
        }

        private void cbbMaKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if(cbbMaKhachHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDiaChi.Text = "";
                mtbDienThoai.Text = "";
            }
            // khi chon ma khach hang thi cac thong tin se hien ra
            str = "select TenKhach from tblKhach where MaKhach = N'" + cbbMaKhachHang.SelectedValue + "'";
            txtTenKhachHang.Text = Functions.GetFieldValues(str);
            str = "select DiaChi from tblKhach where MaKhach =N'" + cbbMaKhachHang.SelectedValue + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "select DienThoai from tblKhach where MaKhach = N'" + cbbMaKhachHang.SelectedValue + "'";
            mtbDienThoai.Text = Functions.GetFieldValues(str);
        }

        private void cbbMatHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if(cbbMatHang.Text == "")
            {
                txtTenHang.Text = "";
                txtDonGia.Text = "";
            }
            // khi chon mat hang thi cac thong tin ve hang hien ra
            str = "select TenHang from tblHang where MaHang = N'" + cbbMatHang.SelectedValue + "'";
            txtTenHang.Text = Functions.GetFieldValues(str);
            str = "select DonGiaBan from tblHang where MaHang = N'" + cbbMatHang.SelectedValue + "'";
            txtDonGia.Text = Functions.GetFieldValues(str);
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            // khi thay doi so luong thi thuc hien lai viec tinh lai thanh tien
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            // khi thay doi giam gia thi tinh lai thanh tien
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void cbbMaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if(cbbMaNhanVien.Text == " ")
            {
                txtTenNhanVien.Text = "";
            }
            // khi chon ma nhan vien thi se hien ra
            str = "select TenNhanVien from tblNhanVien where MaNhanVien = N'" + cbbMaNhanVien.SelectedValue + "'";
            txtTenNhanVien.Text = Functions.GetFieldValues(str);    
        }

        private void cbbMaHDBan_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("select MaHDBan from tblHDBan", cbbMaHDBan, "MaHDBan", "MaHDBan");
            cbbMaHDBan.SelectedIndex = -1;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if(cbbMaHDBan.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMaHDBan.Focus();
                return;
            }
            txtMaHoaDon.Text = cbbMaHDBan.Text;
            Load_ThongTin_Hoa_Don();
            Load_dtgv();
            btnXoa.Enabled = btnLuu.Enabled = btnIn.Enabled = true;
            cbbMaHDBan.SelectedIndex = -1;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            // khoi dong chuong trinh excecl
            COMExecl.Application exApp = new COMExecl.Application();
            COMExecl.Workbook exBook; // trong chuon trinh excecl co nhiu` workbook
            COMExecl.Worksheet exSheet; // trong workbook co nhieu worksheet
            COMExecl.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongTinHD, tblThongTinHang;
            exBook = exApp.Workbooks.Add(COMExecl.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "MeoMeo Shop";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Quận 11 - TP.HCM";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (84)963207521";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";

            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHDBan, a.NgayBan, a.TongTien, b.TenKhach, b.DiaChi, b.DienThoai, c.TenNhanVien FROM tblHDBan AS a, tblKhach AS b, tblNhanVien AS c " +
                "WHERE a.MaHDBan = N'" + txtMaHoaDon.Text + "' AND a.MaKhach = b.MaKhach AND a.MaNhanVien = c.MaNhanVien";
            tblThongTinHD = Functions.GetDataTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongTinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongTinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongTinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongTinHD.Rows[0][5].ToString();

            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenHang, a.SoLuong, b.DonGiaBan, a.GiamGia, a.ThanhTien " +
                  "FROM tblChiTietHDBan AS a , tblHang AS b WHERE a.MaHDBan = N'" +
                  txtMaHoaDon.Text + "' AND a.MaHang = b.MaHang";
            tblThongTinHang = Functions.GetDataTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongTinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongTinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongTinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongTinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongTinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment =COMExecl.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChuoi(double.Parse(tblThongTinHD.Rows[0][2].ToString()));
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongTinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "TP.HCM, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExecl.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongTinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn ";
            exApp.Visible = true;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (Convert.ToInt32(e.KeyChar) == 8)){
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

