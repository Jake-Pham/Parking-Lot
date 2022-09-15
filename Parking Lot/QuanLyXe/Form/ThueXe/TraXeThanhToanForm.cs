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
    public partial class TraXeThanhToanForm : Form
    {
        public TraXeThanhToanForm()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        BAIXETHUE baixe = new BAIXETHUE();
        CONTRACT contract = new CONTRACT();

        public void TraXeThanhToanForm_Load(object sender, EventArgs e)
        {
            DateTime d1 = NgayThueDateTimePicker.Value;
            DateTime d2 = NgayTraDateTimePicker.Value;
            DateTime present = DateTime.Now;
            double result = 0;
            if (XeMayRadioButton.Checked)
            {
                if (present >= d2)
                {
                    result = Tinh_tien(d1, d2) * 100000;
                }
                else if (present < d2)
                {
                    int day = d2.Day - present.Day;
                    result = (double)Tinh_tien(d1, d2) + day * 120000;
                }
            }
            else if(OToRadioButton.Checked)
            {
                if (present >= d2)
                {
                    result = Tinh_tien(d1, d2) * 150000;
                }
                else if (present < d2)
                {
                    int day = d2.Day - present.Day;
                    result = (double)Tinh_tien(d1, d2) + day * 170000;
                }
            }
            ThanhTienTextBox.Text = result.ToString();
        }
        private int Tinh_tien(DateTime d1, DateTime d2)
        {
            int year = (d2.Year - d1.Year) * 365;
            int month = (d2.Month - d1.Month) * 30;
            int result = (d2.Day - d1.Day) + year + month;
            return result;
        }

        private void ThanhToanButton_Click(object sender, EventArgs e)
        {
            //
            string MAKH = Tangma();
            string NguoiThue = NguoiThueTextBox.Text;
            string CMND = CMNDTextBox.Text;
            DateTime NgayThue = NgayThueDateTimePicker.Value;
            DateTime NgayTra = NgayTraDateTimePicker.Value;
            string ChuSH = ChuSHTextBox.Text;
            string LoaiXe = "";
            if(XeMayRadioButton.Checked)
            {
                LoaiXe = "Xe May";
            }
            else if(OToRadioButton.Checked)
            {
                LoaiXe = "O To";
            }
            string BienXe = BienXeTextBox.Text;
            double ThanhTien = Convert.ToDouble(ThanhTienTextBox.Text);
            if(verif())
            {
                //Thay đổi trạng thái và xóa khỏi contract
                SqlCommand command2 = new SqlCommand("DELETE FROM Contract WHERE ChuSH=@ChuSH AND BienSo=@BienSo", mydb.GetConnection);
                string query = "UPDATE [BaiXeThue] SET TrangThai='Free' WHERE BienSo=@BienSo AND ChuSH=@ChuSH";
                SqlCommand command = new SqlCommand(query, mydb.GetConnection);
                mydb.openConnection();
                command.Parameters.Add("@BienSo", SqlDbType.NVarChar).Value = BienXe;
                command.Parameters.Add("@ChuSH", SqlDbType.NVarChar).Value = ChuSH;
                command2.Parameters.Add("@BienSo", SqlDbType.NVarChar).Value = BienXe;
                command2.Parameters.Add("@ChuSH", SqlDbType.NVarChar).Value = ChuSH;
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                mydb.closeConnection();
                //Xóa
                addTraXe(MAKH, NguoiThue, CMND, NgayThue, NgayTra, LoaiXe, BienXe, ThanhTien);
                MessageBox.Show("Đã Thanh Toán", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TraXeForm traxe = new TraXeForm();
                traxe.TraXeForm_Load(null, null);
                Close();
            }
            else
            {
                MessageBox.Show("Error", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }
        public bool addTraXe(string MaKH, string NguoiThue, string CMND, DateTime NgayThue, DateTime NgayTra, string LoaiXe, string BienXe, double ThanhTien)
        {
            SqlCommand command = new SqlCommand("INSERT INTO TraXeThanhToan (MaKH, NguoiThue, CMND, NgayThue, NgayTra, LoaiXe, BienXe, ThanhTien)" + "VALUES(@MaKH, @NguoiThue, @CMND, @NgayThue, @NgayTra, @LoaiXe, @BienXe, @ThanhTien)", mydb.GetConnection);
            command.Parameters.Add("@MaKH", SqlDbType.NChar).Value = MaKH;
            command.Parameters.Add("@NguoiThue", SqlDbType.NVarChar).Value = NguoiThue;
            command.Parameters.Add("@CMND", SqlDbType.NChar).Value = CMND;
            command.Parameters.Add("@NgayThue", SqlDbType.DateTime).Value = NgayThue;
            command.Parameters.Add("@NgayTra", SqlDbType.DateTime).Value = NgayTra;
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@Bienxe", SqlDbType.NChar).Value = BienXe;
            command.Parameters.Add("@ThanhTien", SqlDbType.Float).Value = ThanhTien;
            mydb.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }
        public string Tangma()
        {
            string sql = @"Select * from TraXeThanhToan";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Study\Window Programming\FinalProject\Parking Lot\Parking Lot\ParkingLot.mdf;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            string ma = "";
            if (table.Rows.Count <= 0)
            {
                ma = "KH001";
            }
            else
            {
                int k;
                ma = "KH";
                k = Convert.ToInt32(table.Rows[table.Rows.Count - 1][0].ToString().Substring(2, 3));
                k = k + 1;
                if (k < 10)
                {
                    ma = ma + "00";
                }
                else if (k < 100)
                {
                    ma = ma + "0";
                }
                ma = ma + k.ToString();
            }
            return ma;
        }
        bool verif()
        {
            if ((NguoiThueLabel.Text.Trim() == "") || (CMNDLabel.Text.Trim() == "") || (BienXeLabel.Text.Trim() == "") || (ThanhTienLabel.Text.Trim() == ""))
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
