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

namespace Parking_Lot
{
    public partial class ChoThueForm : Form
    {
        public ChoThueForm()
        {
            InitializeComponent();
        }
        RENT rent = new RENT();
        BAIXETHUE baixe = new BAIXETHUE();

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg; *.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                XePictureBox.Image = Image.FromFile(opf.FileName);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            QuanLyXeForm quanly = new QuanLyXeForm();
            ContractForm hopdong = new ContractForm();
            QuanLyThueXe thue = new QuanLyThueXe();
            string NguoiThue = "";
            string Id = baixe.Tangma();
            string MaHD = rent.Tangma();
            string ChuSH = ChuSHTextBox.Text;
            string cmnd = CMNDTextBox.Text;
            string cmnd_nguoithue = "";
            string bienso = BienXeTextBox.Text;
            BienXeTextBox.Text = quanly.BienSoTextBox.Text;
            DateTime NgayKy = NgayKyDateTimePicker.Value;
            DateTime NgayLay = NgayLayDateTimePicker.Value;
            DateTime NgayKyThue = DateTime.Now;
            DateTime NgayTra = DateTime.Now;
            string TrangThai = "Free";
            //hopdong.NgayKyDatetimePicker.Value = DateTime.Now;
            //int ngaykhadung = Ngay_kha_dung(NgayKy, NgayLay);
            string xe = "";
            if (XeMayRadioButton.Checked)
            {
                xe = "Xe May";
            }
            else if (OToRadioButton.Checked)
            {
                xe = "O To";
            }
            string ghichu = GhiChuTextBox.Text;
            MemoryStream Picxe = new MemoryStream();
            if (verif())
            {
                XePictureBox.Image.Save(Picxe, XePictureBox.Image.RawFormat);
                if (rent.addRent(MaHD, ChuSH, cmnd, NgayKy, NgayLay, xe, bienso, ghichu, Picxe))
                {
                    MessageBox.Show("New Contract Signed", "Rent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    baixe.addXe(Id, bienso, ChuSH, NgayKyThue, NgayTra, TrangThai, xe, Picxe);
                }
                else
                {
                    MessageBox.Show("Error", "Rent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            thue.QuanLyThueXe_Load(null, null);
        }
        bool verif()
        {
            if ((ChuSHLabel.Text.Trim() == "") || (GhiChuLabel.Text.Trim() == "") || (CMNDLabel.Text.Trim() == ""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
