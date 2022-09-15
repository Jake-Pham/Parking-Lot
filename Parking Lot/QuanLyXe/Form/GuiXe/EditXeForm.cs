using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace Parking_Lot
{
    public partial class EditXeForm : Form
    {
        public EditXeForm()
        {
            InitializeComponent();
        }
        VEHICLE vehicle = new VEHICLE();
        MY_DB mydb = new MY_DB();

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string MaXe  = Globals.GlobalUserId;
            SqlCommand command = new SqlCommand("SELECT MaXe, NgayGui, NgayLay, PhuongThucGui, LoaiXe, BienSo, PicXe, NguoiGui, GiaTien FROM Vehicles WHERE MaXe=@MaXe", mydb.GetConnection);
            command.Parameters.Add("@MaXe", SqlDbType.NChar).Value = MaXe;
            DataTable table = vehicle.getBike(command);
            if (table.Rows.Count > 0)
            {
                //Fill TrangThai
                if (table.Rows[0]["TrangThai"].ToString().Trim() == "Da Lay")
                {
                    DaLayRadioButton.Checked = true;
                }
                if (table.Rows[0]["TrangThai"].ToString().Trim() == "Dang Gui")
                {
                    DangGuiRadioButton.Checked = true;
                }
                //Fill LoaiXe
                if (table.Rows[0]["LoaiXe"].ToString().Trim() == "Xe Dap")
                {
                    XeDapRadioButton.Checked = true;
                }
                if (table.Rows[0]["LoaiXe"].ToString().Trim() == "Xe May")
                {
                    XeMayRadioButton.Checked = true;
                }
                if (table.Rows[0]["LoaiXe"].ToString().Trim() == "O To")
                {
                    OToRadioButton.Checked = true;
                }
                BienSoTextBox.Text = table.Rows[0]["BienSo"].ToString();
                //Fill PhuongThucGui
                if (table.Rows[0]["PhuongThucGui"].ToString().Trim() == "Gio")
                {
                    GioRadioButton.Checked = true;
                }
                else if (table.Rows[0]["PhuongThucGui"].ToString().Trim() == "Ngay")
                {
                    NgayRadioButton.Checked = true;
                }
                else if (table.Rows[0]["PhuongThucGui"].ToString().Trim() == "Tuan")
                {
                    TuanRadioButton.Checked = true;
                }
                else
                {
                    ThangRadioButton.Checked = true;
                }
                byte[] picxe = (byte[])table.Rows[0]["PicXe"];
                MemoryStream XePic = new MemoryStream(picxe);
                XePictureBox.Image = Image.FromStream(XePic);
                byte[] nguoigui = (byte[])table.Rows[0]["NguoiGui"];
                MemoryStream NguoiGui = new MemoryStream(nguoigui);
                NguoiGuiPictureBox.Image = Image.FromStream(NguoiGui);
            }
            else
            {
                MessageBox.Show("Not Found", "Find Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            QuanLyXeForm quanly = new QuanLyXeForm();
            string MaXe = Globals.GlobalUserId;
            DateTime ThoiGianDen = dateTimePicker1.Value;
            DateTime ThoiGianRa = dateTimePicker2.Value;
            string TrangThai = "Dang Gui";
            if (DangGuiRadioButton.Checked)
            {
                TrangThai = "Dang Gui";
            }
            else if (DaLayRadioButton.Checked)
            {
                TrangThai = "Da Lay";
            }
            string PhuongThuGui = "Gio";
            if (TuanRadioButton.Checked)
            {
                PhuongThuGui = "Tuan";
            }
            else if (NgayRadioButton.Checked)
            {
                PhuongThuGui = "Ngay";
            }
            else if (ThangRadioButton.Checked)
            {
                PhuongThuGui = "Thang";
            }
            string LoaiXe = "";
            if (XeDapRadioButton.Checked)
            {
                LoaiXe = "Xe Dap";
            }
            else if (XeMayRadioButton.Checked)
            {
                LoaiXe = "Xe May";
            }
            else if (OToRadioButton.Checked)
            {
                LoaiXe = "O To";
            }
            string BienSo = BienSoTextBox.Text;
            float GiaTien = 0;
            MemoryStream Xe = new MemoryStream();
            MemoryStream NguoiGui = new MemoryStream();
            if (verif())
            {
                XePictureBox.Image.Save(Xe, XePictureBox.Image.RawFormat);
                NguoiGuiPictureBox.Image.Save(NguoiGui, NguoiGuiPictureBox.Image.RawFormat);
                if (vehicle.updateBike(MaXe, ThoiGianDen, ThoiGianRa, TrangThai, PhuongThuGui, LoaiXe, BienSo, Xe, NguoiGui, GiaTien))
                {
                    MessageBox.Show("Updated", "Xe Ra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Xe Ra", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Xe Ra", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool verif()
        {
            if ((MaXeLabel.Text.Trim() == "") || (BienSoLabel.Text.Trim() == "") || (XePictureBox.Text.Trim() == null) || (NguoiGuiPictureBox.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string MaXe = Globals.GlobalUserId;
                if (MessageBox.Show("Are you sure you want to delete this", "Delete Vehicle", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (vehicle.deleteBike(MaXe))
                    {
                        MessageBox.Show("Vehicle Deleted", "Delete Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //clear fields after delete
                        MaXeTextBox.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        BienSoLabel.Text = "";
                        XePictureBox.Image = null;
                        NguoiGuiPictureBox.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Vehicle Not  Deleted", "Deleted Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please Enter Valid Ma xe", "Delete Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PicXeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg; *.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                XePictureBox.Image = Image.FromFile(opf.FileName);
            }
        }

        private void PicNguoiButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg; *.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                NguoiGuiPictureBox.Image = Image.FromFile(opf.FileName);
            }
        }
    }
}
