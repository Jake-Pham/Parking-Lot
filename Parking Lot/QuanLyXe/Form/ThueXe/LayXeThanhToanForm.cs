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
    public partial class LayXeThanhToanForm : Form
    {
        public LayXeThanhToanForm()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        RENT rent = new RENT();
        private void ThanhToanButton_Click(object sender, EventArgs e)
        {
            string MAKH = Tangma();
            string ChuSH = ChuSHTextBox.Text;
            string CMND = CMNDTextBox.Text;
            DateTime NgayKy = NgayKyDateTimePicker.Value;
            DateTime NgayLay = NgayLayDateTimePicker.Value;
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
            if (verif())
            {
                SqlCommand command2 = new SqlCommand("DELETE FROM Rent WHERE ChuSH=@ChuSH AND BienSo=@BienSo", mydb.GetConnection);
                string query = "DELETE FROM BaiXeThue WHERE ChuSH=@ChuSH AND BienSo=@BienSo";
                SqlCommand command = new SqlCommand(query, mydb.GetConnection);
                mydb.openConnection();
                command.Parameters.Add("@BienSo", SqlDbType.NVarChar).Value = BienXe;
                command.Parameters.Add("@ChuSH", SqlDbType.NVarChar).Value = ChuSH;
                command2.Parameters.Add("@BienSo", SqlDbType.NVarChar).Value = BienXe;
                command2.Parameters.Add("@ChuSH", SqlDbType.NVarChar).Value = ChuSH;
                command.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                mydb.closeConnection();
                //
                addLayXe(MAKH, ChuSH, CMND, NgayKy, NgayLay, LoaiXe, BienXe, ThanhTien);
                MessageBox.Show("Đã Thanh Toán", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Information);
                

            }
            else
            {
                MessageBox.Show("Error", "Thanh Toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LayXeThanhToanForm_Load(object sender, EventArgs e)
        {
            DateTime d1 = NgayKyDateTimePicker.Value;
            DateTime d2 = NgayLayDateTimePicker.Value;
            DateTime present = DateTime.Now;
            double result = 0;
            if (XeMayRadioButton.Checked)
            {
                result = Tinh_tien(d1, d2) * 80000;
            }
            else if (OToRadioButton.Checked)
            {
                result = Tinh_tien(d1, d2) * 100000;
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
        public bool addLayXe(string MaKH, string ChuSH, string CMND, DateTime NgayKy, DateTime NgayLay, string LoaiXe, string BienSo, double ThanhTien)
        {
            SqlCommand command = new SqlCommand("INSERT INTO LayXeThanhToan (MaKH, ChuSH, CMND, NgayKy, NgayLay, LoaiXe, BienSo, ThanhTien)" + "VALUES(@MaKH, @ChuSH, @CMND, @NgayKy, @NgayLay, @LoaiXe, @BienSo, @ThanhTien)", mydb.GetConnection);
            command.Parameters.Add("@MaKH", SqlDbType.NChar).Value = MaKH;
            command.Parameters.Add("@ChuSH", SqlDbType.NVarChar).Value = ChuSH;
            command.Parameters.Add("@CMND", SqlDbType.NChar).Value = CMND;
            command.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = NgayKy;
            command.Parameters.Add("@NgayLay", SqlDbType.DateTime).Value = NgayLay;
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
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
            if ((ChuSHLabel.Text.Trim() == "") || (CMNDLabel.Text.Trim() == "") || (BienXeLabel.Text.Trim() == "") || (ThanhTienLabel.Text.Trim() == ""))
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
