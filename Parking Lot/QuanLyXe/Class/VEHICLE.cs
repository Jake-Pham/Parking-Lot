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
    class VEHICLE
    {
        MY_DB mydb = new MY_DB();
        public bool updateBike(string MaXe, DateTime NgayGui, DateTime NgayLay, string TrangThai, string PhuongThucGui, string LoaiXe, string BienSo, MemoryStream PicXe, MemoryStream NguoiGui, float GiaTien)
        {
            SqlCommand command = new SqlCommand("UPDATE Vehicles SET NgayGui=@DayGui, NgayLay=@DayLay, TrangThai=@TrangThai, PhuongThucGui=@PhuongThucGui, LoaiXe=@LoaiXe, BienSo=@BienSo, PicXe=@PicXe, NguoiGui=@NguoiGui, GiaTien=@GiaTien WHERE MaXe=@MaXe", mydb.GetConnection);
            command.Parameters.Add("@MaXe", SqlDbType.NChar).Value = MaXe;
            command.Parameters.Add("@DayGui", SqlDbType.DateTime).Value = NgayGui;
            command.Parameters.Add("@DayLay", SqlDbType.DateTime).Value = NgayLay;
            command.Parameters.Add("@TrangThai", SqlDbType.NChar).Value = TrangThai;
            command.Parameters.Add("@PhuongThucGui", SqlDbType.NChar).Value = PhuongThucGui;
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@PicXe", SqlDbType.Image).Value = PicXe.ToArray();
            command.Parameters.Add("@NguoiGui", SqlDbType.Image).Value = NguoiGui.ToArray();
            command.Parameters.Add("@GiaTien", SqlDbType.Float).Value = GiaTien;
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
        public bool addBike(string MaXe, DateTime NgayGui, string TrangThai, string PhuongThucGui, string LoaiXe, string BienSo, MemoryStream PicXe, MemoryStream NguoiGui)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Vehicles (MaXe, NgayGui, TrangThai, PhuongThucGui, LoaiXe, BienSo, PicXe, NguoiGui)" + "VALUES(@MaXe ,@DayGui, @TrangThai, @PhuongThucGui, @LoaiXe, @BienSo, @PicXe, @NguoiGui)", mydb.GetConnection);
            command.Parameters.Add("@MaXe", SqlDbType.NChar).Value = MaXe;
            command.Parameters.Add("@DayGui", SqlDbType.DateTime).Value = NgayGui;
            command.Parameters.Add("@TrangThai", SqlDbType.NChar).Value = TrangThai;
            command.Parameters.Add("@PhuongThucGui", SqlDbType.NChar).Value = PhuongThucGui;
            command.Parameters.Add("LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@PicXe", SqlDbType.Image).Value = PicXe.ToArray();
            command.Parameters.Add("@NguoiGui", SqlDbType.Image).Value = NguoiGui.ToArray();
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
        public bool addtoBill(string Maxe, string LoaiXe, string BienSo, DateTime NgayGui, DateTime NgayLay, float GiaTien)
        {
            SqlCommand command = new SqlCommand("INSERT INTO HoaDon (MaXe, LoaiXe, BienSo, NgayGui, NgayLay, GiaTien)" + "VALUES(@MaXe, @LoaiXe, @BienSo, @NgayGui, @NgayLay, @GiaTien)", mydb.GetConnection);
            command.Parameters.Add("@MaXe", SqlDbType.NChar).Value = Maxe.Trim();
            command.Parameters.Add("@LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo.Trim();
            command.Parameters.Add("@NgayGui", SqlDbType.DateTime).Value = NgayGui;
            command.Parameters.Add("@NgayLay", SqlDbType.DateTime).Value = NgayLay;
            command.Parameters.Add("@GiaTien", SqlDbType.Float).Value = GiaTien;
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
        public bool deleteBike(string MaXe)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Vehicles WHERE MaXe = @MaXe", mydb.GetConnection);
            command.Parameters.Add("@MaXe", SqlDbType.NChar).Value = MaXe;
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
        public DataTable getBike(SqlCommand command)
        {
            command.Connection = mydb.GetConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool checkBike(string MaXe)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Vehicles WHERE MaXe=@maxe", mydb.GetConnection);
            command.Parameters.Add("@maxe", SqlDbType.NChar).Value = MaXe;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string Tangma()
        {
            string sql = @"Select * from AllXe";
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
        public bool addAllXe(string MaXe, DateTime NgayGui, string TrangThai, string PhuongThucGui, string LoaiXe, string BienSo, MemoryStream PicXe, MemoryStream NguoiGui)
        {
            SqlCommand command = new SqlCommand("INSERT INTO AllXe (MaXe, NgayGui, TrangThai, PhuongThucGui, LoaiXe, BienSo, PicXe, NguoiGui)" + "VALUES(@MaXe ,@DayGui, @TrangThai, @PhuongThucGui, @LoaiXe, @BienSo, @PicXe, @NguoiGui)", mydb.GetConnection);
            command.Parameters.Add("@MaXe", SqlDbType.NChar).Value = MaXe;
            command.Parameters.Add("@DayGui", SqlDbType.DateTime).Value = NgayGui;
            command.Parameters.Add("@TrangThai", SqlDbType.NChar).Value = TrangThai;
            command.Parameters.Add("@PhuongThucGui", SqlDbType.NChar).Value = PhuongThucGui;
            command.Parameters.Add("LoaiXe", SqlDbType.NChar).Value = LoaiXe;
            command.Parameters.Add("@BienSo", SqlDbType.NChar).Value = BienSo;
            command.Parameters.Add("@PicXe", SqlDbType.Image).Value = PicXe.ToArray();
            command.Parameters.Add("@NguoiGui", SqlDbType.Image).Value = NguoiGui.ToArray();
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
    }
}
