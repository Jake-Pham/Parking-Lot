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
    public partial class QuanLyThueXe : Form
    {
        public QuanLyThueXe()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        BAIXETHUE baixe = new BAIXETHUE();
        RENT rent = new RENT();

        public void QuanLyThueXe_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT Id as 'Mã Xe', BaiXeThue.BienSo as 'Biển Số', BaiXeThue.ChuSH as 'Chủ Sở Hữu', NgayLay as 'Ngày Lấy', NgayKy as 'Ngày Ký', NgayTra as 'Ngày Trả', TrangThai as 'Trạng Thái', BaiXeThue.LoaiXe as 'Loại Xe', BaiXeThue.PicXe as 'Ảnh Xe' FROM BaiXeThue, Rent WHERE BaiXeThue.BienSo=Rent.BienSo", mydb.GetConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            fillGrid(command);
        }
        private void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn piccol1 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = baixe.getXe(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[8];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ContractForm hopdong = new ContractForm();
            Globals.SetGlobalUserIId(dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim());
            hopdong.BienXeTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim() == "Xe May")
            {
                hopdong.XeMayRadioButton.Checked = true;
            }
            else if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim() == "O To")
            {
                hopdong.OToRadioButton.Checked = true;
            }
            hopdong.SoHuuTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            hopdong.Show();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT Id as 'Mã Xe', BaiXeThue.BienSo as 'Biển Số', BaiXeThue.ChuSH as 'Chủ Sở Hữu', NgayLay as 'Ngày Lấy', NgayKy as 'Ngày Ký', NgayTra as 'Ngày Trả', TrangThai as 'Trạng Thái', BaiXeThue.LoaiXe as 'Loại Xe', BaiXeThue.PicXe as 'Ảnh Xe' FROM BaiXeThue, Rent WHERE BaiXeThue.BienSo=Rent.BienSo", mydb.GetConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            fillGrid(command);
            //QuanLyThueXe_Load(null, null);
        }
        private int Ngay_kha_dung(DateTime d1, DateTime d2)
        {
            int year = (d2.Year - d1.Year) * 365;
            int month = (d2.Month - d1.Month) * 30;
            int result = (d2.Day - d1.Day) + year + month;
            return  result;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            ChoThueForm thue = new ChoThueForm();
            thue.Show();
            QuanLyThueXe_Load(null, null);
        }

        private void XoaXeButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to delete this?", "Xoa Xe", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                string Id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                baixe.deleteXe(Id);
                MessageBox.Show("Deleted", "Xoa Xe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QuanLyThueXe_Load(null, null);
            }
            else
            {
                Close();
            }
        }

        private void TraXeButton_Click(object sender, EventArgs e)
        {
            TraXeForm traxe = new TraXeForm();
            traxe.Show();
        }

        private void LayXeButton_Click(object sender, EventArgs e)
        {
            LayXeForm layxe = new LayXeForm();
            layxe.Show();
        }
    }
}
