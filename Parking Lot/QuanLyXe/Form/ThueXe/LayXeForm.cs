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

namespace Parking_Lot
{
    public partial class LayXeForm : Form
    {
        public LayXeForm()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        RENT thue = new RENT();
        private void LayXeForm_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaHD as 'Mã Hợp Đồng', Rent.ChuSH as 'Chủ Sở Hữu', Rent.CMND as 'CMND', NgayKyHD as 'Ngày Ký', NgayLay as 'Ngày Lấy', BaiXeThue.TrangThai as 'Trạng Thái', Rent.LoaiXe as 'Loại Xe', Rent.BienSo as 'Biển Số', GhiChu as 'Ghi Chú', Rent.PicXe as 'Ảnh Xe' FROM Rent, BaiXeThue WHERE Rent.ChuSH=BaiXeThue.ChuSH AND Rent.ChuSH <> 'SPKT'", mydb.GetConnection);
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
            dataGridView1.DataSource = thue.getRent(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[9];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            LayXeThanhToanForm pay = new LayXeThanhToanForm();
            Globals.SetGlobalUserIId(dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim());
            pay.ChuSHTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
            pay.BienXeTextBox.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim();
            pay.CMNDTextBox.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim();
            pay.NgayKyDateTimePicker.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            DateTime d1 = pay.NgayKyDateTimePicker.Value;
            pay.NgayLayDateTimePicker.Value = (DateTime)dataGridView1.CurrentRow.Cells[4].Value;
            DateTime d2 = pay.NgayLayDateTimePicker.Value;
            if ((dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim() == "Xe May"))
            {
                pay.XeMayRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim() == "O To"))
            {
                pay.OToRadioButton.Checked = true;
            }
            pay.Show();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaHD as 'Mã Hợp Đồng', Rent.ChuSH as 'Chủ Sở Hữu', Rent.CMND as 'CMND', NgayKyHD as 'Ngày Ký', NgayLay as 'Ngày Lấy', BaiXeThue.TrangThai as 'Trạng Thái', Rent.LoaiXe as 'Loại Xe', Rent.BienSo as 'Biển Số', GhiChu as 'Ghi Chú', Rent.PicXe as 'Ảnh Xe' FROM Rent, BaiXeThue WHERE Rent.ChuSH=BaiXeThue.ChuSH AND Rent.ChuSH <> 'SPKT'", mydb.GetConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            fillGrid(command);
        }
    }
}
