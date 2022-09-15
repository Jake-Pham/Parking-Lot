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
    public partial class TraXeForm : Form
    {
        public TraXeForm()
        {
            InitializeComponent();
        }
        CONTRACT contract = new CONTRACT();
        public void TraXeForm_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaHD as 'Mã Hợp Đồng', HoTen as 'Họ Tên', NgayKy as 'Ngày Ký', NgayTra as 'Ngày Trả', BienSo as 'Biển Số', ChuSH as 'Chủ Sở Hữu', CMND as 'CMND', LoaiXe as 'Loại Xe', GhiChu as 'Ghi Chú', NguoiThue as 'Người Thuê' FROM Contract");
            fillGrid(command);
        }
        private void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn piccol1 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = contract.getContract(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[9];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            TraXeThanhToanForm pay = new TraXeThanhToanForm();
            Globals.SetGlobalUserIId(dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim());
            pay.NguoiThueTextBox.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
            pay.BienXeTextBox.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim();
            pay.CMNDTextBox.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim();
            pay.ChuSHTextBox.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim();
            pay.NgayThueDateTimePicker.Value = (DateTime)dataGridView1.CurrentRow.Cells[2].Value;
            DateTime d1 = pay.NgayThueDateTimePicker.Value;
            pay.NgayTraDateTimePicker.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
            DateTime d2 = pay.NgayTraDateTimePicker.Value;
            if ((dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim() == "Xe May"))
            {
                pay.XeMayRadioButton.Checked = true;
            }
            else if ((dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim() == "O To"))
            {
                pay.OToRadioButton.Checked = true;
            }
            pay.Show();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaHD as 'Mã Hợp Đồng', HoTen as 'Họ Tên', NgayKy as 'Ngày Ký', NgayTra as 'Ngày Trả', BienSo as 'Biển Số', ChuSH as 'Chủ Sở Hữu', CMND as 'CMND', LoaiXe as 'Loại Xe', GhiChu as 'Ghi Chú', NguoiThue as 'Người Thuê' FROM Contract");
            fillGrid(command);
        }
    }
}
