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
    public partial class ContractForm : Form
    {
        public ContractForm()
        {
            InitializeComponent();
        }
        CONTRACT contract = new CONTRACT();
        MY_DB mydb = new MY_DB();
        BAIXETHUE baixe = new BAIXETHUE();

        private void AddButton_Click(object sender, EventArgs e)
        {
            QuanLyThueXe quanly = new QuanLyThueXe();
            string id = Globals.GlobalUserId;
            DataTable table = baixe.searchXe(id);
            byte[] pic = (byte[])table.Rows[0]["PicXe"];
            MemoryStream picxe = new MemoryStream(pic);
            string MaHD = contract.Tangma();
            string NguoiThue = NguoiThueTextBox.Text;
            NguoiThueTextBox.Text = MaHD;
            string trangthai = "Busy";
            DateTime NgayKy = NgayKyDatetimePicker.Value;
            DateTime NgayTra = NgayTraDateTimePicker.Value;
            string bienso = BienXeTextBox.Text;
            string chuSH = SoHuuTextBox.Text;
            string cmnd = CMNDTextBox.Text;
            string ghichu = GhiChuTextBox.Text;
            string LoaiXe = "";
            if (XeMayRadioButton.Checked)
            {
                LoaiXe = "Xe May";
            }
            else if (OToRadioButton.Checked)
            {
                LoaiXe = "O To";
            }
            MemoryStream NguoiThuePic = new MemoryStream();
            /*ChoThueForm thue = new ChoThueForm();
            if((NgayTraDateTimePicker.Value > thue.NgayLayDateTimePicker.Value) && chuSH != "SPKT")
            {
                MessageBox.Show("Xe sẽ được lấy vào " + thue.NgayLayDateTimePicker.Value,"Contract", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/    
            if (verif())
            {
                NguoiThuePictureBox.Image.Save(NguoiThuePic, NguoiThuePictureBox.Image.RawFormat);
                if (contract.addContract(MaHD,NguoiThue, NgayKy, NgayTra, bienso, chuSH, cmnd, LoaiXe, ghichu, NguoiThuePic) && baixe.updateXe(id, bienso, chuSH, NgayKy, NgayTra, trangthai, LoaiXe, picxe))
                {
                    MessageBox.Show("New Contract Signed", "Contract", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Contract", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        bool verif()
        {
            if ((CMNDLabel.Text.Trim() == "") || (NguoiThuePictureBox.Image == null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg; *.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                NguoiThuePictureBox.Image = Image.FromFile(opf.FileName);
            }
        }
    }
}
