
namespace Parking_Lot
{
    partial class QuanLyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuanLyForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BaiThueXe = new System.Windows.Forms.Button();
            this.DoanhThuButton = new System.Windows.Forms.Button();
            this.ChoThueButton = new System.Windows.Forms.Button();
            this.ThueButton = new System.Windows.Forms.Button();
            this.NhanVienButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 213);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1442, 540);
            this.dataGridView1.TabIndex = 0;
            // 
            // BaiThueXe
            // 
            this.BaiThueXe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BaiThueXe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BaiThueXe.Location = new System.Drawing.Point(30, 173);
            this.BaiThueXe.Name = "BaiThueXe";
            this.BaiThueXe.Size = new System.Drawing.Size(117, 34);
            this.BaiThueXe.TabIndex = 2;
            this.BaiThueXe.Text = "Bãi Thuê Xe";
            this.BaiThueXe.UseVisualStyleBackColor = false;
            this.BaiThueXe.Click += new System.EventHandler(this.BaiThueXe_Click);
            // 
            // DoanhThuButton
            // 
            this.DoanhThuButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.DoanhThuButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoanhThuButton.Location = new System.Drawing.Point(227, 173);
            this.DoanhThuButton.Name = "DoanhThuButton";
            this.DoanhThuButton.Size = new System.Drawing.Size(209, 34);
            this.DoanhThuButton.TabIndex = 3;
            this.DoanhThuButton.Text = "Doanh Thu Bãi Đậu Xe";
            this.DoanhThuButton.UseVisualStyleBackColor = false;
            this.DoanhThuButton.Click += new System.EventHandler(this.DoanhThuButton_Click);
            // 
            // ChoThueButton
            // 
            this.ChoThueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ChoThueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChoThueButton.Location = new System.Drawing.Point(912, 173);
            this.ChoThueButton.Name = "ChoThueButton";
            this.ChoThueButton.Size = new System.Drawing.Size(213, 34);
            this.ChoThueButton.TabIndex = 4;
            this.ChoThueButton.Text = "Danh Sách Cho Thuê";
            this.ChoThueButton.UseVisualStyleBackColor = false;
            this.ChoThueButton.Click += new System.EventHandler(this.ChoThueButton_Click);
            // 
            // ThueButton
            // 
            this.ThueButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ThueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThueButton.Location = new System.Drawing.Point(557, 173);
            this.ThueButton.Name = "ThueButton";
            this.ThueButton.Size = new System.Drawing.Size(213, 34);
            this.ThueButton.TabIndex = 5;
            this.ThueButton.Text = "Danh Sách Thuê Xe";
            this.ThueButton.UseVisualStyleBackColor = false;
            this.ThueButton.Click += new System.EventHandler(this.ThueButton_Click);
            // 
            // NhanVienButton
            // 
            this.NhanVienButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.NhanVienButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NhanVienButton.Location = new System.Drawing.Point(1241, 173);
            this.NhanVienButton.Name = "NhanVienButton";
            this.NhanVienButton.Size = new System.Drawing.Size(213, 34);
            this.NhanVienButton.TabIndex = 6;
            this.NhanVienButton.Text = "Nhân Viên";
            this.NhanVienButton.UseVisualStyleBackColor = false;
            this.NhanVienButton.Click += new System.EventHandler(this.NhanVienButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-3, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1457, 145);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // QuanLyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1466, 787);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.NhanVienButton);
            this.Controls.Add(this.ThueButton);
            this.Controls.Add(this.ChoThueButton);
            this.Controls.Add(this.DoanhThuButton);
            this.Controls.Add(this.BaiThueXe);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "QuanLyForm";
            this.Text = "Quản Lý";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BaiThueXe;
        private System.Windows.Forms.Button DoanhThuButton;
        private System.Windows.Forms.Button ChoThueButton;
        private System.Windows.Forms.Button ThueButton;
        private System.Windows.Forms.Button NhanVienButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}