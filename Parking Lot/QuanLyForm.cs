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
    public partial class QuanLyForm : Form
    {
        public QuanLyForm()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        VEHICLE vehicle = new VEHICLE();
        BAIXETHUE baixe = new BAIXETHUE();
        CONTRACT contract = new CONTRACT();
        RENT thue = new RENT();
        WORK work = new WORK();

        private void DoanhThuButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaXe as'Mã Xe', LoaiXe as'Loại Xe', BienSo as 'Biển Số', NgayGui as 'Ngày Gửi', NgayLay as 'Ngày Lấy', GiaTien as 'Giá Tiền' FROM  HoaDon");
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = vehicle.getBike(command);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void BaiDauXeButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaXe as'Mã Xe', NgayGui as 'Ngày Gửi', NgayLay as 'Ngày Lấy', TrangThai as 'Trạng Thái', PhuongThucGui as 'Phương Thức Gửi', LoaiXe as 'Loại Xe', BienSo as 'Biển Số', PicXe as 'Ảnh Xe', NguoiGui as 'Người Gửi' , GiaTien as 'Giá Tiền'  FROM Vehicles");
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
        }

        private void BaiThueXe_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT Id as 'Mã Xe', BaiXeThue.BienSo as 'Biển Số', BaiXeThue.ChuSH as 'Chủ Sở Hữu', NgayLay as 'Ngày Lấy', NgayKy as 'Ngày Ký', NgayTra as 'Ngày Trả', TrangThai as 'Trạng Thái', BaiXeThue.LoaiXe as 'Loại Xe', BaiXeThue.PicXe as 'Ảnh Xe' FROM BaiXeThue, Rent WHERE BaiXeThue.BienSo=Rent.BienSo", mydb.GetConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn piccol1 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = baixe.getXe(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[8];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void ThueButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaHD as 'Mã Hợp Đồng', HoTen as 'Họ Tên', NgayKy as 'Ngày Ký', NgayTra as 'Ngày Trả', BienSo as 'Biển Số', ChuSH as 'Chủ Sở Hữu', CMND as 'CMND', LoaiXe as 'Loại Xe', GhiChu as 'Ghi Chú', NguoiThue as 'Người Thuê' FROM Contract");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn piccol1 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = contract.getContract(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[9];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void ChoThueButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT MaHD as 'Mã Hợp Đồng', Rent.ChuSH as 'Chủ Sở Hữu', Rent.CMND as 'CMND', NgayKyHD as 'Ngày Ký', NgayLay as 'Ngày Lấy', BaiXeThue.TrangThai as 'Trạng Thái', Rent.LoaiXe as 'Loại Xe', Rent.BienSo as 'Biển Số', GhiChu as 'Ghi Chú', Rent.PicXe as 'Ảnh Xe' FROM Rent, BaiXeThue WHERE Rent.ChuSH=BaiXeThue.ChuSH AND Rent.ChuSH <> 'SPKT'", mydb.GetConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn piccol1 = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = thue.getRent(command);
            piccol1 = (DataGridViewImageColumn)dataGridView1.Columns[9];
            piccol1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void NhanVienButton_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select Id as 'Mã Nhân Viên', NgayLam as 'Ngày Làm', TimeIn1 as 'Thời Gian Vào Ca 1', TimeOut1 as 'Thời Gian Hết Ca 1', TimeIn2 as 'Thời Gian Vào Ca 2', TimeOut2 as 'Thời Gian Hết Ca 2', SumHours as 'Tổng Giờ Làm', Luong as 'Lương'  FROM NhanVIen", mydb.GetConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = work.getWork(command);
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
