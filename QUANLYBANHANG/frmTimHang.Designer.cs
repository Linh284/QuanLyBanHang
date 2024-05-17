namespace QUANLYBANHANG
{
    partial class frmTimHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.txtMaHang = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnTimLai = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dtgvHang = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbMaChatLieu = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.txtAnh = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTenHang
            // 
            this.txtTenHang.Location = new System.Drawing.Point(212, 76);
            this.txtTenHang.Name = "txtTenHang";
            this.txtTenHang.Size = new System.Drawing.Size(336, 28);
            this.txtTenHang.TabIndex = 45;
            this.txtTenHang.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTenHang_KeyPress);
            // 
            // txtMaHang
            // 
            this.txtMaHang.Location = new System.Drawing.Point(212, 21);
            this.txtMaHang.Name = "txtMaHang";
            this.txtMaHang.Size = new System.Drawing.Size(336, 28);
            this.txtMaHang.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(56, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 24);
            this.label3.TabIndex = 41;
            this.label3.Text = "Tên hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(56, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 24);
            this.label2.TabIndex = 40;
            this.label2.Text = "Mã hàng";
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(715, 575);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(151, 48);
            this.btnDong.TabIndex = 54;
            this.btnDong.Text = "&Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnTimLai
            // 
            this.btnTimLai.Location = new System.Drawing.Point(494, 575);
            this.btnTimLai.Name = "btnTimLai";
            this.btnTimLai.Size = new System.Drawing.Size(151, 48);
            this.btnTimLai.TabIndex = 53;
            this.btnTimLai.Text = "&Tìm lại";
            this.btnTimLai.UseVisualStyleBackColor = true;
            this.btnTimLai.Click += new System.EventHandler(this.btnTimLai_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(273, 575);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(151, 48);
            this.btnTimKiem.TabIndex = 52;
            this.btnTimKiem.Text = "&Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // dtgvHang
            // 
            this.dtgvHang.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dtgvHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvHang.Location = new System.Drawing.Point(25, 254);
            this.dtgvHang.Name = "dtgvHang";
            this.dtgvHang.RowHeadersWidth = 51;
            this.dtgvHang.RowTemplate.Height = 24;
            this.dtgvHang.Size = new System.Drawing.Size(1197, 286);
            this.dtgvHang.TabIndex = 55;
            this.dtgvHang.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvHang_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(56, 135);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 24);
            this.label6.TabIndex = 48;
            this.label6.Text = "Mã chất liệu";
            // 
            // cbbMaChatLieu
            // 
            this.cbbMaChatLieu.FormattingEnabled = true;
            this.cbbMaChatLieu.Location = new System.Drawing.Point(212, 131);
            this.cbbMaChatLieu.Name = "cbbMaChatLieu";
            this.cbbMaChatLieu.Size = new System.Drawing.Size(336, 30);
            this.cbbMaChatLieu.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(602, 24);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 24);
            this.label8.TabIndex = 57;
            this.label8.Text = "Ảnh";
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(938, 12);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(284, 223);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic.TabIndex = 58;
            this.pic.TabStop = false;
            // 
            // txtAnh
            // 
            this.txtAnh.Location = new System.Drawing.Point(676, 12);
            this.txtAnh.Multiline = true;
            this.txtAnh.Name = "txtAnh";
            this.txtAnh.Size = new System.Drawing.Size(256, 86);
            this.txtAnh.TabIndex = 59;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmTimHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1249, 662);
            this.Controls.Add(this.txtAnh);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dtgvHang);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnTimLai);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.cbbMaChatLieu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTenHang);
            this.Controls.Add(this.txtMaHang);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmTimHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm Hàng";
            this.Load += new System.EventHandler(this.frmTimHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.TextBox txtMaHang;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnTimLai;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dtgvHang;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbbMaChatLieu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.TextBox txtAnh;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}