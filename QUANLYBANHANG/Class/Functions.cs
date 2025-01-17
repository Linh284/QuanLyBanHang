﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QUANLYBANHANG.Class
{
    public class Functions
    {
        // tao connect
        public static SqlConnection conn;
       
        public static void Connect()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\LẬP TRÌNH WIN\\QUANLYBANHANG_NHOM7\\LTWIN_CUOIKI\\QUANLYBANHANG\\QUANLYBANHANG\\QuanLyBanHang.mdf\";Integrated Security=True;Connect Timeout=30";
            conn.Open();
            // kiem tra ket noi
            if (conn.State == ConnectionState.Open)
            {
                MessageBox.Show("Kết nối thành công");
            }
            else MessageBox.Show("Không thể kết nối với dữ liệu");
        }

        public static void Disconnect()
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

        }
        public static DataTable GetDataTable(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand  = new SqlCommand(sql);
            da.SelectCommand.Connection = Functions.conn;
            da.SelectCommand.CommandText = sql;
            // khai bao doi tuong table thuoc lop data table
            DataTable dt = new DataTable(); 
            da.Fill(dt);
            return dt;
        }
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0) return true;
            else return false;
        }
        public static void RunSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn); 
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static void RunSqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose ();
            cmd = null;
        }
        public static DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            SqlCommand cmd = new SqlCommand(query, conn);

            if (parameter != null)
            {
                string[] listPara = query.Split(' ');
                int i = 0;
                foreach (string item in listPara)
                {
                    if (item.Contains('@'))
                    {
                        cmd.Parameters.AddWithValue(item, parameter[i++]);
                    }
                }
            }
            SqlDataAdapter dt = new SqlDataAdapter(cmd);
            dt.Fill(data);
            return data;
        }

        public static void FillCombo(string sql, ComboBox cbb, string ma, string ten)
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable(); 
            da.Fill(dt);
            cbb.DataSource = dt;
            cbb.ValueMember = ma;   // trường giá trị
            cbb.DisplayMember = ten;    // trường hiển thị
        }

        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;  
        }
        public static string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1;
            for(int i=0;i<=mLen;i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if(mLen == i)   //chữ số cuối cùng không cần xét tiếp break;
                {
                    switch ((mLen - i) % 9)
                    {
                        case 0:
                            mTemp = mTemp + " tỷ";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 6:
                            mTemp = mTemp + " triệu";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 3:
                            mTemp = mTemp + " nghìn";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        default:
                            switch((mLen - i) % 3)
                            {
                                case 2:
                                    mTemp = mTemp + " trăm";
                                    break;
                                case 1:
                                    mTemp = mTemp + " mươi";
                                    break;
                            }
                            break;
                    }
                }
            }
            // loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không", "");
            // loại bỏ trường hợp 00x
            mTemp = mTemp.Replace("không mươi không", "");
            // loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("không mươi", "linh ");
            mTemp = mTemp.Replace("mươi không", "mươi");
            // fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            // fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            // fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            // fix trường hợp x5,x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            // fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            // fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            // bỏ ký tự space
            mTemp = mTemp.Trim();
            // viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + "đồng";
            return mTemp;
        }

        // tao ma hoa don tu dong
        public static string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            // vi du 18/08/2004
            string d = string.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            // vi du 7:06:02 pm hoac 8:02:01
            if (partsTime[2].Substring(3,2) == "PM")
            {
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            }
            if (partsTime[2].Substring(3,2) == "AM")
            {
                partsTime[0] = "0" + partsTime[0];
            }
            // xoa ky tu trang hoac am va pm
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = string.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }
        // chuyển giờ phút giây
        public static string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }
        #region Đọc và chuyển đổi số 
        // chuyen tu so sang chu
        //123 => một trăm hai ba đồng
        //1,123,000=>một triệu một trăm hai ba nghìn đồng
        //1,123,345,000 => một tỉ một trăm hai ba triệu ba trăm bốn lăm ngàn đồng
        static string[] mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
        //Viết hàm chuyển số hàng chục, giá trị truyền vào là số cần chuyển và một biến đọc phần lẻ hay không ví dụ 101 => một trăm lẻ một
        private static string DocHangChuc(double so, bool daydu)
        {
            string chuoi = "";
            //Hàm để lấy số hàng chục ví dụ 21/10 = 2
            Int64 chuc = Convert.ToInt64(Math.Floor((double)(so / 10)));
            //Lấy số hàng đơn vị bằng phép chia 21 % 10 = 1
            Int64 donvi = (Int64)so % 10;
            //Nếu số hàng chục tồn tại tức >=20
            if (chuc > 1)
            {
                chuoi = " " + mNumText[chuc] + " mươi";
                if (donvi == 1)
                {
                    chuoi += " mốt";
                }
            }
            else if (chuc == 1)
            {//Số hàng chục từ 10-19
                chuoi = " mười";
                if (donvi == 1)
                {
                    chuoi += " một";
                }
            }
            else if (daydu && donvi > 0)
            {//Nếu hàng đơn vị khác 0 và có các số hàng trăm ví dụ 101 => thì biến daydu = true => và sẽ đọc một trăm lẻ một
                chuoi = " lẻ";
            }
            if (donvi == 5 && chuc >= 1)
            {//Nếu đơn vị là số 5 và có hàng chục thì chuỗi sẽ là " lăm" chứ không phải là " năm"
                chuoi += " lăm";
            }
            else if (donvi > 1 || (donvi == 1 && chuc == 0))
            {
                chuoi += " " + mNumText[donvi];
            }
            return chuoi;
        }
        private static string DocHangTram(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng trăm ví du 434 / 100 = 4 (hàm Floor sẽ làm tròn số nguyên bé nhất)
            Int64 tram = Convert.ToInt64(Math.Floor((double)so / 100));
            //Lấy phần còn lại của hàng trăm 434 % 100 = 34 (dư 34)
            so = so % 100;
            if (daydu || tram > 0)
            {
                chuoi = " " + mNumText[tram] + " trăm";
                chuoi += DocHangChuc(so, true);
            }
            else
            {
                chuoi = DocHangChuc(so, false);
            }
            return chuoi;
        }
        private static string DocHangTrieu(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng triệu
            Int64 trieu = Convert.ToInt64(Math.Floor((double)so / 1000000));
            //Lấy phần dư sau số hàng triệu ví dụ 2,123,000 => so = 123,000
            so = so % 1000000;
            if (trieu > 0)
            {
                chuoi = DocHangTram(trieu, daydu) + " triệu";
                daydu = true;
            }
            //Lấy số hàng nghìn
            Int64 nghin = Convert.ToInt64(Math.Floor((double)so / 1000));
            //Lấy phần dư sau số hàng nghin 
            so = so % 1000;
            if (nghin > 0)
            {
                chuoi += DocHangTram(nghin, daydu) + " nghìn";
                daydu = true;
            }
            if (so > 0)
            {
                chuoi += DocHangTram(so, daydu);
            }
            return chuoi;
        }
        public static string ChuyenSoSangChuoi(double so)
        {
            if (so == 0)
                return mNumText[0];
            string chuoi = "", hauto = "";
            Int64 ty;
            do
            {
                //Lấy số hàng tỷ
                ty = Convert.ToInt64(Math.Floor((double)so / 1000000000));
                //Lấy phần dư sau số hàng tỷ
                so = so % 1000000000;
                if (ty > 0)
                {
                    chuoi = DocHangTrieu(so, true) + hauto + chuoi;
                }
                else
                {
                    chuoi = DocHangTrieu(so, false) + hauto + chuoi;
                }
                hauto = " tỷ";
            } while (ty > 0);
            return chuoi + " đồng";
        }
        #endregion
    }

}

