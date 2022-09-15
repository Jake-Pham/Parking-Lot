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
    class BAIXETHUE
    {
        MY_DB mydb = new MY_DB();
        public string Tangma()
        {
            string sql = @"Select * from BaiXeThue";
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Study\Window Programming\FinalProject\Parking Lot\Parking Lot\ParkingLot.mdf;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            string ma = "";
            if (table.Rows.Count <= 0)
            {
                ma = "XE001";
            }
            else
            {
                int k;
                ma = "XE";
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
        public bool addXe(string Id, string BienSo, string ChuSH, DateTime NgayKy, DateTime NgayTra, string TrangThai, string LoaiXe, MemoryStream PicXe)
        {
            SqlCommand command = new SqlCommand("INSERT INTO BaiXeThue (Id, BienSo, ChuSH, NgayKy, NgayTra, TrangThai, LoaiXe, PicXe)" + "VALUES(@Id, @BienSo, @ChuSH, @NgayKy, @NgayTra, @TrangThai, @LoaiXe, @PicXe)", mydb.GetConnection);
            command.Parameters.Add("@Id", SqlDbType.NChar).Value = Id;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@ChuSH", SqlDbType.NChar).Value = ChuSH;
            command.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = NgayKy;
            command.Parameters.Add("@NgayTra", SqlDbType.DateTime).Value = NgayTra;
            command.Parameters.Add("@TrangThai", SqlDbType.NChar).Value = TrangThai;
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@PicXe", SqlDbType.Image).Value = PicXe.ToArray();
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
        public bool deleteXe(string Id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM BaiXeThue WHERE Id = @Id", mydb.GetConnection);
            command.Parameters.Add("@Id", SqlDbType.NChar).Value = Id;
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
        public bool updateXe(string Id, string BienSo, string ChuSH, DateTime NgayKy, DateTime NgayTra, string TrangThai, string LoaiXe, MemoryStream PicXe)
        {
            SqlCommand command = new SqlCommand("UPDATE BaiXeThue SET  BienSo=@BienSo,ChuSH=@ChuSH, NgayKy=@NgayKy, NgayTra=@NgayTra, TrangThai=@TrangThai, LoaiXe=@LoaiXe, PicXe=@PicXe WHERE Id=@Id", mydb.GetConnection);
            command.Parameters.Add("@Id", SqlDbType.NChar).Value = Id;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@ChuSH", SqlDbType.NChar).Value = ChuSH;
            command.Parameters.Add("@NgayKy", SqlDbType.DateTime).Value = NgayKy;
            command.Parameters.Add("@NgayTra", SqlDbType.DateTime).Value = NgayTra;
            command.Parameters.Add("@TrangThai", SqlDbType.NChar).Value = TrangThai;
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@PicXe", SqlDbType.Image).Value = PicXe.ToArray();
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
        public DataTable getXe(SqlCommand command)
        {
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable searchXe(string Id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM BaiXeThue WHERE Id = @Id", mydb.GetConnection);
            command.Parameters.Add("@Id", SqlDbType.NChar).Value = Id;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
