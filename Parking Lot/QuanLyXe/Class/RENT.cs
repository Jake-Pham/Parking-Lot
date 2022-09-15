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
    class RENT
    {
        MY_DB mydb = new MY_DB();
        public string Tangma()
        {
            string sql = @"Select * from Rent";
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
        public bool addRent(string MaHD, string ChuSH, string CMND, DateTime NgayKy, DateTime NgayLay, string LoaiXe, string BienSo, string GhiChu, MemoryStream PicXe)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Rent (MaHD, ChuSH, CMND, NgayKyHD, NgayLay, LoaiXe, BienSo, GhiChu, PicXe)" + "VALUES(@MaHD, @ChuSH, @CMND, @NgayKy, @NgayLay, @LoaiXe, @BienSo, @GhiChu, @PicXe)", mydb.GetConnection);
            command.Parameters.Add("@MaHD", SqlDbType.NChar).Value = MaHD;
            command.Parameters.Add("@ChuSH", SqlDbType.NChar).Value = ChuSH;
            command.Parameters.Add("@CMND", SqlDbType.NChar).Value = CMND;
            command.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = NgayKy;
            command.Parameters.Add("@NgayLay", SqlDbType.DateTime).Value = NgayLay;
            command.Parameters.Add("LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = GhiChu;
            command.Parameters.Add("@PicXe", SqlDbType.Image).Value = PicXe.ToArray(); ;
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
        public bool deleteRent(string MaHD)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Rent WHERE MaHD = @MaHD", mydb.GetConnection);
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
        public bool updateRent(string MaHD, string ChuSH, string CMND, DateTime NgayKy, DateTime NgayLay, string LoaiXe, string BienSo, string GhiChu, MemoryStream PicXe)
        {
            SqlCommand command = new SqlCommand("UPDATE Rent SET ChuSH=@ChuSH, CMND=@CMND, NgayKyHD=@NgayKy, NgayLay=@NgayLay, LoaiXe=@LoaiXe, BienSo=@BienSo, GhiChu=@GhiChu, PicXe=@PicXe WHERE MaHD=@MaHD", mydb.GetConnection);
            command.Parameters.Add("@MaHD", SqlDbType.NChar).Value = MaHD;
            command.Parameters.Add("@ChuSH", SqlDbType.NChar).Value = ChuSH;
            command.Parameters.Add("@CMND", SqlDbType.NChar).Value = CMND;
            command.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = NgayKy;
            command.Parameters.Add("@NgayLay", SqlDbType.DateTime).Value = NgayLay;
            command.Parameters.Add("LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = GhiChu;
            command.Parameters.Add("@PicXe", SqlDbType.Image).Value = PicXe.ToArray(); ;
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
        public DataTable searchRent(string MaHD)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Rent WHERE MaHD = @MaHD", mydb.GetConnection);
            command.Parameters.Add("@MaHD", SqlDbType.NChar).Value = MaHD;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable getRent(SqlCommand command)
        {
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
