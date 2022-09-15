using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Parking_Lot
{
    class CONTRACT
    {
        MY_DB mydb = new MY_DB();
        public string Tangma()
        {
            string sql = @"Select * from Contract";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Study\Window Programming\FinalProject\Parking Lot\Parking Lot\ParkingLot.mdf;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            string ma = "";
            if (table.Rows.Count <= 0)
            {
                ma = "HD001";
            }
            else
            {
                int k;
                ma = "HD";
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
        public bool addContract(string MaHD, string HoTen, DateTime NgayKy, DateTime NgayTra, string BienSo, string ChuSH, string CMND, string LoaiXe, string GhiChu, MemoryStream NguoiThue)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Contract (MaHD, HoTen, NgayKy, NgayTra, BienSo, ChuSH, CMND, LoaiXe, GhiChu, NguoiThue)" + "VALUES(@MaHD, @HoTen, @NgayKy, @NgayTra, @BienSo, @ChuSH, @CMND, @LoaiXe, @GhiChu, @NguoiThue)", mydb.GetConnection);
            command.Parameters.Add("@MaHD", SqlDbType.NChar).Value = MaHD;
            command.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = HoTen;
            command.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = NgayKy;
            command.Parameters.Add("@NgayTra", SqlDbType.DateTime).Value = NgayTra;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@ChuSH", SqlDbType.NChar).Value = ChuSH;
            command.Parameters.Add("@CMND", SqlDbType.NChar).Value = CMND;
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = GhiChu;
            command.Parameters.Add("@NguoiThue", SqlDbType.Image).Value = NguoiThue.ToArray();
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
        public bool deleteContract(string MaHD)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Contract WHERE MaHD = @MaHD", mydb.GetConnection);
            command.Parameters.Add("@MaHD", SqlDbType.NChar).Value = MaHD;
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
        public bool updateContract(string MaHD, string HoTen, DateTime NgayKy, DateTime NgayTra, string BienSo, string ChuSH, string CMND, string LoaiXe, string GhiChu, MemoryStream NguoiThue)
        {
            SqlCommand command = new SqlCommand("UPDATE Contract SET HoTen=@HoTen, NgayKy=@NgayKy, @NgayTra=@NgayTra, BienSo=@BienSo,ChuSH=@ChuSH, CMND=@CMND, LoaiXe=@LoaiXe, GhiChu=@GhiChu, NguoiThue=@NguoiThue WHERE MaHD=@MaHD", mydb.GetConnection);
            command.Parameters.Add("@MaHD", SqlDbType.NChar).Value = MaHD;
            command.Parameters.Add("@Hoten", SqlDbType.NVarChar).Value = HoTen;
            command.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = NgayKy;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@ChuSH", SqlDbType.NChar).Value = ChuSH;
            command.Parameters.Add("@CMND", SqlDbType.NChar).Value = CMND;
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = GhiChu;
            command.Parameters.Add("@NguoiThue", SqlDbType.Image).Value = NguoiThue.ToArray();
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
        public DataTable getContract(SqlCommand command)
        {
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
