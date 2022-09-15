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
using System.IO;

namespace Parking_Lot
{
    public partial class QuanLyXeForm : Form
    {
        public QuanLyXeForm()
        {
            InitializeComponent();
        }
        VEHICLE vehicle = new VEHICLE();
        MY_DB mydb = new MY_DB();

        private void QuanLyXeForm_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaXe as'Mã Xe', NgayGui as 'Ngày Gửi', NgayLay as 'Ngày Lấy', TrangThai as 'Trạng Thái', PhuongThucGui as 'Phương Thức Gửi', LoaiXe as 'Loại Xe', BienSo as 'Biển Số', PicXe as 'Ảnh Xe', NguoiGui as 'Người Gửi' , GiaTien as 'Giá Tiền'  FROM Vehicles");
            //dataGridView1.ReadOnly = true;
            fillGrid(command);
        }
        private void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn piccol1 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            DataGridViewImageColumn piccol2 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = vehicle.getBike(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[7];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            piccol2 = (DataGridViewImageColumn)dataGridView1.Columns[8];
            piccol2.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            SoXeLabel.Text = ("So xe:" + dataGridView1.Rows.Count + "/200");
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Vehicles");
            dataGridView1.ReadOnly = true; //Nap lai du lieu len datagridview
            DataGridViewImageColumn piccol1 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            DataGridViewImageColumn piccol2 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = vehicle.getBike(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[7];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            piccol2 = (DataGridViewImageColumn)dataGridView1.Columns[8];
            piccol2.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            SoXeLabel.Text = ("So xe:" + dataGridView1.Rows.Count + "/200");
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            EditXeForm edit = new EditXeForm();
            Globals.SetGlobalUserIId(dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim());
            //edit.MaXeTextBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            edit.dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[1].Value;
            // Fill loại xe
            if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "Xe Dap")
            {
                edit.XeDapRadioButton.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "Xe May")
            {
                edit.XeMayRadioButton.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "O To")
            {
                edit.OToRadioButton.Checked = true;
            }
            //Fill phương thức gửi
            edit.BienSoTextBox.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Gio")
            {
                edit.GioRadioButton.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Ngay")
            {
                edit.NgayRadioButton.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Tuan")
            {
                edit.TuanRadioButton.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Thang")
            {
                edit.ThangRadioButton.Checked = true;
            }
            //Fill trạng thái
            if (dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim() == "Dang Gui")
            {
                edit.DangGuiRadioButton.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim() == "Da Lay")
            {
                edit.DaLayRadioButton.Checked = true;
            }
            byte[] xepic;
            xepic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream XePic = new MemoryStream(xepic);
            edit.XePictureBox.Image = Image.FromStream(XePic);
            byte[] nguoigui;
            nguoigui = (byte[])dataGridView1.CurrentRow.Cells[8].Value;
            MemoryStream NguoiGui = new MemoryStream(nguoigui);
            edit.NguoiGuiPictureBox.Image = Image.FromStream(NguoiGui);
            edit.Show();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string MaXe = vehicle.Tangma();
            DateTime ThoiGian = dateTimePicker1.Value;
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
            string TrangThai = "Dang Gui";
            if (DaLayRadioButton.Checked)
            {
                TrangThai = "Da Lay";
            }
            string GuiXe = "Gio";
            if (NgayRadioButton.Checked)
            {
                GuiXe = "Ngay";
            }
            else
            if (TuanRadioButton.Checked)
            {
                GuiXe = "Tuan";
            }
            else
            if (ThangRadioButton.Checked)
            {
                GuiXe = "Thang";
            }
            MemoryStream Xe = new MemoryStream();
            MemoryStream NguoiGui = new MemoryStream();
            if (dataGridView1.Rows.Count >= 200)
            {
                MessageBox.Show("The Lot is Full!", "Add New Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (vehicle.checkBike(MaXe))
            {
                MessageBox.Show("This MaXe Already Exist", "Add New Vehicle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif1())
            {
                XePictureBox.Image.Save(Xe, XePictureBox.Image.RawFormat);
                NguoiGuiPictureBox.Image.Save(NguoiGui, NguoiGuiPictureBox.Image.RawFormat);
                if (vehicle.addAllXe(MaXe, ThoiGian, TrangThai, GuiXe, LoaiXe, BienSo, Xe, NguoiGui))
                {
                    if (vehicle.addBike(MaXe, ThoiGian, TrangThai, GuiXe, LoaiXe, BienSo, Xe, NguoiGui))
                    {
                        MessageBox.Show("New vehicle added to the lot", "Add new vehicle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Add new vehicle", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Empty Fields", "Add new vehicle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool verif1()
        {
            if ((MaXeLabel.Text.Trim() == "") || (LoaiXeLabel.Text.Trim() == "") || (BienSoLabel.Text.Trim() == "") || (XePictureBox.Image == null) || (NguoiGuiPictureBox.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CheckButon_Click(object sender, EventArgs e)
        {
            string MaXe = MaXeTextBox.Text;
            DateTime ThoiGianGui = dateTimePicker1.Value;
            DateTime ThoiGianLay = DateTime.Now;
            string TrangThai = "Da Lay";
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
            if (XeDapRadioButton.Checked)
            {
                GiaTien = Tinh_Tien_XeDap(ThoiGianGui, ThoiGianLay);
            }
            else if (XeMayRadioButton.Checked)
            {
                GiaTien = Tinh_Tien_XeMay(ThoiGianGui, ThoiGianLay);
            }
            else if (OToRadioButton.Checked)
            {
                GiaTien = Tinh_Tien_OTo(ThoiGianGui, ThoiGianLay);
            }
            MemoryStream Xe = new MemoryStream();
            MemoryStream NguoiGui = new MemoryStream();
            if (verif2())
            {
                XePictureBox.Image.Save(Xe, XePictureBox.Image.RawFormat);
                NguoiGuiPictureBox.Image.Save(NguoiGui, NguoiGuiPictureBox.Image.RawFormat);
                vehicle.addtoBill(MaXe, LoaiXe, BienSo, ThoiGianGui, ThoiGianLay, GiaTien);
                if (vehicle.updateBike(MaXe, ThoiGianGui, ThoiGianLay, TrangThai, PhuongThuGui, LoaiXe, BienSo, Xe, NguoiGui, GiaTien))
                {
                    MessageBox.Show("Updated", "Xe Ra", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    vehicle.deleteBike(MaXe);
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
        bool verif2()
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
        private int Tinh_Tien_XeDap(DateTime d1, DateTime d2)
        {
            int kq = 0;
            if (GioRadioButton.Checked == true)
            {
                if (d2.Minute - d1.Minute >= 1)
                    kq = 500;
                else if (d2.Day - d1.Day <= 1 && d2.Hour <= d1.Hour)
                    kq = (d2.Hour - d1.Hour) * 500;
                else if (d2.Day - d1.Day >= 1)
                    kq = 2 * (500 * 8);
            }
            if (NgayRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 1)
                    kq = 500 * 8;
                if (d2.Day - d1.Day >= 1)
                    kq = (500 * 8) * 7;
            }
            if (TuanRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 7)
                    kq = (500 * 8) * 3;
                if (d2.Day - d1.Day >= 10 && d2.Day - d1.Day <= 30)
                    kq = (500 * 8) * 3 * 2;
            }
            if (ThangRadioButton.Checked == true)
                kq = (500 * 8) * 3 * 2;
            return kq;
        }
        private int Tinh_Tien_XeMay(DateTime d1, DateTime d2)
        {
            int kq = 0;
            if (GioRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 1 && d2.Hour <= d1.Hour)
                    kq = (d2.Hour - d1.Hour) * 1000;
                if (d2.Day - d1.Day >= 1)
                    kq = 2 * (1000 * 8);
            }
            if (NgayRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 1)
                    kq = 1000 * 8;
                if (d2.Day - d1.Day >= 1)
                    kq = (1000 * 8) * 7;
            }
            if (TuanRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 7)
                    kq = (1000 * 8) * 3;
                if (d2.Day - d1.Day >= 10 && d2.Day - d1.Day <= 30)
                    kq = (1000 * 8) * 3 * 2;
            }
            if (ThangRadioButton.Checked == true)
                kq = (1000 * 8) * 3 * 2;
            return kq;
        }
        private int Tinh_Tien_OTo(DateTime d1, DateTime d2)
        {
            int kq = 0;
            if (GioRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 1 && d2.Hour <= d1.Hour)
                    kq = (d2.Hour - d1.Hour) * 5000;
                if (d2.Day - d1.Day >= 1)
                    kq = 2 * (5000 * 8);
            }
            if (NgayRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 1)
                    kq = 5000 * 8;
                if (d2.Day - d1.Day >= 1)
                    kq = (5000 * 8) * 7;
            }
            if (TuanRadioButton.Checked == true)
            {
                if (d2.Day - d1.Day <= 7)
                    kq = (5000 * 8) * 3;
                if (d2.Day - d1.Day >= 10 && d2.Day - d1.Day <= 30)
                    kq = (5000 * 8) * 3 * 2;
            }
            if (ThangRadioButton.Checked == true)
                kq = (5000 * 8) * 3 * 2;
            return kq;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string MaXe = MaXeTextBox.Text;
            SqlCommand command = new SqlCommand("SELECT MaXe, NgayGui, NgayLay, PhuongThucGui, LoaiXe, BienSo, PicXe, NguoiGui, GiaTien FROM Vehicles WHERE MaXe=@MaXe", mydb.GetConnection);
            command.Parameters.Add("@MaXe", SqlDbType.NChar).Value = MaXe;
            DataTable table = vehicle.getBike(command);
            if (table.Rows.Count > 0)
            {
                dateTimePicker1.Value = (DateTime)table.Rows[0]["NgayGui"];
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

        private void PicXePictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg; *.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                XePictureBox.Image = Image.FromFile(opf.FileName);
            }
        }

        private void PicNguoiButon_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg; *.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                NguoiGuiPictureBox.Image = Image.FromFile(opf.FileName);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            MaXeTextBox.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[1].Value;
            //Trạng Thái
            if ((dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim() == "Dang Gui"))
            {
                DangGuiRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim() == "Da Lay"))
            {
                DaLayRadioButton.Checked = true;
            }
            //Phương Thức Gửi
            if ((dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Gio"))
            {
                GioRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Ngay"))
            {
                NgayRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Tuan"))
            {
                TuanRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Thang"))
            {
                ThangRadioButton.Checked = true;
            }
            //Loại Xe
            if ((dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "Xe Dap"))
            {
                XeDapRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "Xe May"))
            {
                XeMayRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "O To"))
            {
                OToRadioButton.Checked = true;
            }
            BienSoTextBox.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            byte[] pic1;
            pic1 = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream PicXe = new MemoryStream(pic1);
            XePictureBox.Image = Image.FromStream(PicXe);
            byte[] pic2;
            pic2 = (byte[])dataGridView1.CurrentRow.Cells[8].Value;
            MemoryStream PicNguoi = new MemoryStream(pic2);
            NguoiGuiPictureBox.Image = Image.FromStream(PicNguoi);
        }

        private void DoanhThuButton_Click(object sender, EventArgs e)
        {
            DoanhThuForm doanhthu = new DoanhThuForm();
            doanhthu.Show();
        }

        private void ChoThueButton_Click(object sender, EventArgs e)
        {
            if (XeDapRadioButton.Checked == false)
            {
                ChoThueForm rent = new ChoThueForm();
                rent.BienXeTextBox.Text = BienSoTextBox.Text;
                if (XeMayRadioButton.Checked)
                {
                    rent.XeMayRadioButton.Checked = true;
                }
                else if (OToRadioButton.Checked)
                {
                    rent.OToRadioButton.Checked = true;
                }
                rent.XePictureBox.Image = XePictureBox.Image;
                rent.Show();
            }
            else
            {
                MessageBox.Show("Không Thể Cho Thuê", "Cho Thuê", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
